using Mottu.Locacao.Motos.Domain.Dtos;

namespace Mottu.Locacao.Motos.Domain.Interface.Service
{
    internal interface IEntregadorService
    {
        public void CriarCadastro(EntregadorDto entregadorDto);
        public void AlterarFoto(string id, string imagemCnh);
    }
}
