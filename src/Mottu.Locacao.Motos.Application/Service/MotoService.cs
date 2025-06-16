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
        private readonly IRabbitService _rabbitService;
        private readonly NotificacaoDominioHandler _notificationHandler;

        public MotoService(IMotoRepository motoRepository,
            IRabbitService rabbitService,
            NotificacaoDominioHandler dominioHandler)
        {
            _motoRepository = motoRepository;
            _rabbitService = rabbitService;
            _notificationHandler = dominioHandler;
        }

        public async Task Inserir(MotoDto motoDto, CancellationToken cancellation)
        {
            var placa = await _motoRepository.ObterPlaca(motoDto.Placa, cancellation);

            if (!string.IsNullOrWhiteSpace(placa))
            {
                _notificationHandler.AdicionarNotificacao(
                   "MotoService-Inserir", string.Format("Já existe um registro com a placa informada {}", placa));
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
                _notificationHandler.AdicionarNotificacao("MotoService ObterPorPlaca", string.Format("Nenhum registro encontrado para a placa informada {0}", moto));
                return null;
            }

            return moto!.ParaMotoDto();
        }

        public async Task<MotoDto?> ObterPorIdentificador(string identificador, CancellationToken cancellation)
        {
            var moto = await _motoRepository.ObterPorIdentificador(identificador, cancellation);

            if (moto is null)
                return default;

            return moto.ParaMotoDto();
        }

        public async Task<bool> AlterarPlaca(string placa, string identificador, CancellationToken cancellation)
        {
            var registro = await _motoRepository.ObterPorIdentificador(identificador, cancellation);

            if (registro is null)
            {
                _notificationHandler.AdicionarNotificacao("MotoService-AlterarPlaca",
                    string.Format("Registro não encontrado para o id {0} informado", identificador));

                return false;
            }

            return await _motoRepository.AlterarPlaca(placa, identificador, cancellation);
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

            await _motoRepository.InserirMotoAnoEspecifico(placa, ano, cancellation);
        }

        #endregion


    }
}
