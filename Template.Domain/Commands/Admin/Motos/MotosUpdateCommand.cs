using Flunt.Notifications;
using Flunt.Validations;
using MotoManager.Domain.Interfaces.Commands;

namespace MotoManager.Domain.Commands.Admin.Motos
{
    public class MotosUpdateCommand : Notifiable<Notification>, ICommand
    {
        public MotosUpdateCommand() { }

        public MotosUpdateCommand(string placa, string identificador)
        {
            Placa = placa;
            Identificador = identificador;
        }

        public string Identificador { get; set; }
        public string Placa { get; set; }


        public void Validate()
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNullOrEmpty(Placa, nameof(Placa), "Placa é obrigatório")
                .IsNotNullOrEmpty(Identificador, nameof(Identificador), "O identificador da moto é obrigatório"));
        }
    }
}
