using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoManager.Domain.Domain
{
    public class Locacao
    {
        public Locacao(
            int codigoEntregador,
            string codigoMoto,
            int codigoPlano,
            DateTime dataInicio,
            DateTime dataTermino,
            DateTime dataPrevisaoTermino,
            int codigo = 0,
            DateTime? dataDevolucao = null)
        {
            CodigoEntregador = codigoEntregador;
            CodigoMoto = codigoMoto;
            CodigoPlano = codigoPlano;
            DataInicio = dataInicio;
            DataTermino = dataTermino;
            DataPrevisaoTermino = dataPrevisaoTermino;
            Codigo = codigo;
            DataDevolucao = dataDevolucao;
        }

        public int Codigo { get; private set; }
        public int CodigoEntregador { get; private set; }
        public string CodigoMoto { get; private set; }
        public int CodigoPlano { get; private set; }
        public DateTime DataInicio { get; private set; }
        public DateTime DataTermino { get; private set; }
        public DateTime DataPrevisaoTermino { get; private set; }
        public DateTime? DataDevolucao { get; private set; }
        public Moto Moto { get; private set; }
        public Entregador Entregador { get; private set; }
        public Plano Plano { get; private set; }


        public void SetMoto(Moto moto) => Moto = moto;
        public void SetEntregador(Entregador entregador) => Entregador = entregador;
        public void SetPlano(Plano plano) => Plano = plano;

        public void Devolver(DateTime dataDevolucao)
        {
            DataDevolucao = dataDevolucao;
        }

        public MultaLocacao GetMulta()
        {

            var diasNaoEfetivados = (DataPrevisaoTermino - DataDevolucao)?.Days;
            var valorDiariasNaoEfetivadas = diasNaoEfetivados * Plano.Preco;

            decimal multaPercentual7 = 20;
            decimal multaPercentual15 = 40;

            if (DataDevolucao < DataPrevisaoTermino && Plano.Dias == 7)
            {
                return new MultaLocacao(
                    multa: ((multaPercentual7 / 100) * valorDiariasNaoEfetivadas) ?? 0,
                    adicionalDiaria: 0);
            }

            if (DataDevolucao < DataPrevisaoTermino && Plano.Dias == 15)
            {
                return new MultaLocacao(
                    multa: ((decimal?)(multaPercentual15 / 100) * valorDiariasNaoEfetivadas) ?? 0,
                    adicionalDiaria: 0);
            }

            if (DataDevolucao > DataPrevisaoTermino)
            {
                var diasAdicionais = (DataDevolucao - DataPrevisaoTermino)?.Days;

                return new MultaLocacao(
                    multa: 0,
                    adicionalDiaria: (diasAdicionais * 50) ?? 0);
            }

            return new MultaLocacao(
                   multa: 0,
                   adicionalDiaria: 0);
        }

    }

    public class MultaLocacao
    {
        public MultaLocacao(decimal multa, decimal adicionalDiaria)
        {
            Multa = multa;
            AdicionalDiaria = adicionalDiaria;
        }

        public decimal Multa { get; private set; }
        public decimal AdicionalDiaria { get; private set; }
    }

}
