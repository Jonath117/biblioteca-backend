using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workspace.Application.Features.Usuarios.VerificarEstudiante;

namespace Workspace.Presentation.Controllers;

[ApiController]
[Route("api/workspace/estudiantes")]
[Authorize]
public class EstudiantesController : ControllerBase
{
    private readonly ISender _sender;

    public EstudiantesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("verificar/{email}")]
    public async Task<IActionResult> VerificarEstudiante(string email)
    {
        var query = new VerificarEstudianteQuery(email);
        var result = await _sender.Send(query);

        if (result.IsFailure)
        {
            return NotFound(new { Error = result.Error.Message });
        }

        return Ok(result.Value);
    }
}
