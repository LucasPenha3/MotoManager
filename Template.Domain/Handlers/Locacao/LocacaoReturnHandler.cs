using Flunt.Notifications;
using MotoManager.Domain.Commands;
using MotoManager.Domain.Commands.Public.Locacao;
using MotoManager.Domain.Domain;
using MotoManager.Domain.Dtos;
using MotoManager.Domain.Interfaces.Commands;
using MotoManager.Domain.Interfaces.Handler;
using MotoManager.Domain.Interfaces.Repositories;
using MotoManager.Domain.Interfaces.UnitOfWork;

namespace MotoManager.Domain.Handlers.Motos
{
    public class LocacaoReturnHandler : Notifiable<Notification>, IHandler<LocacaoReturnCommand>
    {

        private readonly ILocacaoRepository _locacaoRepository;
        private readonly IEntregadorRepository _entregadorRepository;
        private readonly IPlanoRepository _planoRepository;
        private readonly IMotoRespository _motoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LocacaoReturnHandler(
            ILocacaoRepository locacaoRepository,
            IEntregadorRepository entregadorRepository,
            IPlanoRepository planoRepository,
            IMotoRespository motoRepository,
            IUnitOfWork unitOfWork)
        {
            _locacaoRepository = locacaoRepository;
            _entregadorRepository = entregadorRepository;
            _planoRepository = planoRepository;
            _motoRepository = motoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ICommandResult> Handle(LocacaoReturnCommand command, CancellationToken cancellationToken = default)
        {
            var locacao = await ConvertCommandToDomain(command);

            await Validate(command, locacao);

            if (!IsValid)
                return new CommandResult(false, "Seus dados estão inválidos", Notifications);
            
            await _locacaoRepository.Return(locacao);
            await _unitOfWork.Commit();

            var multaDevolucao = locacao.GetMulta();

            return new CommandResult(true, "Devolução Efetuada", (MultaLocacaoDto)multaDevolucao);
        }

        private async Task Validate(LocacaoReturnCommand command, Locacao locacaoDevolver)
        {
            command.Validate();

            if (!command.IsValid)
                AddNotifications(command.Notifications);

            if (locacaoDevolver == null)
                AddNotification(nameof(command.CodigoLocacao), "Locação não encontrada");

        }

        private async Task<Locacao> ConvertCommandToDomain(LocacaoReturnCommand command)
        {
            var locacao = await _locacaoRepository.GetLocacaoById(command.CodigoLocacao);

            locacao.Devolver(command.DataDevolucao);

            return locacao;
        }

    }
}
