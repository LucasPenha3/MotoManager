using Flunt.Notifications;
using MotoManager.Domain.Commands;
using MotoManager.Domain.Commands.Admin.Motos;
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
    public class MotosDeleteHandler : Notifiable<Notification>, IHandler<MotosDeleteCommand>
    {
        private readonly IMotoRespository _motoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MotosDeleteHandler(IMotoRespository motoRepository, IUnitOfWork unitOfWork)
        {
            _motoRepository = motoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ICommandResult> Handle(MotosDeleteCommand command, CancellationToken cancellationToken = default)
        {
            await Validate(command);

            if (!IsValid)
                return new CommandResult(false, "Seus dados estão inválidos", Notifications);


            await _motoRepository.DeleteMoto(command.Identificador);
            await _unitOfWork.Commit();

            return new CommandResult(true, "Moto excluida com sucesso");
        }

        private async Task Validate(MotosDeleteCommand command)
        {
            command.Validate();

            if (!command.IsValid)
                AddNotifications(command.Notifications);

            var motoExists = await _motoRepository.GetMotoByIdentificador(command.Identificador);

            if (motoExists == null)
                AddNotification(nameof(command.Identificador), "Moto não encontrada");
        }

        private async Task<Moto> ConvertCommandToDomain(MotosUpdateCommand command)
        {
            var moto = (await _motoRepository.GetMotosByPlaca(command.Placa)).FirstOrDefault();

            moto.UpdatePlaca(command.Placa);

            return moto;
        }
    }
}
