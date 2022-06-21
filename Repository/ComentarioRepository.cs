using apiBlog.Models.Clases;
using Microsoft.EntityFrameworkCore;

namespace apiBlog.Repository;

public class ComentarioRepository
{
    private readonly Context _context;
    public ComentarioRepository(Context context)
    {
        _context = context;
    }

    public async Task<ICollection<Comentario>> GetComentarios()
    {
        return await _context.Comentarios
        .Include(c => c.Post)
        .ToListAsync();
    }

    public async Task AddComentario(Comentario c)
    {
        _context.Comentarios.Add(c);
        await _context.SaveChangesAsync();
    }

    public async Task<Comentario?> GetById(int id)
    {
        return await _context.Comentarios.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task Delete(Comentario c)
    {
        _context.Comentarios.Remove(c);
        await _context.SaveChangesAsync();
    }
}