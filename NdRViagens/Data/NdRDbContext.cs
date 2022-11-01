using Microsoft.EntityFrameworkCore;
using NdRViagens.Models;

namespace NdRViagens.Data
{
    public class NdRDbContext :DbContext
    {
        public NdRDbContext(DbContextOptions<NdRDbContext> options) 
            : base(options)
        { }

        public DbSet<Destino> Destino { get; set; }

        public DbSet<PacoteViagem> PacoteViagem { get; set; }
    }
}
