using MotoManager.Domain.Dtos;

namespace MotoManager.Domain.Interfaces.Queries
{
    public interface ILocacaoQuery
    {
        Task<List<LocacaoDto>> GetLocacoes(CancellationToken cancellationToken = default);
    }
}
