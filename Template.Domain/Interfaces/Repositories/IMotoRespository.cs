using MotoManager.Domain.Domain;

namespace MotoManager.Domain.Interfaces.Repositories
{
    public interface IMotoRespository
    {
        Task CreateMoto(Moto moto, CancellationToken cancellationToken = default);
        Task UpdateMoto(Moto moto, CancellationToken cancellationToken = default);
        Task DeleteMoto(string identificador, CancellationToken cancellationToken = default);
        Task<Moto> GetMotoByIdentificador(string identificador, CancellationToken cancellationToken = default);
        Task<List<Moto>> GetMotosByPlaca(string placa, CancellationToken cancellationToken = default);
    }
}
