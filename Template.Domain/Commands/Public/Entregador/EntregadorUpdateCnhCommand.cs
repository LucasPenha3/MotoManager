using Flunt.Notifications;
using Flunt.Validations;
using MotoManager.Domain.Interfaces.Commands;

namespace MotoManager.Domain.Commands.Public.Entregador
{
    public class EntregadorUpdateCnhCommand : Notifiable<Notification>, ICommand
    {
        
        public string FotoCnh { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNullOrEmpty(FotoCnh, nameof(FotoCnh), "Foto é obrigatório"));
        }
    }
}
