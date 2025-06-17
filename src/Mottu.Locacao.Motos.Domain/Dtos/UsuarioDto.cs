using Mottu.Locacao.Motos.Domain.PerfilAcesso;
using Newtonsoft.Json;

namespace Mottu.Locacao.Motos.Domain.Dtos
{
    public class UsuarioDto
    {   
        [JsonProperty("email")]
        public string? Email { get; set; }
        
        [JsonProperty("perfil")]
        public Perfil? Perfil { get; set; }
    }
}
