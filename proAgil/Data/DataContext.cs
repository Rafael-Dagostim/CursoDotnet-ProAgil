using Microsoft.EntityFrameworkCore;
using proAgil.Model;

namespace proAgil.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options) {}

        public DbSet<Evento> Eventos { get; set; }
    }
}