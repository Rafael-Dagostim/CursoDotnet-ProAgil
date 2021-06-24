using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;

namespace ProAgil.Repository
{
  public class ProAgilRepository : IProAgilRepository
  {
    private readonly ProAgilContext _context;

    public ProAgilRepository(ProAgilContext context)
    {
      _context = context;
    }

    // GERAL
    public void Add<T>(T entity) where T : class
    {
      _context.Add(entity);
    }

    public void Update<T>(T entity) where T : class
    {
      _context.Update(entity);
    }

    public void Delete<T>(T entity) where T : class
    {
      _context.Remove(entity);
    }

    public async Task<bool> SaveChangesAsync()
    {
      return (await _context.SaveChangesAsync()) > 0;
    }

    // EVENTOS
    public async Task<Evento[]> GetAllEventoAsync(bool includePalestrante = false)
    {
      IQueryable<Evento> query = _context.Eventos
        .Include(x => x.Lotes)
        .Include(x => x.RedesSociais);

      if (includePalestrante)
      {
        query = query
          .Include(e => e.PalestranteEventos)
          .ThenInclude(pe => pe.Palestrante);
      }

      query = query.OrderByDescending(x => x.DataEvento);

      return await query.ToArrayAsync();
    }

    public async Task<Evento[]> GetAllEventoAsyncByTema(string tema, bool includePalestrante = false)
    {
      IQueryable<Evento> query = _context.Eventos
        .Include(x => x.Lotes)
        .Include(x => x.RedesSociais);

      if (includePalestrante)
      {
        query = query
          .Include(e => e.PalestranteEventos)
          .ThenInclude(pe => pe.Palestrante);
      }

      query = query.OrderByDescending(x => x.DataEvento)
        .Where(x => x.Tema.ToLower().Contains(tema.ToLower()));

      return await query.ToArrayAsync();
    }

    public async Task<Evento> GetEventoAsyncById(int eventoId, bool includePalestrante = false)
    {
      IQueryable<Evento> query = _context.Eventos
       .Include(x => x.Lotes)
       .Include(x => x.RedesSociais);

      if (includePalestrante)
      {
        query = query
          .Include(e => e.PalestranteEventos)
          .ThenInclude(pe => pe.Palestrante);
      }

      query = query.OrderByDescending(x => x.DataEvento)
        .Where(x => x.Id == eventoId);

      return await query.FirstOrDefaultAsync();
    }

    // PALESTRANTE
    public async Task<Palestrante[]> GetAllPalestranteAsyncByName(string name, bool includeEvento = false)
    {
      IQueryable<Palestrante> query = _context.Palestentes
        .Include(x => x.RedesSociais);

      if (includeEvento)
      {
        query = query
          .Include(p => p.PalestranteEventos)
          .ThenInclude(pe => pe.Evento);
      }

      query = query.OrderBy(x => x.Nome)
        .Where(x => x.Nome.ToLower().Contains(name.ToLower()));

      return await query.ToArrayAsync();
    }

    public async Task<Palestrante> GetPalestranteAsyncId(int PalestranteId, bool includeEvento = false )
    {
      IQueryable<Palestrante> query = _context.Palestentes
        .Include(x => x.RedesSociais);

      if (includeEvento)
      {
        query = query
          .Include(p => p.PalestranteEventos)
          .ThenInclude(pe => pe.Evento);
      }

      query = query.OrderBy(x => x.Nome)
        .Where(p => p.Id == PalestranteId);

      return await query.FirstOrDefaultAsync();
    }
  }
}