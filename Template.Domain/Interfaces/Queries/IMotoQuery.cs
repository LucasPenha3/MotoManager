using MotoManager.Domain.Dtos;

namespace MotoManager.Domain.Interfaces.Queries
{
    public interface IMotoQuery
    {
        Task<MotoDto> GetMotoById(string codigoMoto, CancellationToken cancellationToken = default);
        Task<List<MotoDto>> GetMotos(CancellationToken cancellationToken = default);

        Task<List<MotoDto>> GetMotosByid(string[] codigosMoto, CancellationToken cancellationToken = default);
    }
}
