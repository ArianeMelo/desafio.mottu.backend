using Newtonsoft.Json;

namespace Mottu.Locacao.Motos.Domain.Dtos
{
    public class LocacaoResponseDto
    {
        [JsonProperty("entregador_id")]
        public string? EntregadorId { get; set; } = "";

        [JsonProperty("moto_id")]
        public string? MotoId { get; set; } = "";

        [JsonProperty("valor_diaria")]
        public decimal? ValorDiaria { get; set; }

        [JsonProperty("data_inicio")]
        public DateTime DataInicio { get; set; }

        [JsonProperty("data_encerramento")]
        public DateTime DataEncerramento { get; set; }

        [JsonProperty("data_previsao_encerramento")]
        public DateTime DataPrevistaEncerramento { get; set; }

        [JsonProperty("data_devolucao", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? DataDevolucao { get; set; }   

        [JsonProperty("valor_devolucao_adiantado", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? ValorPorAdiantamento { get; set; }

        [JsonProperty("valor_devolucao_atrasado", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? ValorAtraso { get; set; }

        [JsonProperty("valor_total_locacao", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? ValorTotalLocacao { get; set; }
    }   
}