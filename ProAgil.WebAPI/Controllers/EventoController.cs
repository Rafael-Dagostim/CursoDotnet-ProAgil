using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain;
using ProAgil.Repository;

namespace ProAgil.WebAPI.Controllers
{
  [ApiController]
  [Route("api/[Controller]")]
  public class EventoController : ControllerBase
  {
    private readonly IProAgilRepository _repo;

    public EventoController(IProAgilRepository repo)
    {
      _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
      try
      {
        var results = await _repo.GetAllEventoAsync(true);
        return Ok(results);
      }
      catch (Exception error)
      {
        return BadRequest("Sistema Falhou: " + error.Message);
      }
    }

    [HttpGet("{EventoId}")]
    public async Task<IActionResult> Get(int EventoId)
    {
      try
      {
        var results = await _repo.GetEventoAsyncById(EventoId, true);
        return Ok(results);
      }
      catch (Exception error)
      {
        return BadRequest("Sistema Falhou: " + error.Message);
      }
    }

    [HttpGet("getByTema/{Tema}")]
    public async Task<IActionResult> Get(string tema)
    {
      try
      {
        var results = await _repo.GetAllEventoAsyncByTema(tema, true);
        return Ok(results);
      }
      catch (Exception error)
      {
        return BadRequest("Sistema Falhou: " + error.Message);
      }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Evento model)
    {
      try
      {
        _repo.Add(model);
        if (await _repo.SaveChangesAsync())
        {
          return Created($"/api/evento/{model.Id}", model);
        }
      }
      catch (Exception error)
      {
        return BadRequest("Sistema Falhou: " + error.Message);
      }

      return BadRequest();
    }

    [HttpPut]
    public async Task<IActionResult> Put(int eventoId, [FromBody] Evento model)
    {
      try
      {
        var evento = _repo.GetEventoAsyncById(eventoId, false);
        if(evento == null) return NotFound();
        _repo.Update(model);
        if (await _repo.SaveChangesAsync())
        {
          return Created($"/api/evento/{model.Id}", model);
        }
      }
      catch (Exception error)
      {
        return BadRequest("Sistema Falhou: " + error.Message);
      }

      return BadRequest();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int eventoId)
    {
      try
      {
        var evento = _repo.GetEventoAsyncById(eventoId, false);
        if(evento == null) return NotFound();
        _repo.Delete(evento);
        if (await _repo.SaveChangesAsync())
        {
          return Ok();
        }
      }
      catch (Exception error)
      {
        return BadRequest("Sistema Falhou: " + error.Message);
      }

      return BadRequest();
    }
  }
}
