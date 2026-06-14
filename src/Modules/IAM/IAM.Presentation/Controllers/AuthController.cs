using IAM.Application.Features.Usuarios.AutenticarManual;
using IAM.Application.Features.Usuarios.AutenticarSso;
using IAM.Application.Features.Usuarios.RegistrarUsuario;
using IAM.Domain.Exceptions;
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

    [HttpPost("login")]
    public async Task<IActionResult> LoginManual([FromBody] LoginManualRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var command = new AutenticarManualCommand(request.Email, request.Password);
            var token = await _sender.Send(command, cancellationToken);
        
            return Ok(new { Token = token });
        }
        catch (DomainException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpPost("registro")]
    public async Task<IActionResult> Registrar([FromBody] RegistroRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var command =
                new RegistrarUsuarioCommand(request.Email, request.Nombre, request.Apellido, request.Password);
            var usuarioId = await _sender.Send(command, cancellationToken);

            return Ok(new { Id = usuarioId });
        }
        catch (DomainException ex)
        {
            return BadRequest(new {Message  = ex.Message});
        }
            
    }
}