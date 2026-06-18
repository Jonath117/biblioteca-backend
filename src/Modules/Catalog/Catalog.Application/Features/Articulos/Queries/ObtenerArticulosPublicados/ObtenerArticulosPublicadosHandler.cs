using Catalog.Application.Common.Interfaces;
using IAM.Application.Common.Models;
using MediatR;

namespace Catalog.Application.Features.Articulos.Queries.ObtenerArticulosPublicados;

public class ObtenerArticulosPublicadosHandler : IRequestHandler<ObtenerArticulosPublicadosQuery, Result<List<ArticuloPublicadoDto>>>
{
    private readonly IArticuloPublicadoRepository _repository;
    
    public ObtenerArticulosPublicadosHandler(IArticuloPublicadoRepository repository)
    {
        _repository = repository;
    }


    public async Task<Result<List<ArticuloPublicadoDto>>> Handle(ObtenerArticulosPublicadosQuery request, CancellationToken cancellationToken)
    {
        var articulos = await _repository.ObtenerPublicadosAsync(cancellationToken);
        return Result<List<ArticuloPublicadoDto>>.Success(articulos);
    }
}