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
        public DateTime DataNascimento { get; set; }

        [JsonProperty("numero_cnh")]
        public string NumeroCnh { get; set; } = string.Empty;

        [JsonProperty("tipo_cnh")]
        public CategoriaCnh TipoCnh { get; set; }

        [JsonProperty("imagem_cnh")]
        public string ImagemCnh { get; set; } = string.Empty;

    }

    public class EntregadorDtoValidation : AbstractValidator<EntregadorDto>
    {
        public EntregadorDtoValidation()
        {
            RuleFor(dto => dto.Nome)
                .NotEmpty().NotNull()
                .WithMessage("{PropertyName} inválido");

            RuleFor(dto => dto.Identificador)
                .NotEmpty().NotNull()
                .WithMessage("{PropertyName}. Informar válido");

            RuleFor(dto => dto.Cnpj)
                .NotEmpty().NotNull()
                .WithMessage("{PropertyName}. Informar dado válido");

            RuleFor(dto => dto.DataNascimento)
                .GreaterThan(new DateTime(1900, 01, 01))
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("{PropertyName} deve ser menor que a data atual");

            RuleFor(dto => dto.NumeroCnh)
                .NotEmpty().NotNull()
                .WithMessage("{PropertyName}. Informar dado válido");

            RuleFor(dto => dto.ImagemCnh)
                .NotEmpty().NotNull()
                .WithMessage("{PropertyName}. Informar dado válido");
        }
       
    }
}
