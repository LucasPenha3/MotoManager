using Flunt.Notifications;
using Flunt.Validations;
using MotoManager.Domain.Interfaces.Commands;

namespace MotoManager.Domain.Commands.Admin.Motos
{
    public class MotosDeleteCommand : Notifiable<Notification>, ICommand
    {
        public MotosDeleteCommand() { }

        public MotosDeleteCommand(string identificador)
        {
            Identificador = identificador;
        }

        public string Identificador { get; set; }


        public void Validate()
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNullOrEmpty(Identificador, nameof(Identificador), "Indique o Id para exclusão da moto."));
        }
    }
}
