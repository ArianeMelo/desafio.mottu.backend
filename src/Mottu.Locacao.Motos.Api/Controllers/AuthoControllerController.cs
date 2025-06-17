using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Mottu.Locacao.Motos.Application.Configuration;
using Mottu.Locacao.Motos.Domain.Dtos;
using Mottu.Locacao.Motos.Domain.Entities;
using Mottu.Locacao.Motos.Domain.Interface.Service;
using Mottu.Locacao.Motos.Domain.PerfilAcesso;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Mottu.Locacao.Motos.Api.Controllers
{
    /// <inheritdoc/>
    [Route("api/auth")]
    [ApiController]
    public class AuthController(
        ILogger<EntregadorController> logger,
        INotificacaoDominioHandler dominioHandler,
        IOptions<JwtSettings> jwtSettings) : BaseController
    {   
        private readonly JwtSettings _jwtSettigns = jwtSettings.Value;

        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [HttpPost("logar")]        
        public async Task<IActionResult> Logar([FromBody] UsuarioDto usuarioDto, CancellationToken cancellation)
        {
            var usuario = new Usuario(usuarioDto.Email, usuarioDto.Perfil);

            var claims = new List<Claim>
            {
                CriarClaim(usuarioDto.Perfil),
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, usuario.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64)
            };

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();            
            var key = Encoding.ASCII.GetBytes(_jwtSettigns.Secret!);           

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _jwtSettigns.Emissor,
                Audience = _jwtSettigns.ValidoEm,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_jwtSettigns.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });            

            var accessToken = tokenHandler.WriteToken(token);

            return Ok(
                new
                {
                    email = usuario.Email,
                    perfil = usuario.Perfil,
                    accessToken
                });
        }

        private static Claim CriarClaim(Perfil? perfil)
        {
            switch(perfil)
            {
                case Perfil.Admin:
                    return new Claim(ClaimTypes.Role, "Admin");
                case Perfil.Entregador:
                    return new Claim(ClaimTypes.Role, "Entregador");                
                default:
                    throw new ArgumentException("Perfil inválido.");
            }
        }

        private static long ToUnixEpochDate(DateTime date)
          => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}
