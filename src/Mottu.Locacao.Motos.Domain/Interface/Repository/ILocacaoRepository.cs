using Mottu.Locacao.Motos.Domain.Entities;

namespace Mottu.Locacao.Motos.Domain.Interface.Repository
{
    public interface ILocacaoRepository
    {
        public Task InserirLocacao(LocacaoEntity locacao, CancellationToken cancellationToken);
        Task<LocacaoEntity?> ObterLocacao(string entregadorId, CancellationToken cancellation);
        public Task<LocacaoEntity?> ObterPorId(string identificador, CancellationToken cancellationToken);
        public Task<LocacaoEntity?> ObterPorMotoId (string identificador, CancellationToken cancellationToken);
        public Task Atualizar (LocacaoEntity locacaoEntity, CancellationToken cancellationToken);      
    }
}
