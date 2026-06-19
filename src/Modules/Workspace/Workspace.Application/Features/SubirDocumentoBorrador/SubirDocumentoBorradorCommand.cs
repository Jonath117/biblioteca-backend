using System;
using System.Collections.Generic;
using System.IO;
using Workspace.Application.Common.Abstractions;

namespace Workspace.Application.Features.SubirDocumentoBorrador;

public record SubirDocumentoBorradorCommand(
    Guid AutorPrincipalId,
    string Titulo,
    string Resumen,
    Stream ArchivoStream,
    string ArchivoNombre,
    List<Guid> CoautoresIds
) : ICommand<Guid>;
