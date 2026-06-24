using System.Threading;
using System.Threading.Tasks;
using Workspace.Application.Common.Abstractions;
using Workspace.Application.Common.Interfaces;
using Workspace.Application.Common.Models;

namespace Workspace.Application.Features.Usuarios.VerificarEstudiante;

public class VerificarEstudianteQueryHandler : IQueryHandler<VerificarEstudianteQuery, EstudianteDto>
{
    private readonly IEstudianteReplicadoRepository _repository;

    public VerificarEstudianteQueryHandler(IEstudianteReplicadoRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<EstudianteDto>> Handle(VerificarEstudianteQuery request, CancellationToken cancellationToken)
    {
        var estudiante = await _repository.GetByEmailAsync(request.Email, cancellationToken);
        
        if (estudiante == null)
        {
            return Result.Failure<EstudianteDto>(new Error("Workspace.EstudianteNoEncontrado", "No se encontró ningún estudiante con ese correo."));
        }

        return Result.Success(new EstudianteDto(estudiante.Email, estudiante.Nombre, estudiante.Apellido));
    }
}
