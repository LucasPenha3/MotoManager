using MotoManager.Domain.Domain;
using MotoManager.Domain.Interfaces.Repositories;

namespace MotoManager.Infra.Data.Repositories
{
    public class LocacaoRepository : ILocacaoRepository
    {
        private readonly GenericRepository _repository;
        private readonly IPlanoRepository _planoRepository;
        private readonly IEntregadorRepository _entregadorRepository;
        private readonly IMotoRespository _motoRespository;

        public LocacaoRepository(GenericRepository repository, IPlanoRepository planoRepository, IEntregadorRepository entregadorRepository, IMotoRespository motoRespository)
        {
            _repository = repository;
            _planoRepository = planoRepository;
            _entregadorRepository = entregadorRepository;
            _motoRespository = motoRespository;
        }

        public async Task<Locacao> GetLocacaoById(int id, CancellationToken cancellationToken = default)
        {
            var query = $@"SELECT  idEntregador as  CodigoEntregador,
                                   idMoto as CodigoMoto,
                                   plano as CodigoPlano,
	                               datainicio as DataInicio, 
	                               datatermino as DataTermino, 
	                               dataprevisaotermino as DataPrevisaoTermino, 
                                   id as Codigo,
	                               DataEntrega as DataDevolucao
                            FROM   LOCACOES
                            WHERE  id = @id";

            
            var locacao = await _repository.QueryFirstOrDefault<Locacao>(
                query,
                new { id = id }, 
                cancellationToken: cancellationToken);

            if (locacao == null)
                return null;

            locacao.SetMoto(await _motoRespository.GetMotoByIdentificador(locacao.CodigoMoto, cancellationToken));
            locacao.SetPlano(await _planoRepository.GetPlanoById(locacao.CodigoPlano, cancellationToken));
            locacao.SetEntregador(await _entregadorRepository.GetByIdentificador(locacao.CodigoEntregador, cancellationToken));

            return locacao;
        }

        public async Task Rent(Locacao locacao, CancellationToken cancellationToken = default)
        {
            var sql = $@"INSERT INTO locacoes
                        (identregador, idmoto, datainicio, datatermino, dataprevisaotermino, plano)
	                    VALUES 
                        (@Identregador, @Idmoto, @Datainicio, @Datatermino, @Dataprevisaotermino, @Plano);";

            await _repository.Execute(
                sql: sql,
                param: new
                {
                    Identregador = locacao.CodigoEntregador,
                    Idmoto = locacao.CodigoMoto,
                    Datainicio = locacao.DataInicio,
                    Datatermino = locacao.DataTermino,
                    Dataprevisaotermino = locacao.DataPrevisaoTermino,
                    Plano = locacao.CodigoPlano
                });
        }

        public async Task Return(Locacao locacao, CancellationToken cancellationToken = default)
        {
            var query = $@"UPDATE LOCACOES 
                           SET    DATAENTREGA = @DataEntrega
                            WHERE id = @Id";

            await _repository.Execute(
                sql: query,
                param: new
                {
                    Id = locacao.Codigo,
                    DataEntrega = locacao.DataDevolucao
                });
        }
    }
}
