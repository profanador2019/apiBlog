using Microsoft.EntityFrameworkCore;
using apiBlog.Models.Clases;

namespace apiBlog.Repository;

public class Context : DbContext
{
    public Context (DbContextOptions<Context> options)
        : base(options)
    {
    }

    public DbSet<Comentario> Comentarios => Set<Comentario>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<Rol> Roles => Set<Rol>();
}