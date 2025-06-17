using Mottu.Locacao.Motos.Domain.Enum;
using Mottu.Locacao.Motos.Domain.Strategy;

namespace Mottu.Locacao.Motos.Domain.Entities
{
    public class LocacaoEntity : Entity
    {
        public string EntregadorId { get; set; }
        public string MotoId { get; set; }
        public decimal? ValorDiaria { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
        public DateTime DataPrevistaEncerramento { get; set; }
        public DateTime? DataDevolucao { get; set; }
        public int PlanoLocacao { get; set; }
        public decimal? ValorAdiantamento { get; set; }
        public decimal? ValorAtraso { get; set; }
        public decimal? ValorTotalLocacao { get; set; }

        public LocacaoEntity(string entregadorId, string motoId,
            decimal? valorDiaria, DateTime dataInicio, DateTime dataEncerramento,
            DateTime dataPrevistaEncerramento, DateTime? dataDevolucao, int planoLocacao)
        {
            EntregadorId = entregadorId;
            MotoId = motoId;
            ValorDiaria = valorDiaria;
            DataInicio = dataInicio;
            DataTermino = dataEncerramento;
            DataPrevistaEncerramento = dataPrevistaEncerramento;
            DataDevolucao = dataDevolucao;
            PlanoLocacao = planoLocacao;

            InicioLocacao();
        }
        public LocacaoEntity()
        { }

        public void InicioLocacao()
        {
            var diasParaIniciarLocacao = 1;

            DataInicio = DataInicio.AddDays(diasParaIniciarLocacao);
        }

        public void InserirDataDevolucao(DateTime? dataDevolucao)
           => DataDevolucao = dataDevolucao;

        public void FazerCalculosDevolucao()
        {
            if (AplicarRegraPorAdiantamento())
            {
                ValorDiaria = LocacaoStrategyFactory
                    .CriarPlano(PlanoLocacao)
                    .CalcularValorDiarias(CalcularDiasDevolucaoAntecipada());

                CalcularValorDevidoDevolucaoAntecipada();
                return;
            }

            ValorDiaria = LocacaoStrategyFactory
               .CriarPlano(PlanoLocacao)
               .CalcularValorPlano();

            if (AplicarRegraPorAtraso())
            {
                CalcularValorDevidoDevolucaoEmAtraso();
                PreenchervalorTotalLocacaoComAtraso();
                return;
            }

            PreencherValorTotalLocacaoSemAdicionais();
        }

        public bool AplicarRegraPorAdiantamento() =>
            DataDevolucao?.Date < DataPrevistaEncerramento.Date;

        public bool AplicarRegraPorAtraso() =>
            DataDevolucao?.Date > DataPrevistaEncerramento.Date;

        public void CalcularValorDevidoDevolucaoAntecipada()
        {
            if (PlanoLocacao == PlanosLocacao.Plano7Dias.GetHashCode())
                CalcularValorPlano7PorAntecipacao();

            else if (PlanoLocacao == PlanosLocacao.Plano15Dias.GetHashCode())
                CalcularValorPlano15PorAntecipacao();

            PreencherValorTotalLocacaoComAntecipacao();
        }
        public void CalcularValorPlano7PorAntecipacao()
        {
            decimal valorDiaria = 30;
            decimal valorMulta = 0.20m;

            var diasAdiantado = CalcularDiasDevolucaoAntecipada();
            if (diasAdiantado > 0)
                ValorAdiantamento = diasAdiantado * valorDiaria * valorMulta;
        }
        public void CalcularValorPlano15PorAntecipacao()
        {
            decimal valorDiaria = 28m;
            decimal valorMulta = 0.40m;

            var diasAdiantado = CalcularDiasDevolucaoAntecipada();
            if (diasAdiantado > 0)
                ValorAdiantamento = diasAdiantado * valorDiaria * valorMulta;
        }
        public void CalcularValorDevidoDevolucaoEmAtraso()
        {
            var diasAtrasados = CalcularDiasAtraso();
            if (diasAtrasados > 0)
                ValorAtraso = diasAtrasados * 50m;

            PreenchervalorTotalLocacaoComAtraso();
        }

        public int CalcularDiasDevolucaoAntecipada() =>
            DataDevolucao.HasValue && DataDevolucao.Value.Date < DataPrevistaEncerramento.Date
            ? (DataPrevistaEncerramento.Date - DataDevolucao.Value.Date).Days
            : 0;

        public int CalcularDiasAtraso() =>
            DataDevolucao.HasValue && DataDevolucao.Value.Date > DataPrevistaEncerramento.Date
            ? (DataDevolucao.Value.Date - DataPrevistaEncerramento.Date).Days
            : 0;

        public void PreencherValorTotalLocacaoComAntecipacao()
            => ValorTotalLocacao = ValorDiaria + ValorAdiantamento;
        public void PreenchervalorTotalLocacaoComAtraso()
            => ValorTotalLocacao = ValorDiaria + ValorAtraso;
        public void PreencherValorTotalLocacaoSemAdicionais()
            => ValorTotalLocacao = ValorDiaria;
    }
}
