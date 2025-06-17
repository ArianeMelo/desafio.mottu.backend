namespace Mottu.Locacao.Motos.Domain.Interface.Strategy
{
    public interface ILocacaoStrategy
    {
        public decimal CalcularValorDiarias(int diarias);
        public decimal CalcularValorPlano();
    }
}
