using Flunt.Notifications;
using Flunt.Validations;
using MotoManager.Domain.Interfaces.Commands;

namespace MotoManager.Domain.Commands.Admin.Motos
{
    public class MotosCreateCommand : Notifiable<Notification>, ICommand
    {
        public MotosCreateCommand() { }

        public MotosCreateCommand(
            string identificador,
            int ano,
            string modelo,
            string placa)
        {
            Identificador = identificador;
            Ano = ano;
            Modelo = modelo;
            Placa = placa;

            Validate();
        }

        public string Identificador { get; set; }
        public int Ano { get; set; }
        public string Modelo { get; set; }
        public string Placa { get; set; }


        public void Validate()
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNullOrEmpty(Identificador, nameof(Identificador), "Identificador é obrigatório")
                .IsNotNullOrEmpty(Modelo, nameof(Modelo), "Modelo é obrigatório")
                .IsNotNullOrEmpty(Placa, nameof(Placa), "Placa é obrigatório")
                .IsGreaterThan(Ano, 0, nameof(Ano), "Ano é obrigatório"));

        }


    }
}
