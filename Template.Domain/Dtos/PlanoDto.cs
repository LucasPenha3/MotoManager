using MotoManager.Domain.Domain;

namespace MotoManager.Domain.Dtos
{
    public class PlanoDto
    {
        public int Codigo { get; set; }
        public int Dias { get; set; }
        public decimal Preco { get; set; }
        public decimal PercentualMultaDiaria { get; set; }
        public decimal ValorAdicionalDiaria { get; set; }

        public static implicit operator PlanoDto(Plano plano)
        {
            return new PlanoDto
            {
                Dias = plano.Dias,
                Preco = plano.Preco,
                PercentualMultaDiaria = plano.PercentualMultaDiaria,
                ValorAdicionalDiaria = plano.ValorAdicionalDiaria
            };
        }

        public static implicit operator Plano(PlanoDto plano)
        {
            return new Plano(
                codigo: plano.Codigo,
                dias: plano.Dias,
                preco: plano.Preco,
                percentualMultaDiaria: plano.PercentualMultaDiaria,
                valorAdicionalDiaria: plano.ValorAdicionalDiaria
            );
        }
    }
}
