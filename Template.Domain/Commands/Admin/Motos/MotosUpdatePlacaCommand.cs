using Flunt.Notifications;
using Flunt.Validations;
using MotoManager.Domain.Interfaces.Commands;

namespace MotoManager.Domain.Commands.Admin.Motos
{
    public class MotosUpdatePlacaCommand : Notifiable<Notification>, ICommand
    {
        public MotosUpdatePlacaCommand() { }

        public MotosUpdatePlacaCommand(string placa)
        {
            Placa = placa;
        }

        public string Placa { get; set; }


        public void Validate()
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNullOrEmpty(Placa, nameof(Placa), "Placa é obrigatório"));
        }
    }
}
