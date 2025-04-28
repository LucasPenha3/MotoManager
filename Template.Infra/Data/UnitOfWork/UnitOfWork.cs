using MotoManager.Domain.Interfaces.UnitOfWork;
using MotoManager.Infra.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoManager.Infra.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BaseConnection _baseConnection;

        public UnitOfWork(BaseConnection baseConnection)
        {
            _baseConnection = baseConnection;
        }

        public async Task Commit(CancellationToken cancellationToken = default)
        {
            try
            {
                await _baseConnection.Commit(cancellationToken);
            }
            catch 
            {
                await _baseConnection.Rollback(cancellationToken);
            }
        }

        public void Dispose()
        {
            _baseConnection.Dispose();
        }

        ~UnitOfWork()
        {
            Dispose();
        }
    }
}
