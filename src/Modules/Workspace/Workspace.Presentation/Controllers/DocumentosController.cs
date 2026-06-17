using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Workspace.Application.Features.SubirDocumentoBorrador;

namespace Workspace.Presentation.Controllers;

[ApiController]
[Route("api/workspace/documentos")]
public class DocumentosController : ControllerBase
{
    private readonly ISender _sender;

    public DocumentosController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("subir-borrador")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> SubirBorrador([FromForm] SubirDocumentoBorradorRequest request)
    {
        if (request.Archivo == null || request.Archivo.Length == 0)
        {
            return BadRequest(new { Error = "El archivo es obligatorio y no puede estar vacío." });
        }

        // Abrir el stream para pasarlo al Command (manteniendo la capa de Application libre de dependencias HTTP)
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
