using Microsoft.AspNetCore.Mvc;
using apiBlog.Services;
using apiBlog.Models.Clases;

namespace apiBlog.Controllers;

public class ComentarioController: ControllerBase
{
    private readonly ComentarioService _service;
    public ComentarioController(ComentarioService service)
    {
        _service = service;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetComentarios(int idPost)
    {
        return Ok(await _service.GetComentariosDePost(idPost));
    }

    [HttpPost]
    public async Task<ActionResult> AddComentario(Comentario c)
    {
        await _service.NewComentario(c);
        return Created("Creado",null);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteComentario(int id)
    {
        await _service.DeleteComentario(id);
        return Ok();
    }
}