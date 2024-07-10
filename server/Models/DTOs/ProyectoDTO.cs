using server.Models.Entities;
namespace server.Models.DTOs;

public class ProyectoDTO
{
    public required string Nombre { get; set; }
}

public class ProyectoQueryDTO
{
    public Guid? ProyectoId { get; set;}
    public string Nombre { get; set; } = string.Empty;
    public DateTime FechaInicial { get; set; }
    public DateTime? FechaFinal { get; set; } = null;
    public List<UsuarioQueryDTO> usuarios { get; set; } = new List<UsuarioQueryDTO>();
}

public class ProyectoIdQueryDTO
{
    public Guid? ProyectoId { get; set;}
}
