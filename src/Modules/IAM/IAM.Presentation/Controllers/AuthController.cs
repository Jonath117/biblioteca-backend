using IAM.Application.Features.Usuarios.CrearUsuario;
using IAM.Presentation.Requests;
using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace IAM.Presentation.Controllers;

[ApiController]
[Route("api/iam/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ISender _sender;

    public AuthController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("sso")]
    public async Task<IActionResult> AutenticarSso([FromBody] SsoRequests request, CancellationToken cancellationToken)
    {
        var command = new AutenticarSsoCommand(request.IdTokenGoogle);

        var token = await _sender.Send(command, cancellationToken);

        return Ok(new {Token = token});
    }
}