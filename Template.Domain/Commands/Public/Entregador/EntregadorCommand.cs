using Flunt.Notifications;
using Flunt.Validations;
using MotoManager.Domain.Enums;
using MotoManager.Domain.Interfaces.Commands;

namespace MotoManager.Domain.Commands.Public.Entregador
{
    public class EntregadorCreateCommand : Notifiable<Notification>, ICommand
    {
        public int Identificador { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public string NumeroCnh { get; set; }
        public ETipoCnh? TipoCnh { get; set; }
        public DateTime DataNascimento { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsGreaterThan(Identificador, 0,nameof(Identificador), "Identificador é obrigatório")
                .IsNotNullOrEmpty(Nome, nameof(Nome), "Nome é obrigatório")
                .IsNotNullOrEmpty(Cnpj, nameof(Cnpj), "CNPJ é obrigatório")
                .IsNotNullOrEmpty(NumeroCnh, nameof(NumeroCnh), "Numero da CNH é obrigatório"));

            if (!IsValidTipoCnh())
                AddNotification(nameof(TipoCnh), "Tipo CNH deva ser A (0), B (1) ou A+B (2)");

        }

        private bool IsValidTipoCnh()
        {
            return TipoCnh switch
            {
                ETipoCnh.A => true,
                ETipoCnh.B => true,
                ETipoCnh.AB => true,
                _ => false,
            };
        }
    }
}
