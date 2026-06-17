using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Workspace.Application.Features.ObtenerDocumentosPorAutor;
using Workspace.Application.Features.SubirDocumentoBorrador;

namespace Workspace.Presentation.Controllers;

[ApiController]
[Route("api/workspace/documentos")]
[Authorize]
public class DocumentosController : ControllerBase
{
    private readonly ISender _sender;

    public DocumentosController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerDocumentos()
    {
        var autorIdClaim = User.FindFirst("sub")
                           ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
        
        if (autorIdClaim == null || !Guid.TryParse(autorIdClaim.Value, out var autorId))
        {
            return Unauthorized(new { Error = "No se pudo identificar al usuario." });
        }

        var query = new ObtenerDocumentosPorAutorQuery(autorId);
        var result = await _sender.Send(query);

        if (result.IsFailure)
        {
            return BadRequest(new { Error = result.Error.Message });
        }

        return Ok(result.Value);
    }

    [HttpPost("subir-borrador")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> SubirBorrador([FromForm] SubirDocumentoBorradorRequest request)
    {
        if (request.Archivo == null || request.Archivo.Length == 0)
        {
            return BadRequest(new { Error = "El archivo es obligatorio y no puede estar vacío." });
        }

        using var stream = request.Archivo.OpenReadStream();

        var command = new SubirDocumentoBorradorCommand(
            request.AutorPrincipalId,
            request.Titulo,
            request.Resumen,
            stream,
            request.Archivo.FileName,
            request.CoautoresIds
        );

        var result = await _sender.Send(command);

        if (result.IsFailure)
        {
            return BadRequest(new { Error = result.Error.Message, Code = result.Error.Code });
        }

        return Ok(new { DocumentoId = result.Value });
    }
}

public class SubirDocumentoBorradorRequest
{
    public Guid AutorPrincipalId { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Resumen { get; set; } = string.Empty;
    public IFormFile Archivo { get; set; } = null!;
    public List<Guid> CoautoresIds { get; set; } = new();
}
