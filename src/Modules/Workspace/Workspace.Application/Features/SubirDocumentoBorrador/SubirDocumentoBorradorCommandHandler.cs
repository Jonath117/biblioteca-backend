using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Workspace.Application.Common.Abstractions;
using Workspace.Application.Common.Interfaces;
using Workspace.Application.Common.Models;
using Workspace.Domain.Entities;

namespace Workspace.Application.Features.SubirDocumentoBorrador;

public class SubirDocumentoBorradorCommandHandler : ICommandHandler<SubirDocumentoBorradorCommand, Guid>
{
    private readonly IDocumentoRepository _documentoRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileStorageService _fileStorageService;
    
    private readonly IPublisher _publisher;

    public SubirDocumentoBorradorCommandHandler(
        IDocumentoRepository documentoRepository,
        IUnitOfWork unitOfWork,
        IFileStorageService fileStorageService,
        IPublisher publisher)
    {
        _documentoRepository = documentoRepository;
        _unitOfWork = unitOfWork;
        _fileStorageService = fileStorageService;
        _publisher = publisher;
    }

    public async Task<Result<Guid>> Handle(SubirDocumentoBorradorCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // 1. Subir archivo al almacenamiento configurado (Local o S3)
            string archivoUrl = await _fileStorageService.UploadFileAsync(
                request.ArchivoStream,
                request.ArchivoNombre,
                cancellationToken);

            // 2. Crear la entidad Documento
            var documentoId = Guid.NewGuid();
            var documento = new Documento
            {
                Id = documentoId,
                AutorPrincipalId = request.AutorPrincipalId,
                Titulo = request.Titulo,
                Resumen = request.Resumen,
                ArchivoUrl = archivoUrl,
                Estado = "Borrador",
                FechaCreacion = DateTime.UtcNow,
                FechaModificacion = DateTime.UtcNow
            };

            // 3. Gestionar la lista de coautores (tabla intermedia Coautor)
            if (request.CoautoresIds != null)
            {
                foreach (var coautorId in request.CoautoresIds)
                {
                    // Evitar que el autor principal se agregue como coautor
                    if (coautorId == request.AutorPrincipalId)
                    {
                        continue;
                    }

                    documento.Coautores.Add(new Coautor
                    {
                        DocumentId = documentoId,
                        UsuarioId = coautorId
                    });
                }
            }

            // 4. Registrar en base de datos y confirmar cambios
            await _documentoRepository.AddAsync(documento, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var evento = new DocumentoSubidoEvent(documentoId, request.AutorPrincipalId, request.Titulo);
            await _publisher.Publish(evento, cancellationToken);
            
            return Result.Success(documentoId);
        }
        catch (Exception ex)
        {
            return Result.Failure<Guid>(new Error(
                "Workspace.SubirDocumentoBorradorError", 
                $"Error al procesar y subir el borrador: {ex.Message}"));
        }
    }
}
