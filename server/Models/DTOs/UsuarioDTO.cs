using server.Models.Entities;
namespace server.Models.DTOs;

public class UsuarioDTO
{
    public required string Nombre { get; set; }
    public required string Email { get; set; }
}

public class UsuarioQueryDTO
{
    public Guid UsuarioId { get; set; }
    public required string Nombre { get; set; }
    public required string Email { get; set; }
    public Rol? rol { get; set; }
}

public class UsuarioProyectoQueryDTO
{
    public Guid UsuarioId { get; set; }
    public required string Nombre { get; set; }
    public required string Email { get; set; }
    public Rol? rol { get; set; } = null;
    public Guid? ProyectoId { get; set; }
}
