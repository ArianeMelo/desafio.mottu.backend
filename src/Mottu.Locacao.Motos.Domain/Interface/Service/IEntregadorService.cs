using Mottu.Locacao.Motos.Domain.Dtos;

namespace Mottu.Locacao.Motos.Domain.Interface.Service
{
    public interface IEntregadorService
    {
        public Task Inserir(EntregadorDto entregadorDto, CancellationToken cancellationToken);      
    }
}
