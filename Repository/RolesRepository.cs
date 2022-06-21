using apiBlog.Models.Clases;
using Microsoft.EntityFrameworkCore;

namespace apiBlog.Repository;

public class RolesRepository
{
    private readonly Context _context;
    public RolesRepository(Context context)
    {
        _context = context;
    }

    public async Task<Rol?> GetById(Rol rol)
    {
        return await _context.Roles.FirstOrDefaultAsync(r => r.Id == rol.Id);
    }
}