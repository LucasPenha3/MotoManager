using Flunt.Notifications;
using Flunt.Validations;
using MotoManager.Domain.Interfaces.Commands;

namespace MotoManager.Domain.Commands.Public.Entregador
{
    public class EntregadorUpdateCnhCommand : Notifiable<Notification>, ICommand
    {
        
        public string FotoCnhBase64 { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNullOrEmpty(FotoCnhBase64, nameof(FotoCnhBase64), "Foto é obrigatório"));
        }
    }
}
