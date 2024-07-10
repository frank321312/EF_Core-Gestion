using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models.Entities;

public class Usuario
{
    [Key]
    public Guid UsuarioId { get; set; } = Guid.NewGuid();

    [Required]
    [StringLength(45)]
    public string Nombre { get; set; } = null!;

    [Required]
    [StringLength(120)]
    public string Email { get; set; } = null!;

    [ForeignKey("RolId")]
    public Rol? rol { get; set;} = null;

    [ForeignKey("ProyectoId")]
    public Proyecto? proyecto { get; set; }

    public Usuario(string nombre, string email)
    {
        Nombre = nombre;
        Email = email;
    }
    public Usuario() { }
}
