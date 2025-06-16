using Newtonsoft.Json;

namespace Mottu.Locacao.Motos.Api.Response
{
    public class ResponseRequest
    {
        [JsonProperty("status_cod")]
        public int StatusCode { get; set; }

        [JsonProperty("sucesso")]
        public bool Sucesso { get; set; }

        [JsonProperty("dados")]
        public object? Dados { get; set; }

        [JsonProperty("erros")]
        public IEnumerable<string> Erros { get; set; }

        public ResponseRequest(int statusCode, bool sucesso, object? dados, IEnumerable<string> erros)
        {
            StatusCode = statusCode;
            Sucesso = sucesso;
            Dados = dados;
            Erros = erros;
        }       
    }

   
}
