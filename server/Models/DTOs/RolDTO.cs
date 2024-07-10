using System.ComponentModel.DataAnnotations;
namespace server.Models.DTOs;

public class RolDTO
{
    [Required]
    [StringLength(45)]
    public string Nombre { get; set; } = null!;
}
