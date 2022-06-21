using apiBlog.Models.Enum;

namespace apiBlog.Models.Clases;

public class Post
{
    public int Id {get; set;}
    public string Titulo {get; set;}
    public DateTime F_creacion {get; set;}
    public DateTime F_publicacion {get; set;}
    public DateTime F_actualizacion{get; set;}
    public string Cuerpo {get; set;}
    public EstadoPost Estado {get; set;}
    public Usuario Autor {get; set;}
    public Usuario Revisor {get; set;}

}