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
  public class PalestranteController : ControllerBase
  {
    private readonly IProAgilRepository _repo;

    public PalestranteController(IProAgilRepository repo)
    {
      _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> Get(string name)
    {
      try
      {
        var results = await _repo.GetAllPalestranteAsyncByName(name, true);
        return Ok(results);
      }
      catch (Exception error)
      {
        return BadRequest("Sistema Falhou: " + error.Message);
      }
    }

    [HttpGet("{PalestranteId}")]
    public async Task<IActionResult> Get(int PalestranteId)
    {
      try
      {
        var results = await _repo.GetPalestranteAsyncId(PalestranteId, true);
        return Ok(results);
      }
      catch (Exception error)
      {
        return BadRequest("Sistema Falhou: " + error.Message);
      }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Palestrante model)
    {
      try
      {
        _repo.Add(model);
        if (await _repo.SaveChangesAsync())
        {
          return Created($"/api/palestrante/{model.Id}", model);
        }
      }
      catch (Exception error)
      {
        return BadRequest("Sistema Falhou: " + error.Message);
      }

      return BadRequest();
    }

    [HttpPut]
    public async Task<IActionResult> Put(int palestranteId, [FromBody] Palestrante model)
    {
      try
      {
        var evento = _repo.GetEventoAsyncById(palestranteId, false);
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