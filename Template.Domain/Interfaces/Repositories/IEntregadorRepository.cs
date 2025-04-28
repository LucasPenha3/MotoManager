using MotoManager.Domain.Domain;

namespace MotoManager.Domain.Interfaces.Repositories
{
    public interface IEntregadorRepository
    {
        Task Create(Entregador entregador, CancellationToken cancellationToken = default);
        Task UpdateCnh(Entregador entregador, CancellationToken cancellationToken = default);

        Task<Entregador> GetByCnpj(string Cnpj, CancellationToken cancellationToken = default);
        Task<Entregador> GetByCnh(string Cnh, CancellationToken cancellationToken = default);
        Task<Entregador> GetByIdentificador(int identificador, CancellationToken cancellationToken = default);
    }
}
