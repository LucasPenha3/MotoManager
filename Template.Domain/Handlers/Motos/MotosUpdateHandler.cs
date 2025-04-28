using Flunt.Notifications;
using MotoManager.Domain.Commands;
using MotoManager.Domain.Commands.Admin.Motos;
using MotoManager.Domain.Domain;
using MotoManager.Domain.Interfaces.Commands;
using MotoManager.Domain.Interfaces.Handler;
using MotoManager.Domain.Interfaces.Repositories;
using MotoManager.Domain.Interfaces.UnitOfWork;

namespace MotoManager.Domain.Handlers.Motos
{
    public class MotosUpdateHandler : Notifiable<Notification>, IHandler<MotosUpdateCommand>
    {
        private readonly IMotoRespository _motoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MotosUpdateHandler(IMotoRespository motoRepository, IUnitOfWork unitOfWork)
        {
            _motoRepository = motoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ICommandResult> Handle(MotosUpdateCommand command, CancellationToken cancellationToken = default)
        {
            var moto = await ConvertCommandToDomain(command);

            await Validate(command, moto);

            if (!IsValid)
                return new CommandResult(false, "Seus dados estão inválidos", Notifications);

            await _motoRepository.UpdateMoto(moto);
            await _unitOfWork.Commit();

            return new CommandResult(true, "Moto alterada com sucesso");
        }

        private async Task Validate(MotosUpdateCommand command, Moto motoAtualizar)
        {
            command.Validate();

            if (!command.IsValid)
                AddNotifications(command.Notifications);

            var motoExists = await _motoRepository.GetMotosByPlaca(command.Placa);

            // se já existir uma moto com id diferente não pode usar essa placa
            if (motoExists != null && motoExists.Any(x => x.Identificador != command.Identificador))
                AddNotification(nameof(command.Placa), "Já existe uma moto com essa placa");

            if(motoAtualizar == null)
                AddNotification(nameof(command.Identificador), "Moto não encontrada");
        }

        private async Task<Moto> ConvertCommandToDomain(MotosUpdateCommand command)
        {
            var moto = await _motoRepository.GetMotoByIdentificador(command.Identificador);

            if (moto == null)
                return null;

            moto.UpdatePlaca(command.Placa);

            return moto;
        }

    }
}
