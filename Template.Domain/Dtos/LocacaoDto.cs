
using MotoManager.Domain.Domain;

namespace MotoManager.Domain.Dtos
{
    public class LocacaoDto
    {
        public int Codigo { get; set; }
        public int CodigoEntregador { get; set; }
        public int CodigoPlano { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
        public DateTime DataPrevisaoTermino { get; set; }
        public DateTime? DataDevolucao { get; set; }
        public MotoDto MotoAlugada { get; set; }
        public MultaLocacaoDto Multa { get; set; }

        public static implicit operator LocacaoDto(Locacao locacao)
        {
            return new LocacaoDto
            {
                Codigo = locacao.Codigo,
                CodigoEntregador = locacao.CodigoEntregador,
                CodigoPlano = locacao.CodigoPlano,
                DataDevolucao = locacao.DataDevolucao,
                DataInicio = locacao.DataInicio,
                DataPrevisaoTermino = locacao.DataPrevisaoTermino,
                DataTermino = locacao.DataTermino,
                MotoAlugada = (MotoDto)locacao.Moto,
                Multa = (MultaLocacaoDto)locacao.GetMulta()
            };
        }

        public class MultaLocacaoDto
        {
            public decimal Multa { get; set; }
            public decimal AdicionalDiaria { get; set; }

            public static implicit operator MultaLocacaoDto(MultaLocacao multa)
            {
                return new MultaLocacaoDto
                {
                    Multa = multa.Multa,
                    AdicionalDiaria = multa.AdicionalDiaria,
                };
            }
        }
    }
}
