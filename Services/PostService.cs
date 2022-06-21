using apiBlog.Models.Clases;
using apiBlog.Repository;
using apiBlog.Models.Enum;

namespace apiBlog.Services;

public class PostService
{
    private readonly PostRepository _PRepository;
    private readonly UsuariosRepository _URepository;
    public PostService(PostRepository PRepository, UsuariosRepository URepository)
    {
        _PRepository = PRepository;
        _URepository = URepository;
    }

    public async Task<ICollection<Post>> GetPosts(Usuario u)
    {
        var usuario = await _URepository.GetById(u.Id);
        var posts = await _PRepository.GetAll();

        if(usuario is null)
        {
            return posts.Where(p => p.Estado == EstadoPost.Publicado).ToList();
        }

        if(usuario.Rol.TipoRol == TipoRol.Escritor)
        {
            return posts.Where(p => p.Autor == usuario).ToList();
        }

        return posts.Where(p => p.Estado == EstadoPost.Creado || p.Estado == EstadoPost.Pendiente)
        .ToList();
        
    }

    public async Task<Post> GetPostById(Usuario u, int id)
    {
        var usuario = await _URepository.GetById(u.Id);
        var post = await _PRepository.GetById(id);

        if(post is null)
        {
            throw new Exception("No se encontro el post solicitado");
        }

        if(usuario is null)
        {
            if(post.Estado == EstadoPost.Publicado)
            {
                return post;
            }
            else
            {
                throw new Exception("Su usuario no esta autorizado a obtener post");
            }
        }

        if(usuario.Rol.TipoRol == TipoRol.Escritor)
        {
            if(post.Autor == usuario)
            {
                return post;
            }
            else
            {
                if(post.Estado == EstadoPost.Publicado)
                {
                    return post;
                }
                else
                {
                    throw new Exception("Su usuario no esta autorizado a obtener post");
                }
            }
        }

        if(!(post.Estado == EstadoPost.Rechazado))
        {
            return post;
        }
        else
        {
            throw new Exception("Su usuario no esta autorizado a obtener post");
        }
    }

    public async Task CreatePost(Post p)
    {
        var usuario = await _URepository.GetById(p.Autor.Id);

        if(usuario is null)
        {
            throw new Exception("No se encontro el usuario");
        }

        if(usuario.Rol.TipoRol == TipoRol.Escritor)
        {
            var post = new Post()
            {
                Titulo = p.Titulo,
                F_creacion = DateTime.Now,
                Cuerpo = p.Cuerpo,
                Estado = EstadoPost.Creado,
                Autor = usuario
            };

            await _PRepository.AddPost(post);
        }
        else
        {
            throw new Exception("No esta autorizado a crear post");
        }
    }

    public async Task EditPost(Post p)
    {
        var usuario = await _URepository.GetById(p.Autor.Id);
        var post = await _PRepository.GetById(p.Id);

        if(usuario is null)
        {
            throw new Exception("Usuario no encontrado");
        }

        if(post is null)
        {
            throw new Exception("No se encontro el post");
        }

        if(usuario.Rol.TipoRol == TipoRol.Escritor)
        {
            if(post.Estado == EstadoPost.Rechazado)
            {
                post.Titulo = p.Titulo;
                post.F_actualizacion = DateTime.Now;
                post.Cuerpo = p.Cuerpo;
                post.Estado = EstadoPost.Pendiente;

                await _PRepository.Update();
            }
            else
            {
                throw new Exception("No posee permiso para editar");
            }
        }
        else
        {
            if(post.Estado == EstadoPost.Creado || post.Estado == EstadoPost.Pendiente)
            {
                post.Revisor = usuario;
                post.Estado = p.Estado;

                if(p.Estado == EstadoPost.Publicado)
                {
                    post.F_publicacion = DateTime.Now;
                }

                await _PRepository.Update();
            }
            else
            {
                throw new Exception("No posee permiso para editar");
            }
        }

    }

    public async Task DeletePost(Usuario u, int id)
    {
        var usuario = await _URepository.GetById(u.Id);
        var post = await _PRepository.GetById(id);

        if(usuario is null)
        {
            throw new Exception("No se encontro el usuario");
        }

        if(post is null)
        {
            throw new Exception("No se encontro el post a eliminar");
        }

        if(usuario.Rol.TipoRol == TipoRol.Escritor)
        {
            throw new Exception("No posee permisos para eliminar");
        }

        await _PRepository.Delete(post);
    }
}    