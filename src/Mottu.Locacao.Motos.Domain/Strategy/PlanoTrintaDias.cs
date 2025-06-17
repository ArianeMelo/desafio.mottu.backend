using Mottu.Locacao.Motos.Domain.Entities;
using Mottu.Locacao.Motos.Domain.Interface.Strategy;

namespace Mottu.Locacao.Motos.Domain.Strategy
{
    public class PlanoTrintaDias : ILocacaoStrategy
    {
        public const decimal ValorDiaria = 22.00m;
        public const int DiasPlano = 30;
        public decimal CalcularValorDiarias(int diarias)
        {
            var diasRestantes = DiasPlano - diarias;
            return diasRestantes < 1 ? 0 : diasRestantes * ValorDiaria;
        }
        public decimal CalcularValorPlano()
            => DiasPlano * ValorDiaria;

    }
}


