namespace Workspace.Domain.Entities;

public class Coautor
{
    public Guid DocumentId { get; set; }
    public Guid UsuarioId { get; set; }

    public Documento Documento { get; set; } = null;
}