using MotoManager.Domain.Domain;
using MotoManager.Domain.Enums;
using MotoManager.Domain.Interfaces.Repositories;

namespace MotoManager.Infra.Data.Repositories
{
    public class EntregadorRepository : IEntregadorRepository
    {
        private readonly GenericRepository _repository;

        public EntregadorRepository(GenericRepository repository)
        {
            _repository = repository;
        }

        public async Task Create(Entregador entregador, CancellationToken cancellationToken = default)
        {
            var query = $@"INSERT INTO entregador
                           (nome, cnpj, datanascimento, numerocnh, tipocnh)
	                       VALUES 
                           (@Nome, @Cnpj, @Datanascimento, @Numerocnh, @tipocnh)";

            await _repository.Execute(
                sql: query,
                param: new
                {
                    Nome = entregador.Nome,
                    Cnpj = entregador.Cnpj,
                    Datanascimento = entregador.DataNascimento,
                    Numerocnh = entregador.Cnh,
                    tipocnh = entregador.TipoCnh
                },
                cancellationToken: cancellationToken);
        }

        public async Task<Entregador> GetByCnh(string cnh, CancellationToken cancellationToken = default)
        {
            try
            {
                var query = $@"SELECT  identificador as Codigo, 
                                       nome as Nome, 
                                       cnpj as Cnpj, 
                                       datanascimento as DataNascimento, 
                                       numerocnh as Cnh,
                                       tipocnh as TipoCnh,
                                       imagemcnh as ImagemCnh
                                FROM   ENTREGADOR
                                WHERE  numerocnh = @Cnh";

                var entregador = await _repository.QueryFirstOrDefault<Entregador>(
                    sql: query,
                    param: new { Cnh = cnh },
                    cancellationToken: cancellationToken);

                return entregador;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<Entregador> GetByCnpj(string cnpj, CancellationToken cancellationToken = default)
        {
            try
            {
                var query = $@"SELECT  identificador as Codigo, 
                                   nome as Nome, 
                                   cnpj as Cnpj, 
                                   datanascimento as DataNascimento, 
                                   numerocnh as Cnh,
                                   tipocnh as TipoCnh,
                                   imagemcnh as ImagemCnh
                            FROM   ENTREGADOR
                            WHERE  cnpj = @Cnpj";

                var entregador = await _repository.QueryFirstOrDefault<Entregador>(
                    sql: query,
                    param: new { Cnpj = cnpj },
                    cancellationToken: cancellationToken);

                return entregador;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<Entregador> GetByIdentificador(int identificador, CancellationToken cancellationToken = default)
        {
            var query = $@"SELECT  identificador as Codigo, 
                                   nome as Nome, 
                                   cnpj as Cnpj, 
                                   datanascimento as DataNascimento, 
                                   numerocnh as Cnh,
                                   tipocnh as TipoCnh,
                                   imagemcnh as ImagemCnh
                            FROM   ENTREGADOR
                            WHERE  identificador = @Identificador";

            var entregador = await _repository.QueryFirstOrDefault<Entregador>(
                sql: query,
                param: new { Identificador = identificador },
                cancellationToken: cancellationToken);

            return entregador;
        }

        public async Task UpdateCnh(Entregador entregador, CancellationToken cancellationToken = default)
        {
            var query = $@"UPDATE ENTREGADOR 
                           SET    IMAGEMCNH = @CaminhoImagem
                           WHERE  IDENTIFICADOR = @Identificador";

            await _repository.Execute(
               sql: query,
               param: new
               {
                   Identificador = entregador.Codigo,
                   CaminhoImagem = entregador.ImagemCnh
               },
               cancellationToken: cancellationToken);

        }

    }
}
