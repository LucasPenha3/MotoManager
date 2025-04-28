using Flunt.Notifications;
using MotoManager.Domain.Interfaces.Commands;

namespace MotoManager.Domain.Commands.Public.Locacao
{
    public class LocacaoReturnCommand : Notifiable<Notification>, ICommand
    {
        public LocacaoReturnCommand(int codigoLocacao, DateTime dataDevolucao)
        {
            CodigoLocacao = codigoLocacao;
            DataDevolucao = dataDevolucao;
        }

        public int CodigoLocacao { get; set; }
        public DateTime DataDevolucao { get; set; }

        public void Validate()
        {
            if(DataDevolucao == null)
                AddNotification(nameof(DataDevolucao), "Data de devolução deve ser preenchida");
        }
    }
}
