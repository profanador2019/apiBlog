namespace apiBlog.Models.Clases;

public class Comentario
{
    public int Id {get; set;}
    public DateTime Fecha {get; set;}
    public string Cuerpo {get; set;}
    public string Nombre {get; set;}
    public Post Post {get; set;}

}