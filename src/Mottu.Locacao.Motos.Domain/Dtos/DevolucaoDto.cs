using FluentValidation;
using Newtonsoft.Json;

namespace Mottu.Locacao.Motos.Domain.Dtos
{
    public class DevolucaoDto
    {
        [JsonProperty("data_devolucao")]
        public DateTime? DataDevolucao { get; set; }
    }

    public class DevolucaoDtoValidator : AbstractValidator<DevolucaoDto>
    {
        public DevolucaoDtoValidator()
        {
            RuleFor(dto => dto.DataDevolucao)
                .NotNull()
                .WithMessage("{PropertyName} não pode ser nulo.")
                .GreaterThanOrEqualTo(DateTime.Now)
                .WithMessage("{PropertyName} deve ser maior ou igual a data atual.");
        }
    }
}