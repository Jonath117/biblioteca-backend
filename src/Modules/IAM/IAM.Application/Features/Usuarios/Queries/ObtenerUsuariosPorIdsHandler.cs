using IAM.Application.Common.Models;
using IAM.Application.Interfaces.Repository;
using MediatR;

namespace IAM.Application.Features.Usuarios.Queries;

public class ObtenerUsuariosPorIdsQueryHandler : IRequestHandler<ObtenerUsuariosPorIdsQuery, Result<List<UsuarioDto>>>
{
    private readonly IUsuarioRepository _repository;

    public ObtenerUsuariosPorIdsQueryHandler(IUsuarioRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<List<UsuarioDto>>> Handle(ObtenerUsuariosPorIdsQuery request,
        CancellationToken cancellationToken)
    {
        if (request.Ids == null || request.Ids.Count == 0)
        {
            return Result<List<UsuarioDto>>.Success(new List<UsuarioDto>());
        }

        var usuarios = await _repository.GetByIdsAsync(request.Ids, cancellationToken);

        List<UsuarioDto> usuarioDtos = usuarios.Select(u => new UsuarioDto(
            u.Id,
            u.Nombre,
            u.Apellido,
            u.Email
        )).ToList();
        
        return Result<List<UsuarioDto>>.Success(usuarioDtos);
    }
}