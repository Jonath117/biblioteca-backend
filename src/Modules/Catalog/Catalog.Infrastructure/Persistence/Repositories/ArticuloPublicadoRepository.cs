using Catalog.Application.Common.Interfaces;
using Catalog.Application.Features.Articulos.Queries.ObtenerArticulosPublicados;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Persistence.Repositories;

public class ArticuloPublicadoRepository : IArticuloPublicadoRepository
{
    private readonly CatalogDbContext _context;

    public ArticuloPublicadoRepository(CatalogDbContext context)
    {
        _context = context;
    }
    
    
    public async Task AddAsync(ArticuloPublicado articuloPublicado, CancellationToken cancellationToken = default)
    {
        await _context.AddAsync(articuloPublicado, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<ArticuloPublicadoDto>> ObtenerPublicadosAsync(CancellationToken cancellationToken = default)
    {
        return await _context.ArticulosPublicados
            .AsNoTracking()
            .Include(a => a.ArticuloEtiquetas)
            .ThenInclude(ae => ae.Etiqueta)
            .OrderByDescending(a => a.FechaPublicacion)
            .Select(a => new ArticuloPublicadoDto(
                a.Id,
                a.Titulo,
                a.NombresAutores,
                a.Resumen,
                a.ArchivoUrl,
                a.Carrera,
                a.Materia,
                a.FechaPublicacion,
                a.ArticuloEtiquetas.Select(ae => ae.Etiqueta.Nombre).ToList()
            ))
            .ToListAsync(cancellationToken);
    }
}