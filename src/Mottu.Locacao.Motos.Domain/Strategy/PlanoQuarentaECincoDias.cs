using Mottu.Locacao.Motos.Domain.Interface.Strategy;

namespace Mottu.Locacao.Motos.Domain.Strategy
{
    public class PlanoQuarentaECincoDias : ILocacaoStrategy
    {
        private const decimal ValorDiaria = 20.00m;
        private const int DiasPlano = 45;
        public decimal CalcularValorDiarias(int diarias)
        {
            var diasRestantes = DiasPlano - diarias;
            return diasRestantes < 1 ? 0 : diasRestantes * ValorDiaria;
        }
        public decimal CalcularValorPlano()
            => DiasPlano * ValorDiaria;
    }
}
