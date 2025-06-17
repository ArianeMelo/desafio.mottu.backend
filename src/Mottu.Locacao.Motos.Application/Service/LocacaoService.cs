using Mottu.Locacao.Motos.Application.Extensions;
using Mottu.Locacao.Motos.Domain.Dtos;
using Mottu.Locacao.Motos.Domain.Enum;
using Mottu.Locacao.Motos.Domain.Interface.Repository;
using Mottu.Locacao.Motos.Domain.Interface.Service;
using Mottu.Locacao.Motos.Domain.Strategy;

namespace Mottu.Locacao.Motos.Application.Service
{
    public class LocacaoService : ILocacaoService
    {
        private readonly ILocacaoRepository _locacaoRepository;
        private readonly IEntregadorRepository _entregadorRepository;
        private readonly INotificacaoDominioHandler _notificationHandler;

        public LocacaoService(ILocacaoRepository locacaoRepository, IEntregadorRepository entregadorRepository,
            INotificacaoDominioHandler notificacaoDominioHandler)
        {
            _locacaoRepository = locacaoRepository;
            _entregadorRepository = entregadorRepository;
            _notificationHandler = notificacaoDominioHandler;
        }

        public async Task Inserir(LocacaoRequestDto locacaoDto, CancellationToken cancellationToken)
        {
            var locacaoObtida = await _locacaoRepository.ObterLocacaoPorIdEntregador(locacaoDto.EntregadorId, cancellationToken);

            if (locacaoObtida is not null)
            {
                _notificationHandler.AdicionarNotificacao("LocacaoService-InserirLocacao", "Já existe uma locação ativa para o entregador informado.");
                return;
            }

            var entregador = await _entregadorRepository.ObterEntregadorPorId(locacaoDto.EntregadorId, cancellationToken);

            if (entregador is null)
            {
                _notificationHandler.AdicionarNotificacao("LocacaoService-InserirLocacao", string.Format("CNH não encontrada para o entregador {0}", locacaoDto.EntregadorId));
                return;
            }

            if (!entregador!.CategoriaCnhA())
            {
                _notificationHandler.AdicionarNotificacao("LocacaoService-InserirLocacao", string.Format("Entregador não possui CNH correpondente {0}", locacaoDto.EntregadorId));
                return;
            }

            var locacao = locacaoDto.ParaDominio();

            await _locacaoRepository.Inserir(locacao, cancellationToken);
        }

        public async Task<LocacaoResponseDto?> ObterLocacao(string entregadorId, CancellationToken cancellationToken)
        {
            var locacao = await _locacaoRepository.ObterLocacaoCompletaPorIdEntregador(entregadorId, cancellationToken);

            if (locacao is null)
            {
                _notificationHandler.AdicionarNotificacao("LocacaoService-ObterLocacao", string.Format("Não existe locação {0}.", entregadorId));
                return null;
            }

            return locacao!.ParaResponseDto();
        }

        public async Task AtualizarLocacao(string entregadorId, DevolucaoDto dataDevolucao, CancellationToken cancellationToken)
        {
            var locacao = await _locacaoRepository.ObterLocacaoCompletaPorIdEntregador(entregadorId, cancellationToken);

            if (locacao is null)
            {
                _notificationHandler.AdicionarNotificacao("LocacaoService-ObterEncerramentoLocacao", "Não existe locação para o ID informado.");
                return;
            }

            locacao.InserirDataDevolucao(dataDevolucao.DataDevolucao);

            locacao.FazerCalculosDevolucao();

            await _locacaoRepository.Atualizar(locacao, cancellationToken);
        }
    }
}
