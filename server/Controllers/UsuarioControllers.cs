using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using server.Models.Entities;
using server.Models.DTOs;
using AutoMapper;

namespace server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuarioControllers : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    public UsuarioControllers(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<UsuarioProyectoQueryDTO>>> GetUsuariosAsync()
    {
        return await _context.Usuarios
            .Select(x => new UsuarioProyectoQueryDTO
            {
                Nombre = x.Nombre,
                Email = x.Email,
                UsuarioId = x.UsuarioId,
                rol = x.rol,
                ProyectoId = x.proyecto != null ? x.proyecto.ProyectoId : null
            }).ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult> PostUsuarioAsync(string nombre, string email)
    {
        var existeUsuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Nombre == nombre || x.Email == email);
        if (existeUsuario != null)
        {
            throw new InvalidOperationException("Error, ya existe un usuario con esos datos");
        }
        var usuario = _mapper.Map<Usuario>(new UsuarioDTO { Nombre = nombre, Email = email });
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPost("asingarRol")]
    public async Task<ActionResult> AsignarRolAsync(Guid idUsuario, Guid idRol)
    {
        try
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.UsuarioId == idUsuario);
            var rol = await _context.Roles.FirstOrDefaultAsync(x => x.RolId == idRol);

            if (usuario == null)
            {
                return NotFound(new { message = "Usuario no encontrado, debe proporciona uno valido" });
            }
            else if (rol == null)
            {
                return NotFound(new { message = "Rol no encontrado, debe proporciona uno valido" });
            }

            usuario.rol = rol;
            await _context.SaveChangesAsync();

            return Ok();

        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Ocurrio un error al intentar asignar un rol al usuario", details = ex.Message });
        }
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteUsuarioAsync(Guid idUsuario)
    {
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.UsuarioId == idUsuario);
        if (usuario == null)
        {
            return NotFound(new { message = "Usuario no encontrado, debe proporciona uno valido" });
        }

        _context.Usuarios.Remove(usuario);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPut]
    public async Task<ActionResult> PutNameUsuarioAsync(Guid idUsuario, string nombre)
    {
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.UsuarioId == idUsuario);
        if (usuario == null)
        {
            return NotFound(new { message = "Usuario no encontrado, debe proporciona uno valido" });
        }

        usuario.Nombre = nombre;
        await _context.SaveChangesAsync();

        return Ok();
    }
}
