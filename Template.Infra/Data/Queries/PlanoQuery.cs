using MotoManager.Domain.Domain;
using MotoManager.Domain.Interfaces.Queries;
using MotoManager.Infra.Data.Repositories;

namespace MotoManager.Infra.Data.Queries
{
    public class PlanoQuery : IPlanoQuery
    {
        private readonly GenericRepository _repository;

        public PlanoQuery(GenericRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Plano>> GetPlanosById(int[] ids, CancellationToken cancellationToken = default)
        {
            var identifiers = string.Join(",", ids.Select(id => $"'{id}'"));

            var query = $@"SELECT  id as codigo, 
                                   dias as dias, 
                                   preco as preco, 
                                   multapercentualadiantamento as PercentualMultaDiaria, 
                                   adicionaldiaria as ValorAdicionalDiaria
                            FROM   PLANOS
                            WHERE  id IN ({identifiers})";

            var planos = await _repository.Query<Plano>(
                sql: query,
                param: new { Codigos = ids },
                cancellationToken: cancellationToken);

            return planos.ToList();
        }
    }
}
