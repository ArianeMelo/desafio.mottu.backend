using Mottu.Locacao.Motos.Domain.Entities;

namespace Mottu.Locacao.Motos.Domain.Interface.Repository
{
    public interface IEntregadorRepository
    {
        public Task Inserir(Entregador entregador, CancellationToken cancellation);
        public Task<Entregador?> ObterPorCnpj(string cnpj, CancellationToken cancellation);
        Task<Entregador?> ObterPorCnh(string cnh, CancellationToken cancellation);
        Task<Entregador?> ObterEntregadorPorId(string identificadorEntregador, CancellationToken cancellation);
    }
}
