using Microsoft.AspNetCore.Mvc;
using apiBlog.Services;
using apiBlog.Models.Clases;

namespace apiBlog.Controllers;

[ApiController]
[Route("[controller]")]
public class UsuarioController: ControllerBase
{
    UsuarioService _service;

    public UsuarioController(UsuarioService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        return Ok(await _service.GetUsuarios());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(int id)
    {
        var u = await _service.GetUsuarioById(id);

        if(u is not null)
        {
            return Ok(u);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<ActionResult> Add(Usuario u)
    {
        await _service.AddUsuario(u);
        return Created("Creado",null);
    }

    [HttpPut]
    public async Task<ActionResult> Edit(Usuario u)
    {
        await _service.EditUsuario(u);
        return Created("Editado",null);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _service.DeleteUsuario(id);
        return Ok();
    }


}