using MotoManager.Domain.Domain;

namespace MotoManager.Domain.Interfaces.Queries
{
    public interface IPlanoQuery
    {
        Task<List<Plano>> GetPlanosById(int[] ids, CancellationToken cancellationToken = default);
    }
}
