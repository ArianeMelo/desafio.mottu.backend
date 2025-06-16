using Mottu.Locacao.Motos.Domain.Entities;

namespace Mottu.Locacao.Motos.Domain.Interface.Repository
{
    public interface IMotoRepository
    {
        public Task Inserir(Moto moto, CancellationToken cancellation);
        public Task<Moto?> ObterPorPlaca(string placa, CancellationToken cancellation);
        public Task<string?> ObterPlaca(string placa, CancellationToken cancellation);
        Task<Moto?> ObterPorIdentificador(string identificador, CancellationToken cancellation);
        public Task<bool> AlterarPlaca(string placa, string identificador, CancellationToken cancellation);

        #region Ano2024
        Task<string?> ObterPlacaAnoEspecifico(string placa, CancellationToken cancellation);
        Task InserirMotoAnoEspecifico(string placa, int ano, CancellationToken cancellation);
        #endregion

    }
}
