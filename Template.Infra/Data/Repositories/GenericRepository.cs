using Dapper;
using System.Data;

namespace MotoManager.Infra.Data.Repositories
{
    public sealed class GenericRepository 
    {
        private readonly BaseConnection _connection;

        public GenericRepository(BaseConnection connection)
        {
            _connection = connection;
        }

        public IDbConnection DbConnection => _connection.DbConnection;
        public IDbTransaction DbTransaction => _connection.DbTransaction;

        public async Task Execute(
            string sql, 
            object param = null, 
            CommandType commandType = CommandType.Text,
            CancellationToken cancellationToken = default)
        {
            var commandDefinition = new CommandDefinition(
                commandText: sql,
                parameters: param,
                transaction: DbTransaction,
                commandType: commandType,
                cancellationToken: cancellationToken);

            await DbConnection.ExecuteAsync(commandDefinition);
        }

        public async Task<T> QueryFirstOrDefault<T>(
            string sql,
            object param = null,
            CommandType commandType = CommandType.Text,
            CancellationToken cancellationToken = default)
        {
            var commandDefinition = new CommandDefinition(
                commandText: sql,
                parameters: param,
                transaction: DbTransaction,
                commandType: commandType,
                cancellationToken: cancellationToken);

            return await DbConnection.QueryFirstOrDefaultAsync<T>(commandDefinition);
        }

        public async Task<SqlMapper.GridReader> QueryMultiple(
            string sql,
            object param = null,
            CommandType commandType = CommandType.Text,
            CancellationToken cancellationToken = default)
        {
            var commandDefinition = new CommandDefinition(
                commandText: sql,
                parameters: param,
                transaction: DbTransaction,
                commandType: commandType,
                cancellationToken: cancellationToken);

            return await DbConnection.QueryMultipleAsync(commandDefinition);
        }

        public async Task<IEnumerable<T>> Query<T>(
            string sql,
            object param = null,
            CommandType commandType = CommandType.Text,
            CancellationToken cancellationToken = default)
        {
            var commandDefinition = new CommandDefinition(
                commandText: sql,
                parameters: param,
                transaction: DbTransaction,
                commandType: commandType,
                cancellationToken: cancellationToken);

            return await DbConnection.QueryAsync<T>(commandDefinition);
        }
    }
}
