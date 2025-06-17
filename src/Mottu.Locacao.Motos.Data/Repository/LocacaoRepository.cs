using Dapper;
using Mottu.Locacao.Motos.Data.Queries;
using Mottu.Locacao.Motos.Domain.Entities;
using Mottu.Locacao.Motos.Domain.Interface.Repository;
using System.Data;

namespace Mottu.Locacao.Motos.Data.Repository
{
    public class LocacaoRepository : BaseRepository, ILocacaoRepository
    {
        public LocacaoRepository(IDbConnection connection) : base(connection)
        { }
        public async Task Inserir(LocacaoEntity locacao, CancellationToken cancellation)
        {
            var param = new DynamicParameters();
            param.Add(@"Id", locacao.Id, DbType.Guid);
            param.Add(@"EntregadorId", locacao.EntregadorId, DbType.AnsiString);
            param.Add(@"MotoId", locacao.MotoId, DbType.AnsiString);
            param.Add(@"ValorDiaria", locacao.ValorDiaria, DbType.Decimal);
            param.Add(@"DataInicio", locacao.DataInicio, DbType.DateTime);
            param.Add(@"DataEncerramento", locacao.DataTermino, DbType.DateTime);
            param.Add(@"DataPrevista", locacao.DataPrevistaEncerramento, DbType.DateTime);
            param.Add(@"PlanoLocacao", locacao.PlanoLocacao, DbType.Int32);

            await base.Inserir(LocacaoQuery.Inserir, param, cancellation);
        }

        public async Task<LocacaoEntity?> ObterLocacaoCompletaPorIdEntregador(string entregadorId, CancellationToken cancellation)
        {
            var param = new DynamicParameters();
            param.Add("@EntregadorId", entregadorId, DbType.AnsiString);

            return await base.ObterPorFiltro<LocacaoEntity?>(LocacaoQuery.ObterLocacao, param, cancellation);
        }

        public async Task<LocacaoEntity?> ObterLocacaoPorIdEntregador(string entregadorId, CancellationToken cancellation)
        {
            var param = new DynamicParameters();
            param.Add("@EntregadorId", entregadorId, DbType.AnsiString);

            return await base.ObterPorFiltro<LocacaoEntity?>(LocacaoQuery.ObterPorId, param, cancellation);
        }

        public async Task<LocacaoEntity?> ObterPorMotoId(string motoId, CancellationToken cancellation)
        {
            var param = new DynamicParameters();
            param.Add("@MotoId", motoId, DbType.AnsiString);

            return await base.ObterPorFiltro<LocacaoEntity?>(LocacaoQuery.ObterPorMotoId, param, cancellation);
        }

        public async Task Atualizar(LocacaoEntity locacao, CancellationToken cancellation)
        {
            var param = new DynamicParameters();          
            param.Add(@"EntregadorId", locacao.EntregadorId, DbType.AnsiString);
            param.Add(@"DataDevolucao", locacao.DataDevolucao, DbType.DateTime);
            param.Add(@"ValorDiaria", locacao.ValorDiaria, DbType.Decimal);
            param.Add("ValorAtraso", locacao.ValorAtraso ?? (object)DBNull.Value, DbType.Decimal);
            param.Add("ValorAdiantamento", locacao.ValorAdiantamento ?? (object)DBNull.Value, DbType.Decimal);
            param.Add("ValorTotalLocacao", locacao.ValorTotalLocacao ?? (object)DBNull.Value, DbType.Decimal);

            var inserido = await base.Alterar<LocacaoEntity>(LocacaoQuery.Atualizar, param, cancellation);
        }
    }
}
