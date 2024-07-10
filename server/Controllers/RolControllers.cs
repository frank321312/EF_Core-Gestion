using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using server.Models.Entities;
using server.Models.DTOs;
using AutoMapper;

namespace server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RolControllers : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    public RolControllers(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<Rol>>> GetRolesAsync() 
        => await _context.Roles.ToListAsync();

    [HttpPost]
    public async Task<ActionResult<Rol>> PostRolAsync(string nombre)
    {
        try
        {
            var existeRol = await _context.Roles.FirstOrDefaultAsync(x => x.Nombre  == nombre);
            if (existeRol != null) throw new InvalidOperationException("Error, ya existe ese rol");
            var rol = _mapper.Map<Rol>(new RolDTO { Nombre = nombre });
            _context.Roles.Add(rol);
            await _context.SaveChangesAsync();
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Ocurrio un error al crear el rol", details = ex.Message });
        }
    }

    [HttpDelete]
    public async Task<ActionResult<Rol>> DeleteRolAsync(Guid rolId)
    {
        try
        {
            var rolDelete = await _context.Roles.FirstOrDefaultAsync(x => x.RolId == rolId);
            if (rolDelete == null)
            {
                return NotFound(new { message = "Rol no encontrado, debe proporciona uno valido" });
            }
            _context.Roles.Remove(rolDelete);
            await _context.SaveChangesAsync();
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Ocurrio un error al intentar eliminar el rol", details = ex.Message });
        }
    }

    [HttpPut("nombre")]
    public async Task<ActionResult<Rol>> PutRolAsync(Guid rolId, string nombre)
    {
        try
        {
            var rolUpdate = await _context.Roles.FirstOrDefaultAsync(x => x.RolId == rolId);
            if (rolUpdate == null)
            {
                return NotFound(new { message = "Rol no encontrado, debe proporciona uno valido" });
            }
            
            rolUpdate.Nombre = nombre;
            await _context.SaveChangesAsync();

            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Ocurrio un error al intentar actualizar el rol", details = ex.Message });
        }
    }
}
