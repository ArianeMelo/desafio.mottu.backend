using FluentValidation;
using Newtonsoft.Json;

namespace Mottu.Locacao.Motos.Domain.Dtos
{
    public class LocacaoRequestDto
    {       

        [JsonProperty("entregador_id")]
        public string? EntregadorId { get; set; } = string.Empty;

        [JsonProperty("moto_id")]
        public string? MotoId { get; set; } = string.Empty;

        [JsonProperty("data_inicio")]
        public DateTime DataInicio { get; set; }

        [JsonProperty("data_encerramento")]
        public DateTime DataEncerramento { get; set; }

        [JsonProperty("data_previsao_encerramento")]
        public DateTime DataPrevistaEncerramento { get; set; }

        [JsonProperty("plano_locacao")]
        public int PlanoLocacao { get; set; }
    }

    public class LocacaoRequestDtoValidator : AbstractValidator<LocacaoRequestDto>
    {
        const string MensagemErro = "Digite um valor válido";
        public LocacaoRequestDtoValidator()
        {           
            RuleFor(dto => dto.EntregadorId)
               .NotNull()
               .NotEmpty()
               .WithMessage("{PropertyName}. " + MensagemErro);

            RuleFor(dto => dto.MotoId)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName}. " + MensagemErro);

            RuleFor(dto => dto.PlanoLocacao)
                .Must(plano => IsValidPlanoLocacao(plano))
                .WithMessage("{PropertyName} inválido");

            RuleFor(dto => dto.DataInicio)
                .GreaterThanOrEqualTo(DateTime.Now)
                .WithMessage("{PropertyName} deve ser maior que a data atual");

            RuleFor(dto => dto.DataEncerramento)
                .GreaterThanOrEqualTo(DateTime.Now)
                .WithMessage("{PropertyName} deve ser maior que a data atual");

            RuleFor(dto => dto.DataPrevistaEncerramento)
                .GreaterThanOrEqualTo(DateTime.Now)
                .WithMessage("{PropertyName} deve ser maior que a data atual");
        }

        private bool IsValidPlanoLocacao(int plano)
        {
            int[] planos = { 7, 15, 30, 45, 50 };

            return planos.Contains(plano);
        }

    }
}
