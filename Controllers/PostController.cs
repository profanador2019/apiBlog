using Microsoft.AspNetCore.Mvc;
using apiBlog.Services;
using apiBlog.Models.Clases;

namespace apiBlog.Controllers;


[ApiController]
[Route("[controller]")]
public class PostController: ControllerBase
{
    PostService _service;
    public PostController(PostService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult> GetPosts(Usuario u)
    {
        return Ok(await _service.GetPosts(u));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(Usuario u, int id)
    {
        return Ok(await _service.GetPostById(u,id));
    }

    [HttpPost]
    public async Task<ActionResult> NewPost(Post p)
    {
        await _service.CreatePost(p);
        return Created("Creado",null);
    }

    [HttpPut]
    public async Task<ActionResult> EditPost(Post p)
    {
        await _service.EditPost(p);
        return Created("Editado",null);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePost(Usuario u, int id)
    {
        await _service.DeletePost(u,id);
        return Ok();
    }
}