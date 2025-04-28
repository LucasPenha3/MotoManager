namespace MotoManager.Domain.Interfaces.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task Commit(CancellationToken cancellationToken = default);
    }
}
