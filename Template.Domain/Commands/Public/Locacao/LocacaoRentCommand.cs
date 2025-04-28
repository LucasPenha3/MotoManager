using Flunt.Notifications;
using Flunt.Validations;
using MotoManager.Domain.Interfaces.Commands;

namespace MotoManager.Domain.Commands.Public.Locacao
{
    public class LocacaoRentCommand : Notifiable<Notification>, ICommand
    {
        public int CodigoEntregador { get; set; }
        public string CodigoMoto { get; set; }
        public int CodigoPlano { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataPrevisaoTermino { get; set; }

        public void Validate()
        {
            if (DataInicio == null || DataInicio > DataInicio.AddDays(1).AddHours(23))
                AddNotification(nameof(DataInicio), "Data de inicio deve ser obrigatoriamente amanhã");

            if (DataPrevisaoTermino == null)
                AddNotification(nameof(DataInicio), "Data de previsão deve ser informada");

            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsGreaterThan(CodigoEntregador,0, "Codigo do entregador deve ser válido")
                .IsGreaterThan(CodigoPlano,0, "Codigo do plano deve ser válido")
                .IsNotNullOrEmpty(CodigoMoto, nameof(CodigoMoto), "Codigo da moto deve ser válido"));
        }
    }
}
