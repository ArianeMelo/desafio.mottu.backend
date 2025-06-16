using Dapper;
using Mottu.Locacao.Motos.Data.Queries;
using Mottu.Locacao.Motos.Domain.Dtos;
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
            param.Add("Id", moto.Id, DbType.Guid);
            param.Add(@"identificador", moto.Identificador, DbType.AnsiString);
            param.Add(@"ano", moto.Ano, DbType.Int16);
            param.Add(@"modelo", moto.Modelo, DbType.AnsiString);
            param.Add(@"placa", moto.Placa, DbType.AnsiString);

            await base.Inserir(MotoQuery.Inserir, param, cancellation);
        }

        public async Task<Moto?> ObterMotoPorPlaca(string placa, CancellationToken cancellation)
        {
            var param = new DynamicParameters();
            param.Add(@"placa", placa, DbType.AnsiString);

            return await base.ObterPorFiltro<Moto?>(MotoQuery.ObterPorPlaca, param, cancellation);
        }

        public async Task<Moto?> ObterPorPlaca(string placa, CancellationToken cancellation)
        {
            var param = new DynamicParameters();
            param.Add(@"placa", placa, DbType.AnsiString);

            return await base.ObterPorFiltro<Moto?>(MotoQuery.ObterPorPlaca, param, cancellation);
        }

        public async Task<string?> ObterPlaca(string placa, CancellationToken cancellation)
        {
            var param = new DynamicParameters();
            param.Add(@"placa", placa, DbType.AnsiString);

            return await base.ObterPorFiltro<string?>(MotoQuery.ObterPlaca, param, cancellation);
        }

        public async Task<Moto?> ObterPorIdentificador(string identificador, CancellationToken cancellation)
        {
            var param = new DynamicParameters();
            param.Add(@"identificador", identificador, DbType.AnsiString);

            return await base.ObterPorFiltro<Moto?>(MotoQuery.ObterPorIdentificador, param, cancellation);
        }

        public async Task<bool> AlterarPlaca(string placa, string identificador, CancellationToken cancellation)
        {
            var param = new DynamicParameters();
            param.Add(@"identificador", identificador, DbType.AnsiString);
            param.Add(@"placa", placa, DbType.AnsiString);

            return await base.Alterar<Moto>(MotoQuery.AlterarPlaca, param, cancellation);
        }




        #region Regsitros Ano 2024

        public async Task<string?> ObterPlacaAnoEspecifico(string placa, CancellationToken cancellation)
        {
            var param = new DynamicParameters();
            param.Add(@"placa", placa, DbType.AnsiString);

            var registro = await base.ObterPorFiltro<string?>(MotoQuery.ObterPlaca2024, param, cancellation);

            return registro;
        }
        public async Task InserirMotoAnoEspecifico(string placa, int ano, CancellationToken cancellation)
        {
            var param = new DynamicParameters();
            param.Add("@Id", Guid.NewGuid(), DbType.Guid);
            param.Add(@"ano", ano, DbType.Int16);
            param.Add(@"placa", placa, DbType.AnsiString);

            await base.Inserir(MotoQuery.InserirAno2024, param, cancellation);
        }

        #endregion
    }
}
