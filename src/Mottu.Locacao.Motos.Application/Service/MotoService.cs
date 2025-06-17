using Mottu.Locacao.Motos.Application.Evento;
using Mottu.Locacao.Motos.Application.Extensions;
using Mottu.Locacao.Motos.Domain.Dtos;
using Mottu.Locacao.Motos.Domain.Interface.Application;
using Mottu.Locacao.Motos.Domain.Interface.Repository;
using Mottu.Locacao.Motos.Domain.Interface.Service;
using Mottu.Locacao.Motos.Domain.Notification;

namespace Mottu.Locacao.Motos.Application.Service
{
    public class MotoService : IMotoService
    {
        private readonly IMotoRepository _motoRepository;
        private readonly ILocacaoRepository _locacaoRepository;
        private readonly IRabbitService _rabbitService;
        private readonly INotificacaoDominioHandler _notificationHandler;

        public MotoService(IMotoRepository motoRepository,
            ILocacaoRepository locacaoRepository,
            IRabbitService rabbitService,
            INotificacaoDominioHandler dominioHandler)
        {
            _motoRepository = motoRepository;
            _locacaoRepository = locacaoRepository;
            _rabbitService = rabbitService;
            _notificationHandler = dominioHandler;
        }

        public async Task Inserir(MotoDto motoDto, CancellationToken cancellation)
        {
            var placa = await _motoRepository.ObterPlaca(motoDto.Placa, cancellation);

            if (!string.IsNullOrWhiteSpace(placa))
            {
                _notificationHandler.AdicionarNotificacao(
                   "MotoService-Inserir", string.Format("Já existe um registro com a placa informada {0}", placa));
                return;
            }

            var moto = motoDto.ParaMoto();

            await _motoRepository.Inserir(moto, cancellation);

            if (moto.PodePostarNaFila())
                await _rabbitService.PostarMenssagem(new MotoCadastradaEvent(motoDto.Ano, motoDto.Placa));
        }

        public async Task<MotoDto?> ObterPorPlaca(string placa, CancellationToken cancellation)
        {
            var moto = await _motoRepository.ObterPorPlaca(placa, cancellation);

            if (moto is null)
            {
                _notificationHandler.AdicionarNotificacao("MotoService ObterPorPlaca", string.Format("Nenhum registro encontrado para a placa informada {0}", placa));
                return null;
            }

            return moto!.ParaMotoDto();
        }

        public async Task<MotoDto?> ObterPorIdentificador(string identificador, CancellationToken cancellation)
        {
            var moto = await _motoRepository.ObterPorIdentificador(identificador, cancellation);

            if (moto is null)
            {
                _notificationHandler.AdicionarNotificacao("MotoService ObterPorIdentificador", string.Format("Nenhum registro encontrado para a Identificador informado {0}", identificador));
                return null;
            }

            return moto.ParaMotoDto();
        }

        public async Task<bool> AlterarPlaca(string placa, string identificador, CancellationToken cancellation)
        {
            var moto = await _motoRepository.ObterPorIdentificador(identificador, cancellation);

            if (moto is null)
            {
                _notificationHandler.AdicionarNotificacao("MotoService-AlterarPlaca",
                    string.Format("Registro não encontrado para o id {0} informado", identificador));

                return false;
            }

            return await _motoRepository.AlterarPlaca(placa, identificador, cancellation);
        }

        public async Task Remover(string motoId, CancellationToken cancellation)
        {
            var locacao = await _locacaoRepository.ObterPorMotoId(motoId, cancellation);

            if (locacao is not null)
            {
                _notificationHandler.AdicionarNotificacao("MotoService-Remover",
                    string.Format("Não é possível remover pois existem locações para este registro {0}", motoId));
                return;
            }

            await _motoRepository.Remover(motoId, cancellation);
        }

        #region Registros Moto Ano2024
        public async Task InserirAnoEspecifico(string placa, int ano, CancellationToken cancellation)
        {
            var registroPlaca = await _motoRepository.ObterPlacaAnoEspecifico(placa, cancellation);
            if (!string.IsNullOrWhiteSpace(registroPlaca))
            {
                _notificationHandler.AdicionarNotificacao("MotoService-InserirDado Consumer", "Já existe um registro com a placa informada");
                return;
            }

            await _motoRepository.InserirPlaca(placa, ano, cancellation);
        }

        #endregion


    }
}
