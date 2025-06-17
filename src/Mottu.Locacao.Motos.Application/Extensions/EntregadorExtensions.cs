using Mottu.Locacao.Motos.Domain.Dtos;
using Mottu.Locacao.Motos.Domain.Entities;

namespace Mottu.Locacao.Motos.Application.Extensions
{
    public static class EntregadorExtensions
    {
        public static Entregador? ParaDominio(this EntregadorDto dto)
        {
            if (dto == null) return null;

            return new Entregador(
                dto.Identificador,
                dto.Nome,
                dto.Cnpj,
                dto.DataNascimento,
                dto.NumeroCnh,
                (int)dto.TipoCnh,
                dto.ImagemCnh
            );
        }

        public static async Task<bool> SalvarImagemCnh(string base64, string cnpj, string cnhFolder)
        {
            var partes = base64.Split(',');
            var base64Data = partes.Length > 1 ? partes[1] : partes[0];

            string? extensaoArquivo = await ObterExtensao(partes[0]);

            if (extensaoArquivo is null)
                return false;

            byte[] bytes = Convert.FromBase64String(base64Data);

            var nomeArquivo = $"{cnpj}.{extensaoArquivo}";
            var caminhoCompleto = Path.Combine(cnhFolder, nomeArquivo);

            if (File.Exists(caminhoCompleto))
                return true;

            File.WriteAllBytes(caminhoCompleto, bytes);

            return true;

            //return $"/cnh/{nomeArquivo}";
        }

        private static async Task<string?> ObterExtensao(string? tipoArquivo)
        {
            if (tipoArquivo == null)
                return null;

            Dictionary<string, string> _mapeamentos = new()
            {
                { "image/bmp", "bmp" },
                { "image/png", "png" }
            };

            foreach (var entrada in _mapeamentos)
            {
                if (tipoArquivo.Contains(entrada.Key))
                    return entrada.Value;
            }

            return null;
        }
    }
}
