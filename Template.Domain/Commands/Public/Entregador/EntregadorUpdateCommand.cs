using Flunt.Notifications;
using Flunt.Validations;
using MotoManager.Domain.Interfaces.Commands;
using System.Text.RegularExpressions;

namespace MotoManager.Domain.Commands.Public.Entregador
{
    public class EntregadorUpdateCommand : Notifiable<Notification>, ICommand
    {
        public EntregadorUpdateCommand(int codigoEntregador, string fotoCnh)
        {
            CodigoEntregador = codigoEntregador;
            FotoCnh = fotoCnh;
        }

        public int CodigoEntregador { get; set; }
        public string FotoCnh { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsGreaterThan(CodigoEntregador, 0, nameof(CodigoEntregador), "Codigo do entregador deve ser informado")
                .IsNotNullOrEmpty(FotoCnh, nameof(FotoCnh), "Foto é obrigatório"));

            if (!IsStringBase64())
                AddNotification(nameof(FotoCnh), "A imagem deve vir codificada em base64");
        }

        private bool IsStringBase64()
        {
            if (string.IsNullOrEmpty(FotoCnh))
                return false;

            if (FotoCnh.Length % 4 != 0)
                return false;
            

            return Regex.IsMatch(FotoCnh, "^[a-zA-Z0-9\\+/]*={0,2}$");
        }
    }
}
