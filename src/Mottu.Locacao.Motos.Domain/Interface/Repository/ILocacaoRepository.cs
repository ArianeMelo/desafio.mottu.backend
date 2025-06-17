using Mottu.Locacao.Motos.Domain.Entities;

namespace Mottu.Locacao.Motos.Domain.Interface.Repository
{
    public interface ILocacaoRepository
    {
        public Task Inserir(LocacaoEntity locacao, CancellationToken cancellationToken);
        public Task<LocacaoEntity?> ObterLocacaoCompletaPorIdEntregador(string entregadorId, CancellationToken cancellation);
        public Task<LocacaoEntity?> ObterLocacaoPorIdEntregador(string identificador, CancellationToken cancellationToken);
        public Task<LocacaoEntity?> ObterPorMotoId (string identificador, CancellationToken cancellationToken);
        public Task Atualizar (LocacaoEntity locacaoEntity, CancellationToken cancellationToken);      
    }
}
