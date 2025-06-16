using FluentValidation;
using Mottu.Locacao.Motos.Domain.Enum;
using Newtonsoft.Json;

namespace Mottu.Locacao.Motos.Domain.Dtos
{
    public class EntregadorDto
    {
        [JsonProperty("identificador")]
        public string Identificador { get; set; } = string.Empty;

        [JsonProperty("nome")]
        public string Nome { get; set; } = string.Empty;

        [JsonProperty("cnpj")]
        public string Cnpj { get; set; } = string.Empty;

        [JsonProperty("data_nascimento")]
        public string DataNascimento { get; set; } = string.Empty;

        [JsonProperty("numero_cnh")]
        public string NumeroCnh { get; set; } = string.Empty;

        [JsonProperty("tipo_cnh")]
        public string TipoCnh { get; set; } = string.Empty;

        [JsonProperty("imagem_cnh")]
        public string ImagemCnh { get; set; } = string.Empty;

    }

    public class EntregadorDtoValidation : AbstractValidator<EntregadorDto>
    {
        public EntregadorDtoValidation()
        {
            RuleFor(dto => dto.Cnpj)
               .NotEmpty().NotNull()
               .WithMessage("{Propertyname}. Informar dado válido");

            RuleFor(dto => dto.TipoCnh)
                .IsInEnum()
                .WithMessage("{Propertyname}. Informar  válido");
        }
    }
}
