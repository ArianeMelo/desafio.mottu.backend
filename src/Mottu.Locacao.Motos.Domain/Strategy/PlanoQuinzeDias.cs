using Mottu.Locacao.Motos.Domain.Interface.Strategy;

namespace Mottu.Locacao.Motos.Domain.Strategy
{
    public class PlanoQuinzeDias : ILocacaoStrategy
    {
        private const decimal ValorDiaria = 28.00m;

        private const int DiasPlano = 15;
        public decimal CalcularValorDiarias(int diarias)
        {
            var diasRestantes = DiasPlano - diarias;
            return diasRestantes < 1 ? 0 : diasRestantes * ValorDiaria;
        }
        public decimal CalcularValorPlano()
            => DiasPlano * ValorDiaria;
    }
}
