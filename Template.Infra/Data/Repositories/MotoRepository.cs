using MotoManager.Domain.Domain;
using MotoManager.Domain.Interfaces.Repositories;

namespace MotoManager.Infra.Data.Repositories
{
    public class MotoRepository : IMotoRespository
    {
        private readonly GenericRepository _repository;

        public MotoRepository(GenericRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateMoto(Moto moto, CancellationToken cancellationToken = default)
        {
            var sql = @$"INSERT INTO MOTOS
                         (identificador, ano, modelo, placa)
	                     VALUES
                         (@identificador, @ano, @modelo, @placa)";

            await _repository.Execute(
                sql: sql,
                param: new
                {
                    identificador = moto.Identificador,
                    ano = moto.Ano,
                    modelo = moto.Modelo,
                    placa = moto.Placa
                },
                cancellationToken: cancellationToken);
        }

        public async Task DeleteMoto(string Identificador, CancellationToken cancellationToken = default)
        {
            var sql = @$"DELETE FROM MOTOS
                          WHERE IDENTIFICADOR = @identificador";

            await _repository.Execute(
                sql: sql,
                param: new
                {
                    identificador = Identificador
                },
                cancellationToken: cancellationToken);
        }

        public async Task<Moto> GetMotoByIdentificador(string identificador, CancellationToken cancellationToken = default)
        {
            try
            {
                var query = $@"SELECT *
                           FROM   MOTOS
                           WHERE  IDENTIFICADOR = @identificador";

                var moto = await _repository.QueryFirstOrDefault<Moto>(
                    sql: query,
                    param: new { identificador = identificador },
                    cancellationToken: cancellationToken);

                return moto;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<List<Moto>> GetMotosByPlaca(string placa, CancellationToken cancellationToken = default)
        {

            var query = $@"SELECT *
                           FROM   MOTOS
                           WHERE  PLACA = @placa";

            var moto = await _repository.Query<Moto>(
                sql: query,
                param: new { placa = placa },
                cancellationToken: cancellationToken);

            return moto.ToList();
        }

        public async Task UpdateMoto(Moto moto, CancellationToken cancellationToken = default)
        {
            var query = @$"UPDATE MOTOS
                           SET    PLACA = @placa
                           WHERE  IDENTIFICADOR = @identificador";

            await _repository.Execute(
                sql: query,
                param: new
                {
                    placa = moto.Placa,
                    identificador = moto.Identificador
                },
                cancellationToken: cancellationToken);
        }
    }
}
