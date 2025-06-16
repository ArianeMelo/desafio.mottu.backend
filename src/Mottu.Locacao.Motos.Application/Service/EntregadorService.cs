using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottu.Locacao.Motos.Application.Service
{
    public class EntregadorService
    {
        private readonly string _cnhFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "cnh");
        public EntregadorService()
        {
            if (!Directory.Exists(_cnhFolder))
                Directory.CreateDirectory(_cnhFolder);
        }

        public async Task<string> SalvarCnhAsync(IFormFile arquivo)
        {
            var extensao = Path.GetExtension(arquivo.FileName).ToLower();

            if (extensao != ".png" && extensao != ".bmp")
                throw new InvalidDataException("Somente arquivos PNG ou BMP são permitidos.");

            var nomeArquivo = $"{Guid.NewGuid()}{extensao}";
            var caminhoCompleto = Path.Combine(_cnhFolder, nomeArquivo);

            using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

            // Retorna URL relativa para acesso
            var urlRelativa = $"/cnh/{nomeArquivo}";
            return urlRelativa;
        }
    }
}
