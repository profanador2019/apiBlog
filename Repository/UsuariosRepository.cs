using apiBlog.Models.Clases;
using Microsoft.EntityFrameworkCore;

namespace apiBlog.Repository;

public class UsuariosRepository
{
    private readonly Context _context;

    public UsuariosRepository(Context context)
    {
        _context = context;
    }

    public async Task<ICollection<Usuario>> GetAll()
    {
        return await _context.Usuarios
        .Include(u => u.Rol)
        .ToListAsync();
    }

    public async Task<Usuario?> GetById(int id)
    {
        return await _context.Usuarios
            .Include(u => u.Rol)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<Usuario> Add(Usuario u)
    {
        _context.Usuarios.Add(u);
        await _context.SaveChangesAsync();
        return u;
    }

    public async Task Update()
    {
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Usuario u)
    {
        _context.Usuarios.Remove(u);
        await _context.SaveChangesAsync();
    }
}