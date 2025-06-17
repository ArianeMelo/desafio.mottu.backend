using Mottu.Locacao.Motos.Domain.PerfilAcesso;

namespace Mottu.Locacao.Motos.Domain.Entities
{
    public class Usuario : Entity
    {
        public string? Email { get; set; }
        public Perfil? Perfil { get; set; }

        public Usuario(string? email, Perfil? perfil)
        {
            Email = email;
            Perfil = perfil;
        }
    }
}
