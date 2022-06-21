using apiBlog.Models.Clases;
using apiBlog.Repository;

namespace apiBlog.Services;

public class UsuarioService
{
    private readonly UsuariosRepository _URepository;
    private readonly RolesRepository _RRepository;
    public UsuarioService(UsuariosRepository URepository, RolesRepository RRepository)
    {
        _URepository = URepository;
        _RRepository = RRepository;
    }

    public async Task<ICollection<Usuario>> GetUsuarios()
    {
        return await _URepository.GetAll();
    }

    public async Task<Usuario?> GetUsuarioById(int id)
    {
        return await _URepository.GetById(id);
    }

    public async Task<Usuario> AddUsuario(Usuario u)
    {
        var usuarios = GetUsuarios();

        foreach(Usuario usu in usuarios.Result)
        {
            if(u.Nombre == usu.Nombre)
            {
                throw new Exception("El nombre de usuario ya existe");
            }
        }

        var rol = await _RRepository.GetById(u.Rol);

        if(rol is null)
        {
            throw new Exception("Rol no encontrado");
        }

        var nuevoUsuario = new Usuario()
        {
            Nombre = u.Nombre,
            Rol = rol
        };

        return await _URepository.Add(nuevoUsuario);

    }

    public async Task<Usuario> EditUsuario(Usuario u)
    {
        var usu = await _URepository.GetById(u.Id);

        if(usu is null)
        {
            throw new Exception("Usuario no encontrado");
        }

        var r = await _RRepository.GetById(u.Rol);

        if(r is null)
        {
            throw new Exception("Rol no encontrado");
        }
        
        usu.Nombre = u.Nombre;
        usu.Rol = r;
        await _URepository.Update();
        return u;
    }

    public async Task DeleteUsuario(int id)
    {
        var usu = await GetUsuarioById(id);

        if(usu is null)
        {
            throw new Exception("Usuario a borrar no encontrado");
        }

        await _URepository.Delete(usu);
    }
}