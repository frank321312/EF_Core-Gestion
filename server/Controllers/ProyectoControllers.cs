using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using server.Models.Entities;
using server.Models.DTOs;
namespace server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProyectoControllers : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    public ProyectoControllers(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProyectoQueryDTO>>> GetProyectosAsync()
    {
        return await _context.Proyectos.Include(p => p.usuarios)
            .Select(p => new ProyectoQueryDTO
            {
                Nombre = p.Nombre,
                ProyectoId = p.ProyectoId,
                FechaInicial = p.FechaInicial,
                FechaFinal = p.FechaFinal,
                usuarios = p.usuarios.Select(u => _mapper.Map<UsuarioQueryDTO>(new UsuarioQueryDTO
                {
                    UsuarioId = u.UsuarioId,
                    Nombre = u.Nombre,
                    Email = u.Email,
                    rol = u.rol,
                })).ToList()
            })
        .ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult> AddProyectoAsync(string nombre)
    {
        var existeProyecto = await _context.Proyectos.FirstOrDefaultAsync(x => x.Nombre == nombre);
        if (existeProyecto != null) throw new InvalidOperationException("Error, ya existe un proyecto con ese nombre");
        var proyecto = _mapper.Map<Proyecto>(new ProyectoDTO { Nombre = nombre });
        _context.Proyectos.Add(proyecto);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPost("agregarUsuario")]
    public async Task<ActionResult> AddUsuarioAsync(Guid idUsuario, Guid idProyecto)
    {
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.UsuarioId == idUsuario);
        var proyecto = await _context.Proyectos.FirstOrDefaultAsync(x => x.ProyectoId == idProyecto);
        if (usuario == null)
        {
            return NotFound(new { message = "Usuario no encontrado, debe proporciona uno valido" });
        }
        else if (proyecto == null)
        {
            return NotFound(new { message = "Proyecto no encontrado, debe proporciona uno valido" });
        }
        proyecto.usuarios.Add(usuario);

        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete("eliminarUsuario")]
    public async Task<ActionResult> DeleteUsuarioAsync(Guid idUsuario, Guid idProyecto)
    {
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.UsuarioId == idUsuario);
        var proyecto = await _context.Proyectos.FirstOrDefaultAsync(x => x.ProyectoId == idProyecto);
        if (usuario == null)
        {
            return NotFound(new { message = "Usuario no encontrado, debe proporciona uno valido" });
        }
        else if (proyecto == null)
        {
            return NotFound(new { message = "Proyecto no encontrado, debe proporciona uno valido" });
        }
        proyecto.usuarios.Remove(usuario);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPost("fechaFinal")]
    public async Task<ActionResult> AsignarFechaFinalAsync(Guid idProyecto)
    {
        try
        {
            var proyecto = await _context.Proyectos.FirstOrDefaultAsync(x => x.ProyectoId == idProyecto);
            if (proyecto == null)
            {
                return NotFound(new { message = "Proyecto no encontrado, debe proporciona uno valido" });
            }
            DateTime fechaActual = DateTime.Now;
            proyecto.FechaFinal = new DateTime(fechaActual.Year, fechaActual.Month, fechaActual.Day).ToUniversalTime();
            await _context.SaveChangesAsync();

            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Ocurrio un error al intentar asignar la fecha final al proyecto", details = ex.Message });
        }
    }
}
