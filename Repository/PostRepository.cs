using apiBlog.Models.Clases;
using Microsoft.EntityFrameworkCore;

namespace apiBlog.Repository;

public class PostRepository
{
    private readonly Context _context;

    public PostRepository(Context context)
    {
        _context = context;
    }

    public async Task<ICollection<Post>> GetAll()
    {
        return await _context.Posts
        .Include(p => p.Autor)
        .ToListAsync();
    }

    public async Task<Post?> GetById(int id)
    {
        return await _context.Posts
        .Include(p => p.Autor)
        .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddPost(Post p)
    {
        _context.Posts.Add(p);
        await _context.SaveChangesAsync();
    }

    public async Task Update()
    {
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Post p)
    {
        _context.Posts.Remove(p);
        await _context.SaveChangesAsync();
    }
}