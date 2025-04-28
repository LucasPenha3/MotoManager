using Flunt.Notifications;
using MotoManager.Domain.Commands;
using MotoManager.Domain.Commands.Admin.Motos;
using MotoManager.Domain.Commands.Public.Entregador;
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
    
    public class EntregadorCreateHandler : Notifiable<Notification>, IHandler<EntregadorCreateCommand>
    {
        private readonly IEntregadorRepository _entregadorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EntregadorCreateHandler(IEntregadorRepository entregadorRepository, IUnitOfWork unitOfWork)
        {
            _entregadorRepository = entregadorRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ICommandResult> Handle(EntregadorCreateCommand command, CancellationToken cancellationToken = default)
        {
            await Validate(command);

            if (!IsValid)
                return new CommandResult(false, "Seus dados estão inválidos", Notifications);

            var entregador = ConvertCommandToDomain(command);

            await _entregadorRepository.Create(entregador);
            await _unitOfWork.Commit();

            return new CommandResult(true, "Entregador criado com sucesso");
        }

        private async Task Validate(EntregadorCreateCommand command)
        {
            command.Validate();

            if (!command.IsValid)
                AddNotifications(command.Notifications);

            var entregadorExistsCnh = await _entregadorRepository.GetByCnh(command.NumeroCnh);
            var entregadorExistsCnpj = await _entregadorRepository.GetByCnpj(command.Cnpj);

            if (entregadorExistsCnh != null)
                AddNotification(nameof(command.NumeroCnh), "Já existe um entregador com esse CNH");

            if (entregadorExistsCnpj != null)
                AddNotification(nameof(command.Cnpj), "Já existe um entregador com esse CNPJ");
        }

        private Entregador ConvertCommandToDomain(EntregadorCreateCommand command)
        {
            return new Entregador(
                codigo: 0,
                nome: command.Nome,
                cnpj: command.Cnpj,
                cnh: command.NumeroCnh,
                tipoCnh: ((int) command.TipoCnh).ToString(),
                dataNascimento: command.DataNascimento);
        }

    }
}
