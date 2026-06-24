using Workspace.Application.Common.Abstractions;

namespace Workspace.Application.Features.Usuarios.VerificarEstudiante;

public record VerificarEstudianteQuery(string Email) : IQuery<EstudianteDto>;

public record EstudianteDto(string Email, string Nombre, string Apellido);
