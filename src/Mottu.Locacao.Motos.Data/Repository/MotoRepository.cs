using Dapper;
using Mottu.Locacao.Motos.Data.Queries;
using Mottu.Locacao.Motos.Domain.Entities;
using Mottu.Locacao.Motos.Domain.Interface.Repository;
using System.Data;

namespace Mottu.Locacao.Motos.Data.Repository
{
    public class MotoRepository : BaseRepository, IMotoRepository
    {
        public MotoRepository(IDbConnection connection) : base(connection)
        { }

        public async Task Inserir(Moto moto, CancellationToken cancellation)
        {
            var param = new DynamicParameters();
            param.Add("@Id", moto.Id, DbType.Guid);
            param.Add("@Identificador", moto.Identificador, DbType.AnsiString);
            param.Add("@Ano", moto.Ano, DbType.Int16);
            param.Add("@Modelo", moto.Modelo, DbType.AnsiString);
            param.Add("@Placa", moto.Placa, DbType.AnsiString);

            await base.Inserir(MotoQuery.Inserir, param, cancellation);
        }

        public async Task<Moto?> ObterPorPlaca(string placa, CancellationToken cancellation)
        {
            var param = new DynamicParameters();
            param.Add("@Placa", placa, DbType.AnsiString);

            return await base.ObterPorFiltro<Moto?>(MotoQuery.ObterPorPlaca, param, cancellation);
        }

        public async Task<string?> ObterPlaca(string placa, CancellationToken cancellation)
        {
            var param = new DynamicParameters();
            param.Add("@Placa", placa, DbType.AnsiString);

            return await base.ObterPorFiltro<string?>(MotoQuery.ObterPlaca, param, cancellation);
        }

        public async Task<Moto?> ObterPorIdentificador(string identificador, CancellationToken cancellation)
        {
            var param = new DynamicParameters();
            param.Add("@Identificador", identificador, DbType.AnsiString);

            return await base.ObterPorFiltro<Moto?>(MotoQuery.ObterPorIdentificador, param, cancellation);
        }

        public async Task<bool> AlterarPlaca(string placa, string identificador, CancellationToken cancellation)
        {
            var param = new DynamicParameters();
            param.Add("@Identificador", identificador, DbType.AnsiString);
            param.Add("@Placa", placa, DbType.AnsiString);

            return await base.Alterar<Moto>(MotoQuery.AlterarPlaca, param, cancellation);
        }

        public async Task Remover(string identificador, CancellationToken cancellation)
        {
            var param = new DynamicParameters();
            param.Add("@Identificador", identificador, DbType.AnsiString);

            await base.Remover(MotoQuery.Remover, param, cancellation);
        }




        #region Regsitros Ano 2024

        public async Task<string?> ObterPlacaAnoEspecifico(string placa, CancellationToken cancellation)
        {
            var param = new DynamicParameters();
            param.Add("@Placa", placa, DbType.AnsiString);

            var registro = await base.ObterPorFiltro<string?>(MotoQuery.ObterPlaca2024, param, cancellation);

            return registro;
        }
        public async Task InserirPlaca(string placa, int ano, CancellationToken cancellation)
        {
            var param = new DynamicParameters();
            param.Add("@Id", Guid.NewGuid(), DbType.Guid);
            param.Add("@Ano", ano, DbType.Int16);
            param.Add("@Placa", placa, DbType.AnsiString);

            await base.Inserir(MotoQuery.InserirAno2024, param, cancellation);
        }

        #endregion
    }
}
