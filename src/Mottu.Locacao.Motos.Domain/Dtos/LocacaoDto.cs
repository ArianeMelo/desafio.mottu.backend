using Newtonsoft.Json;

namespace Mottu.Locacao.Motos.Domain.Dtos
{
    public class LocacaoDto
    {
        [JsonProperty("entregador_id")]
        public string EntregadorId { get; set; }

    }
}
