using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoManager.Domain.Domain
{
    public class Moto
    {
        public Moto(string identificador, int ano, string modelo, string placa)
        {
            Identificador = identificador;
            Ano = ano;
            Modelo = modelo;
            Placa = placa;
        }

        public string Identificador { get; private set; }
        public int Ano { get; private set; }
        public string Modelo { get; private set; }
        public string Placa { get; private set; }

        public void UpdatePlaca(string placa) => Placa = placa;

    }
}
