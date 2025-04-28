using MotoManager.Domain.Domain;
using MotoManager.Domain.Dtos;
using MotoManager.Domain.Interfaces.Queries;
using MotoManager.Infra.Data.Repositories;

namespace MotoManager.Infra.Data.Queries
{
    public class LocacaoQuery : ILocacaoQuery
    {
        private readonly GenericRepository _repository;
        private readonly IMotoQuery _motoQuery;

        public LocacaoQuery(GenericRepository repository, IMotoQuery motoQuery)
        {
            _repository = repository;
            _motoQuery = motoQuery;
        }

        public async Task<List<LocacaoDto>> GetLocacoes(CancellationToken cancellationToken = default)
        {
            var query = $@"SELECT  idEntregador as  CodigoEntregador,
                                   idMoto as CodigoMoto,
                                   plano as CodigoPlano,
	                               datainicio as DataInicio, 
	                               datatermino as DataTermino, 
	                               dataprevisaotermino as DataPrevisaoTermino, 
                                   id as Codigo,
	                               DataEntrega as DataDevolucao
                            FROM   LOCACOES";


            var locacoes = await _repository.Query<Locacao>(
                query,
                cancellationToken: cancellationToken);

            if (locacoes == null)
                return null;

            var motos = await _motoQuery.GetMotosByid(locacoes.Select(x => x.CodigoMoto).ToArray(), cancellationToken);

            foreach (var locacao in locacoes)
            {
                locacao.SetMoto(motos.FirstOrDefault(x => x.Identificador == locacao.CodigoMoto));
                // Aqui faltou entregador e plano
            }


            return locacoes.Select(x => (LocacaoDto)x).ToList();

        }
    }
}
