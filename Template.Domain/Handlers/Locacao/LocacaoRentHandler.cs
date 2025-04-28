using Flunt.Notifications;
using MotoManager.Domain.Commands;
using MotoManager.Domain.Commands.Admin.Motos;
using MotoManager.Domain.Commands.Public.Entregador;
using MotoManager.Domain.Commands.Public.Locacao;
using MotoManager.Domain.Domain;
using MotoManager.Domain.Interfaces.Commands;
using MotoManager.Domain.Interfaces.Handler;
using MotoManager.Domain.Interfaces.Repositories;
using MotoManager.Domain.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoManager.Domain.Handlers.Motos
{
    
    public class LocacaoRentHandler : Notifiable<Notification>, IHandler<LocacaoRentCommand>
    {
        private readonly ILocacaoRepository _locacaoRepository;
        private readonly IEntregadorRepository _entregadorRepository;
        private readonly IPlanoRepository _planoRepository;
        private readonly IMotoRespository _motoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LocacaoRentHandler(ILocacaoRepository locacaoRepository, IEntregadorRepository entregadorRepository, IPlanoRepository planoRepository, IMotoRespository motoRepository, IUnitOfWork unitOfWork)
        {
            _locacaoRepository = locacaoRepository;
            _entregadorRepository = entregadorRepository;
            _planoRepository = planoRepository;
            _motoRepository = motoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ICommandResult> Handle(LocacaoRentCommand command, CancellationToken cancellationToken = default)
        {
            await Validate(command);

            if (!IsValid)
                return new CommandResult(false, "Seus dados estão inválidos", Notifications);

            var locacao = await ConvertCommandToDomain(command);

            await _locacaoRepository.Rent(locacao);
            await _unitOfWork.Commit();

            return new CommandResult(true, "Locação efetuada com sucesso");
        }

        private async Task Validate(LocacaoRentCommand command)
        {
            command.Validate();

            if (!command.IsValid)
                AddNotifications(command.Notifications);

            var entregador = await _entregadorRepository.GetByIdentificador(command.CodigoEntregador);
            var plano = await _planoRepository.GetPlanoById(command.CodigoPlano);
            var moto = await _motoRepository.GetMotoByIdentificador(command.CodigoMoto);

            if (moto == null)
                AddNotification(nameof(command.CodigoPlano), "Moto não encontrado");

            if (plano == null)
                AddNotification(nameof(command.CodigoPlano), "Plano não encontrado");

            if (entregador == null)
                AddNotification(nameof(command.CodigoEntregador), "Entregador não encontrado");

            if (entregador?.TipoCnh != Enums.ETipoCnh.A)
                AddNotification(nameof(command.CodigoEntregador), "Somente entregadores habilitados na categoria A podem efetuar locações");
        }

        private async Task<Locacao> ConvertCommandToDomain(LocacaoRentCommand command)
        {
            var plano = await _planoRepository.GetPlanoById(command.CodigoPlano);

            return new Locacao(
                codigoEntregador: command.CodigoEntregador,
                codigoMoto: command.CodigoMoto,
                codigoPlano: command.CodigoPlano,
                dataInicio: command.DataInicio,
                dataTermino: command.DataInicio.AddDays(plano.Dias),
                dataPrevisaoTermino: command.DataPrevisaoTermino);
        } 
    }
}
