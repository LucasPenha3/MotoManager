using MotoManager.Domain.Domain;

namespace MotoManager.Domain.Interfaces.Repositories
{
    public interface IPlanoRepository
    {
        Task<Plano> GetPlanoById(int id, CancellationToken cancellationToken = default);
    }
}
