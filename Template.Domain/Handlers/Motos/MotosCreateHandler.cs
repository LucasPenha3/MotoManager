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
    
    public class MotosCreateHandler : Notifiable<Notification>, IHandler<MotosCreateCommand>
    {
        private readonly IMotoRespository _motoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MotosCreateHandler(IMotoRespository motoRepository, IUnitOfWork unitOfWork)
        {
            _motoRepository = motoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ICommandResult> Handle(MotosCreateCommand command, CancellationToken cancellationToken = default)
        {
            await Validate(command);

            if (!IsValid)
                return new CommandResult(false, "Seus dados estão inválidos", Notifications);

            var moto = ConvertCommandToDomain(command);

            await _motoRepository.CreateMoto(moto, cancellationToken);

            await _unitOfWork.Commit(cancellationToken);

            // TODO Fila

            return new CommandResult(true, "Moto criada com sucesso");
        }

        private async Task Validate(MotosCreateCommand command)
        {
            command.Validate();

            if (!command.IsValid)
                AddNotifications(command.Notifications);

            var motoExists = await _motoRepository.GetMotosByPlaca(command.Placa);

            if (motoExists != null && motoExists.Any())
                AddNotification(nameof(command.Placa), "Já existe uma moto com essa placa");
        }

        private Moto ConvertCommandToDomain(MotosCreateCommand command)
        {
            return new Moto(
                identificador: command.Identificador,
                ano: command.Ano,
                modelo: command.Modelo,
                placa: command.Placa);
        } 
    }
}
