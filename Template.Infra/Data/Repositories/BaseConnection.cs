using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace MotoManager.Infra.Data.Repositories
{
    public sealed class BaseConnection : IDisposable
    {
        private readonly string _connectionString;

        public BaseConnection(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("connectionString");
        }

        private IDbConnection dbConnection;

        public IDbConnection DbConnection
        {
            get 
            {
                TryOpenConnection();
                return dbConnection; 
            }
        }

        private IDbTransaction dbTransaction;
        public IDbTransaction DbTransaction
        { 
            get 
            {
                TryOpenConnection();
                return dbTransaction; 
            }
        }

        private void TryOpenConnection()
        {
            if(dbConnection == null && !string.IsNullOrEmpty(_connectionString))
            {
                dbConnection = new NpgsqlConnection(_connectionString);
                dbConnection.Open();

                dbTransaction = DbConnection.BeginTransaction();
            }
        }

        public void Dispose()
        {
            DbConnection?.Dispose();
            DbTransaction?.Dispose();
        }

        ~BaseConnection()
        {
            Dispose();
        }

        public async Task Commit(CancellationToken cancellationToken = default)
            => await Task.Run(() => DbTransaction?.Commit(), cancellationToken);

        public async Task Rollback(CancellationToken cancellationToken = default)
            => await Task.Run(() => DbTransaction?.Rollback(), cancellationToken);
    }
}
