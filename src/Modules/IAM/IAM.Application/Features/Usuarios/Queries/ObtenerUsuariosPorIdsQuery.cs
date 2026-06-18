using IAM.Application.Common.Models;
using MediatR;

namespace IAM.Application.Features.Usuarios.Queries;

public record UsuarioDto(Guid Id, string Nombre, string Apellido, string email);

public record ObtenerUsuariosPorIdsQuery(List<Guid> Ids) : IRequest<Result<List<UsuarioDto>>>;