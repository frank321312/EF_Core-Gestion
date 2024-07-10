using Microsoft.EntityFrameworkCore;
using server.Models.Entities;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<Rol>().HasKey(x => x.RolId);
        // modelBuilder.Entity<Usuario>().HasKey(x => x.UsuarioId);
        modelBuilder.Entity<Rol>().HasData(
            new Rol { RolId = Guid.NewGuid(), Nombre = "SuperAdmin" }
        );

        modelBuilder.Entity<Usuario>().HasData(
            new Usuario("Carlos", "carlos12@ŋmail.com")
        );

        modelBuilder.Entity<Proyecto>().HasData(
            new Proyecto("EndPoint")
        );
    }
    
    public DbSet<Rol> Roles => Set<Rol>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Proyecto> Proyectos => Set<Proyecto>();
}