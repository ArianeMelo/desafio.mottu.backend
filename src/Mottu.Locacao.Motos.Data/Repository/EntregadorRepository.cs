using Dapper;
using Mottu.Locacao.Motos.Data.Queries;
using Mottu.Locacao.Motos.Domain.Entities;
using Mottu.Locacao.Motos.Domain.Interface.Repository;
using System.Data;

namespace Mottu.Locacao.Motos.Data.Repository
{
    public class EntregadorRepository : BaseRepository, IEntregadorRepository
    {
        public EntregadorRepository(IDbConnection connection) : base(connection)
        { }
        public async Task Inserir(Entregador entregador, CancellationToken cancellation)
        {
            var param = new DynamicParameters();
            param.Add("@Id", entregador.Id, DbType.Guid);
            param.Add("@Identificador", entregador.Identificador, DbType.String);
            param.Add("@Nome", entregador.Nome, DbType.AnsiString);
            param.Add("@Cnpj", entregador.Cnpj, DbType.AnsiString);
            param.Add("@DataNascimento", entregador.DataNascimento, DbType.DateTime);
            param.Add("@NumeroCnh", entregador.NumeroCnh, DbType.AnsiString);
            param.Add("@TipoCnh", entregador.TipoCnh, DbType.Int32);

            await base.Inserir(EntregadorQuery.Inserir, param, cancellation);
        }

        public async Task<Entregador?> ObterPorCnh(string numeroCnh, CancellationToken cancellation)
        {
            var param = new DynamicParameters();
            param.Add("@NumeroCnh", numeroCnh, DbType.AnsiString);

            return await base.ObterPorFiltro<Entregador?>(EntregadorQuery.ObterPorCnh, param, cancellation);
        }

        public async Task<Entregador?> ObterPorCnpj(string cnpj, CancellationToken cancellation)
        {
            var param = new DynamicParameters();
            param.Add("@cnpj", cnpj, DbType.AnsiString);

            return await base.ObterPorFiltro<Entregador?>(EntregadorQuery.ObterCnpj, param, cancellation);
        }

        public async Task<Entregador?> ObterEntregadorPorId(string identificador, CancellationToken cancellation)
        {
            var param = new DynamicParameters();
            param.Add("@Identificador", identificador, DbType.AnsiString);

            return await base.ObterPorFiltro<Entregador?>(EntregadorQuery.ObterTipoCnh, param, cancellation);
        }
    }
}
