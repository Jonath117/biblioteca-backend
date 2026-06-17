using FluentValidation;

namespace IAM.Application.Features.Usuarios.AutenticarManual;

public class AutenticarManualValidator : AbstractValidator<AutenticarManualCommand>
{
    public AutenticarManualValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El email es requerido.")
            .EmailAddress().WithMessage("El formato del email es incorrecto.")
            .Must(email => email.ToLower().EndsWith("@ucb.edu.bo"))
            .WithMessage("Solo se permiten correos institucionales (@ucb.edu.bo).");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("La contraseña es requerida.");
    }
}