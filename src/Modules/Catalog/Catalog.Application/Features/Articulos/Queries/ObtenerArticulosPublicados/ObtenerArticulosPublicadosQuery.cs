using IAM.Application.Common.Models;
using MediatR;

namespace Catalog.Application.Features.Articulos.Queries.ObtenerArticulosPublicados;

public record ArticuloPublicadoDto(
    Guid Id,
    string Titulo,
    string NombresAutores,
    string Resumen,
    string ArchivoUrl,
    string Carrera,
    string Materia,
    DateTime FechaPublicacion,
    List<string> Etiquetas);

public record ObtenerArticulosPublicadosQuery : IRequest<Result<List<ArticuloPublicadoDto>>>;