
using MotoManager.Domain.Enums;

namespace MotoManager.Domain.Domain
{
    public class Entregador
    {
        public Entregador(
            int codigo,
            string nome,
            string cnpj,
            DateTime dataNascimento,
            string cnh,
            string tipoCnh,
            string imagemCnh = null)
        {
            Codigo = codigo;
            Nome = nome;
            Cnpj = cnpj;
            Cnh = cnh;
            TipoCnh = (ETipoCnh)int.Parse(tipoCnh);
            ImagemCnh = imagemCnh;
            DataNascimento = dataNascimento;
        }

        public int Codigo { get; private set; }
        public string Nome { get; private set; }
        public string Cnpj { get; private set; }
        public string Cnh { get; private set; }
        public ETipoCnh TipoCnh { get; private set; }
        public string ImagemCnh { get; private set; }
        public DateTime DataNascimento { get; private set; }

        public void UpdateCnh(string CaminhoImagem) => ImagemCnh = CaminhoImagem;

    }
}
