namespace MotoManager.Domain.Domain
{
    public class Plano
    {
        public Plano(
            int codigo,
            int dias,
            decimal preco,
            decimal percentualMultaDiaria,
            decimal valorAdicionalDiaria)
        {
            Dias = dias;
            Preco = preco;
            PercentualMultaDiaria = percentualMultaDiaria;
            ValorAdicionalDiaria = valorAdicionalDiaria;
            Codigo = codigo;
        }

        public int Codigo { get; private set; }
        public int Dias { get; private set; }
        public decimal Preco { get; private set; }
        public decimal PercentualMultaDiaria { get; private set; }
        public decimal ValorAdicionalDiaria { get; private set; }
    }
}
