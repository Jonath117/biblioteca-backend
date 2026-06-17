using System;
using FluentValidation;

namespace Workspace.Application.Features.SubirDocumentoBorrador;

public class SubirDocumentoBorradorCommandValidator : AbstractValidator<SubirDocumentoBorradorCommand>
{
    public SubirDocumentoBorradorCommandValidator()
    {
        RuleFor(x => x.AutorPrincipalId)
            .NotEmpty().WithMessage("El ID del autor principal es obligatorio.");

        RuleFor(x => x.Titulo)
            .NotEmpty().WithMessage("El título del documento es obligatorio.")
            .MaximumLength(250).WithMessage("El título no puede superar los 250 caracteres.");

        RuleFor(x => x.Resumen)
            .NotEmpty().WithMessage("El resumen del documento es obligatorio.");

        RuleFor(x => x.ArchivoStream)
            .NotNull().WithMessage("El archivo del borrador es obligatorio.");

        RuleFor(x => x.ArchivoNombre)
            .NotEmpty().WithMessage("El nombre del archivo es obligatorio.")
            .Must(nombre => nombre.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
            .WithMessage("Únicamente se permiten archivos con formato PDF.");
    }
}
