using Mottu.Locacao.Motos.Domain.Enum;
using Mottu.Locacao.Motos.Domain.Interface.Strategy;

namespace Mottu.Locacao.Motos.Domain.Strategy
{
    public static class LocacaoStrategyFactory
    {
        public static ILocacaoStrategy CriarPlano(int numeroPLano)
        {
            switch (numeroPLano)
            {
                case (int)PlanosLocacao.Plano7Dias:
                    return new PlanoSeteDias();
                case (int)PlanosLocacao.Plano15Dias:
                    return new PlanoQuinzeDias();
                case (int)PlanosLocacao.Plano30Dias:
                    return new PlanoTrintaDias();
                case (int)PlanosLocacao.Plano45Dias:
                    return new PlanoQuarentaECincoDias();
                case (int)PlanosLocacao.Plano50Dias:
                    return new PlanoCinquentaDias();
                default:
                    throw new ArgumentException($"Plano {numeroPLano} não definido.");
            }
        }

    }
}
