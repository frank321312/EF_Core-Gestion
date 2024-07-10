using System.ComponentModel.DataAnnotations;
namespace server.Models.Entities;

public class Rol
{
    [Key]
    public Guid RolId { get; set; }

    [Required]
    [StringLength(45)]
    public string Nombre { get; set; } = null!;

}