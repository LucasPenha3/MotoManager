using Flunt.Notifications;
using MotoManager.Domain.Commands;
using MotoManager.Domain.Commands.Public.Entregador;
using MotoManager.Domain.Domain;
using MotoManager.Domain.Interfaces.Commands;
using MotoManager.Domain.Interfaces.Handler;
using MotoManager.Domain.Interfaces.Repositories;
using MotoManager.Domain.Interfaces.Services;
using MotoManager.Domain.Interfaces.UnitOfWork;

namespace MotoManager.Domain.Handlers.Motos
{
    public class EntregadorUpdateHandler : Notifiable<Notification>, IHandler<EntregadorUpdateCommand>
    {
        private readonly IEntregadorRepository _entregadorRepository;
        private readonly IFileService _fileService;
        private readonly IUnitOfWork _unitOfWork;

        public EntregadorUpdateHandler(IEntregadorRepository entregadorRepository, IFileService file, IUnitOfWork unitOfWork)
        {
            _entregadorRepository = entregadorRepository;
            _fileService = file;
            _unitOfWork = unitOfWork;
        }

        public async Task<ICommandResult> Handle(EntregadorUpdateCommand command, CancellationToken cancellationToken = default)
        {
            var entregador = await ConvertCommandToDomain(command);

            await Validate(command, entregador);

            if (!IsValid)
                return new CommandResult(false, "Seus dados estão inválidos", Notifications);

            command.FotoCnh = _fileService.SaveDisk(command.FotoCnh);

            entregador.UpdateCnh(command.FotoCnh);

            await _entregadorRepository.UpdateCnh(entregador);
            await _unitOfWork.Commit();



            return new CommandResult(true, "Foto CNH alterada com sucesso");
        }

        private async Task Validate(EntregadorUpdateCommand command, Entregador entregadorAtualizar)
        {
            command.Validate();

            if (!command.IsValid)
                AddNotifications(command.Notifications);

            if (entregadorAtualizar == null)
                AddNotification(nameof(command.CodigoEntregador), "Entregador não encontrado");
        }

        private async Task<Entregador> ConvertCommandToDomain(EntregadorUpdateCommand command)
        {
            var entregador = await _entregadorRepository.GetByIdentificador(command.CodigoEntregador);

            return entregador;
        }
    }
}
