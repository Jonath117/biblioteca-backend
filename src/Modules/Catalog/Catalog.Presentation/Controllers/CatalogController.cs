using Catalog.Application.Features.Articulos.Queries.ObtenerArticulosPublicados;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Presentation.Controllers;

[ApiController]
[Route("api/catalog/articulos")]
[AllowAnonymous]
public class CatalogController : ControllerBase
{
    private readonly ISender _sender;

    public CatalogController(ISender sender)
    {
        _sender = sender;
    }
    
    [HttpGet]
    public async Task<IActionResult> ObtenerCatalogo()
    {
        try
        {
            var query = new ObtenerArticulosPublicadosQuery();
            var result = await _sender.Send(query);

            if (result.IsFailure)
            {
                return BadRequest(new { Error = result.Error.Message });
            }

            return Ok(result.Value);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = "Error interno del servidor al cargar el catálogo.", Detalle = ex.Message });
        }
    }
}