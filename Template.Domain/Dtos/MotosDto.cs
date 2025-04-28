
using MotoManager.Domain.Domain;

namespace MotoManager.Domain.Dtos
{
    public class MotoDto
    {
        public string Identificador { get; set; }
        public int Ano { get; set; }
        public string Modelo { get; set; }
        public string Placa { get; set; }

        public static implicit operator MotoDto(Moto moto)
        {
            return new MotoDto
            {
                Ano = moto.Ano,
                Identificador = moto.Identificador,
                Modelo = moto.Modelo,
                Placa = moto.Placa
            };
        }

        public static implicit operator Moto(MotoDto moto)
        {
            return new Moto
            (
                ano: moto.Ano,
                identificador: moto.Identificador,
                modelo: moto.Modelo,
                placa: moto.Placa
            );
        }
    }
}
