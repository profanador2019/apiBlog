using apiBlog.Models.Enum;

namespace apiBlog.Models.Clases;

public class Rol
{
    public int Id {get; set;}
    public TipoRol TipoRol {get; set;}
    public string Descripcion {get; set;}

}