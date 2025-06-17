using Mottu.Locacao.Motos.Domain.Dtos;

namespace Mottu.Locacao.Motos.Domain.Interface.Service
{
    public interface ILocacaoService
    {
        public Task Inserir(LocacaoRequestDto locacaoDto, CancellationToken cancellationToken);
        public Task<LocacaoResponseDto> ObterLocacao(string entregadorId, CancellationToken cancellationToken);
        public Task AtualizarLocacao(string id, DevolucaoDto dataDevolução, CancellationToken cancellationToken);

    }
}
