using Dapper;
using Npgsql;
using System.Data;

namespace Mottu.Locacao.Motos.Domain.Interface.Repository
{
    public abstract class BaseRepository(IDbConnection connection)
    {
        private readonly IDbConnection _connection = connection; 
        public async Task<bool> Inserir(string sql, DynamicParameters? parameters, CancellationToken cancellation)
        {
            using (var conn = new NpgsqlConnection(_connection.ConnectionString))
            {
                var inserido = await conn
                    .ExecuteAsync(sql, parameters)
                    .WaitAsync(cancellation);

                return inserido > 0;
            }
        }

        public async Task<T?> ObterPorFiltro<T>(string sql, DynamicParameters? parameters, CancellationToken cancellation)
        {
            using (var conn = new NpgsqlConnection(_connection.ConnectionString))
            {
                return await conn
                     .QueryFirstOrDefaultAsync<T?>(sql, parameters)
                     .WaitAsync(cancellation);
            }
        }

        public async Task<bool> Alterar<T>(string sql, DynamicParameters? parameters, CancellationToken cancellation)
        {
            using (var conn = new NpgsqlConnection(_connection.ConnectionString))
            {
                var alterado = await conn
                    .ExecuteAsync(sql, parameters)
                    .WaitAsync(cancellation);

                return alterado > 0;
            }
        }
    }
}
