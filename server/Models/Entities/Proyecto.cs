using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace server.Models.Entities;

public class Proyecto
{
    [Key]
    public Guid ProyectoId { get; set;} = Guid.NewGuid();

    [Required]
    [StringLength(45)]
    public string Nombre { get; set; } = string.Empty;
    public DateTime FechaInicial { get; set; }
    public DateTime? FechaFinal { get; set; } = null;
    public List<Usuario> usuarios { get; set; } = new List<Usuario>();
    public Proyecto(string nombre)
    {
        DateTime fechaActual = DateTime.Now;
        int año = fechaActual.Year;
        int mes = fechaActual.Month;
        int dia = fechaActual.Day;
        Nombre = nombre;
        FechaInicial = new DateTime(año, mes, dia).ToUniversalTime();
    }
    public Proyecto() { }
}
