using IAM.Domain.Exceptions;

namespace IAM.Domain.Entities;

public class Usuario
{
    public Guid Id { get; private set; }
    public string Email { get; private set; } = string.Empty;
    public string Nombre { get; private set; } = string.Empty;
    public string Apellido { get; private set; } = string.Empty;
    public string? PasswordHash { get; private set; }
    public int RolId { get; private set; }
    public bool Activo { get; private set; } = true;
    public DateTime FechaRegistro { get; private set; }

    public Rol Rol { get; private set; } = null!;

    private Usuario(){ }

    private Usuario(string email, string nombre, string apellido, int rolId, string? passwordHash = null)
    {
        Id = Guid.NewGuid();
        Email = email;
        Nombre = nombre;
        Apellido = apellido;
        RolId = rolId;
        PasswordHash = passwordHash;
        Activo = true;
        FechaRegistro = DateTime.UtcNow;
    }
    
    public static Usuario CreateSSO(string email, string nombre, string apellido, int rolId)
    {
        ValidarDatosBase(email, nombre, apellido, rolId);
        
        return new Usuario(email.Trim().ToLowerInvariant(), nombre.Trim(), apellido.Trim(), rolId);
    }

    public static Usuario CreateManual(string email, string nombre, string apellido, string passwordHash, int rolId)
    {
        ValidarDatosBase(email, nombre, apellido, rolId);
        
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new DomainException("La contraseña es requerida para el registro manual.");

        return new Usuario(email.Trim().ToLowerInvariant(), nombre.Trim(), apellido.Trim(), rolId, passwordHash);
    }
    
    private static void ValidarDatosBase(string email, string nombre, string apellido, int rolId)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new DomainException("El email es requerido.");
        
        if (string.IsNullOrWhiteSpace(nombre))
            throw new DomainException("El nombre es requerido.");
        
        if (string.IsNullOrWhiteSpace(apellido))
            throw new DomainException("El apellido es obligatorio.");    
        
        if (rolId <= 0)
            throw new DomainException("El rol es requerido.");
    }

    public void ActulizarNombre(string nuevoNombre)
    {
        if(string.IsNullOrWhiteSpace(nuevoNombre))
            throw new DomainException("El nuevo nombre es requerido");

        if (nuevoNombre == Nombre)
            throw new DomainException("El nuevo nombre es igual al actual");
        
        Nombre = nuevoNombre;
    }

    public void ActulizarApellido(string nuevoApellido)
    {
        if (string.IsNullOrWhiteSpace(nuevoApellido))
            throw new DomainException("El apellido es  obligatorio");
        
        if (nuevoApellido == Apellido)
            throw new DomainException("El nuevo apellido es igual al actual");
        
        Apellido = nuevoApellido;
    }

    public void CambiarRol(int nuevoRolId)
    {
        if(nuevoRolId <= 0)
            throw new DomainException("Rol inavlido");
        
        if(nuevoRolId == RolId)
            throw new DomainException("El usuario ya tiene asignado este rol");
        
        RolId = nuevoRolId;
    }

    public void ActivarCuenta()
    {
        if(Activo)
            throw new DomainException("La cuenta ya esta activa");

        Activo = true;
    }

    public void DesactivarCuenta()
    {
        if(!Activo)
            throw new DomainException("La cuenta ya esta desactiva");
        
        Activo = false;
    }

    public string NombreCompleto() => $"{Nombre} {Apellido}".Trim();

}