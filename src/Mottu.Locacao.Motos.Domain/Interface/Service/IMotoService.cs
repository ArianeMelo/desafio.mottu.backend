using Mottu.Locacao.Motos.Domain.Dtos;

namespace Mottu.Locacao.Motos.Domain.Interface.Application
{
    public interface IMotoService
    {
        public Task Inserir(MotoDto moto, CancellationToken cancellation);
        public Task<MotoDto?> ObterPorPlaca(string placa, CancellationToken cancellation);
        public Task<bool> AlterarPlaca(string placa, string id, CancellationToken cancellation);
        Task<MotoDto?> ObterPorIdentificador(string identificador, CancellationToken cancellation);

        Task InserirAnoEspecifico(string placa, int ano, CancellationToken cancellation);
    }
}
