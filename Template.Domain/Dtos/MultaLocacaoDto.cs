using MotoManager.Domain.Domain;

namespace MotoManager.Domain.Dtos
{
    public class MultaLocacaoDto
    {
        public decimal Multa { get; set; }
        public decimal AdicionalDiaria { get; set; }

        public static implicit operator MultaLocacaoDto(MultaLocacao multa)
        {
            return new MultaLocacaoDto
            {
                AdicionalDiaria = multa.AdicionalDiaria,
                Multa = multa.Multa
            };
        }

    }
}
