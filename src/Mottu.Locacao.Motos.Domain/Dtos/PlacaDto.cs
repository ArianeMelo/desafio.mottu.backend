using FluentValidation;

namespace Mottu.Locacao.Motos.Domain.Dtos
{
    public class PlacaDto
    {
        public string Placa { get; set; }
    }

    public class PlacaMotoDtoValidator : AbstractValidator<PlacaDto>
    {
        const string MensagemErro = "Digite um valor válido";
        public PlacaMotoDtoValidator()
        {            

            RuleFor(dto => dto.Placa)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName}. " + MensagemErro);
        }
    }
}
