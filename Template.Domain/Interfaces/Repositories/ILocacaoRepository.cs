using MotoManager.Domain.Domain;

namespace MotoManager.Domain.Interfaces.Repositories
{
    public interface ILocacaoRepository
    {
        Task Rent(Locacao locacao, CancellationToken cancellationToken = default);
        Task Return(Locacao locacao, CancellationToken cancellationToken = default);
        Task<Locacao> GetLocacaoById(int id, CancellationToken cancellationToken = default);
    }
}
