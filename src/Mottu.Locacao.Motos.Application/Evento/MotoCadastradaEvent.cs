using Newtonsoft.Json;

namespace Mottu.Locacao.Motos.Application.Evento
{
    public class MotoCadastradaEvent
    {
        [JsonProperty("ano")]
        public int Ano { get; set; }

        [JsonProperty("placa")]
        public string Placa { get; set; }

        public MotoCadastradaEvent(int ano, string placa)
        {
            Ano = ano;
            Placa = placa;
        }

    }
}
