using Mottu.Locacao.Motos.Domain.Dtos;

namespace Mottu.Locacao.Motos.Domain.Interface.Service
{
    public interface ILocacaoService
    {
        public void AlugarMoto(LocacaoDto locacaoDto);
        public void ConsultarLocacao(string id);
        public void InformarDataDevolucao(string id, DateTime dataDevolucao);

    }
}
