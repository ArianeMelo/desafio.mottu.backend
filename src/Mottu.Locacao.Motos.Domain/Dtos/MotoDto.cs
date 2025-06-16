using FluentValidation;
using Newtonsoft.Json;

namespace Mottu.Locacao.Motos.Domain.Dtos
{
    public class MotoDto
    {
        [JsonProperty("identificador")]
        public string Identificador { get; set; }

        [JsonProperty("ano")]
        public int Ano { get; set; }

        [JsonProperty("modelo")]
        public string Modelo { get; set; }

        [JsonProperty("placa")]
        public string Placa { get; set; }

        public MotoDto(string identificador, int ano, string modelo, string placa)
        {
            Identificador = identificador;
            Ano = ano;
            Modelo = modelo;
            Placa = placa;
        }
    }

    public class MotoDtoValidator : AbstractValidator<MotoDto>
    {
        const string MensagemErro = "Digite um valor válido";
        public MotoDtoValidator()
        {
            RuleFor(dto => dto.Identificador)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName}. " + MensagemErro);

            RuleFor(dto => dto.Ano)
                .GreaterThan(1800)
                .WithMessage("{PropertyName}. " + MensagemErro);

            RuleFor(dto => dto.Modelo)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName}. " + MensagemErro);

            RuleFor(dto => dto.Placa)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName}. " + MensagemErro);
        }
    }
}
