using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
    public async Task<IActionResult> Get() {
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
    public async Task<IActionResult> Get(int EventoId) {
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
    public async Task<IActionResult> Get(string tema) {
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
    public async Task<IActionResult> Post([FromBody] Evento model) {
      try
      {
          _repo.Add(model);
          return Ok();
      }
      catch (Exception error)
      {
        return BadRequest("Sistema Falhou: " + error.Message);
      }
    }
  }
}
