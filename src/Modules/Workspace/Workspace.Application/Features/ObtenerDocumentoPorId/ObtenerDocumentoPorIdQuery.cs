using Workspace.Application.Common.Abstractions;

namespace Workspace.Application.Features.ObtenerDocumentoPorId;

public record ObtenerDocumentoPorIdQuery(Guid Id) : IQuery<DocumentoDto>;