using FluentValidation;

namespace Mottu.Locacao.Motos.Domain.Dtos
{
    public class PlacaMotoDto
    {
        public string Placa { get; set; }
    }

    public class PlacaMotoDtoValidator : AbstractValidator<PlacaMotoDto>
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
