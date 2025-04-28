using MotoManager.Domain.Domain;
using MotoManager.Domain.Dtos;
using MotoManager.Domain.Interfaces.Queries;
using MotoManager.Infra.Data.Repositories;

namespace MotoManager.Infra.Data.Queries
{
    public class MotoQuery : IMotoQuery
    {
        private readonly GenericRepository _repository;

        public MotoQuery(GenericRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<MotoDto>> GetMotos(CancellationToken cancellationToken = default)
        {
            try
            {
                var query = $@"SELECT *
                               FROM   MOTOS";

                var moto = await _repository.Query<Moto>(
                    sql: query,
                    cancellationToken: cancellationToken);

                return moto.Select(x => (MotoDto)x).ToList();
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<MotoDto> GetMotoById(string codigoMoto, CancellationToken cancellationToken = default)
        {
            try
            {
                var query = $@"SELECT *
                               FROM   MOTOS
                               WHERE  IDENTIFICADOR = @identificador";

                var moto = await _repository.QueryFirstOrDefault<Moto>(
                    sql: query,
                    param: new { identificador = codigoMoto },
                    cancellationToken: cancellationToken);

                return (MotoDto)moto;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<List<MotoDto>> GetMotosByid(string[] codigosMoto, CancellationToken cancellationToken = default)
        {
            try
            {
                var identifiers = string.Join(",", codigosMoto.Select(id => $"'{id}'")); 

                var query = $@"SELECT *
                               FROM MOTOS
                               WHERE IDENTIFICADOR IN ({identifiers})";

                var motos = await _repository.Query<Moto>(
                    sql: query,
                    cancellationToken: cancellationToken);

                return motos.Select(x => (MotoDto)x).ToList();
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
