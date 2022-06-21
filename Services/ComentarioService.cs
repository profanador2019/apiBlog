using apiBlog.Repository;
using apiBlog.Models.Clases;

namespace apiBlog.Services;

public class ComentarioService
{
    private readonly ComentarioRepository _CRepository;
    private readonly PostRepository _PRepository;
    public ComentarioService(ComentarioRepository CRepository, PostRepository PRepository)
    {
        _CRepository = CRepository;
        _PRepository = PRepository;
    }

    public async Task<ICollection<Comentario>> GetComentariosDePost(int idPost)
    {
        var lista = await _CRepository.GetComentarios();

        return lista.Where(l => l.Post.Id == idPost).ToList();
    }

    public async Task NewComentario(Comentario c)
    {
        var post = await _PRepository.GetById(c.Post.Id);

        if(post is null)
        {
            throw new Exception("No existe post");
        }

        Comentario com = new Comentario();
        
        com.Nombre = c.Nombre;
        com.Cuerpo = c.Cuerpo;
        com.Post = post;
        com.Fecha = DateTime.Now;

        await _CRepository.AddComentario(com);
        
    }

    public async Task DeleteComentario(int id)
    {
        var com = await _CRepository.GetById(id);

        if(com is null)
        {
            throw new Exception("No existe comentario");
        }

        await _CRepository.Delete(com);
    }
}