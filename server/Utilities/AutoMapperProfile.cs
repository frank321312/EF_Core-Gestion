using AutoMapper;
using server.Models.DTOs;
using server.Models.Entities;

namespace server.Utilities;

public class AutoMapperProfile: Profile
{
    public AutoMapperProfile()
    {
        CreateMap<RolDTO, Rol>();
        CreateMap<UsuarioDTO, Usuario>();
        CreateMap<ProyectoDTO, Proyecto>();
        CreateMap<UsuarioQueryDTO, Usuario>();
        CreateMap<UsuarioProyectoQueryDTO, Usuario>();
    }
}