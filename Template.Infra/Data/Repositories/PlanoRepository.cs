using MotoManager.Domain.Domain;
using MotoManager.Domain.Interfaces.Repositories;

namespace MotoManager.Infra.Data.Repositories
{
    public class PlanoRepository : IPlanoRepository
    {
        private readonly GenericRepository _repository;

        public PlanoRepository(GenericRepository repository)
        {
            _repository = repository;
        }

        public async Task<Plano> GetPlanoById(int id, CancellationToken cancellationToken = default)
        {

            var query = $@"SELECT  id as codigo, 
                                   dias as dias, 
                                   preco as preco, 
                                   multapercentualadiantamento as PercentualMultaDiaria, 
                                   adicionaldiaria as ValorAdicionalDiaria
                            FROM   PLANOS
                            WHERE  ID = @id";

            var plano = await _repository.QueryFirstOrDefault<Plano>(
                sql: query,
                param: new { id = id },
                cancellationToken: cancellationToken);

            return plano;
        }
    }
}
