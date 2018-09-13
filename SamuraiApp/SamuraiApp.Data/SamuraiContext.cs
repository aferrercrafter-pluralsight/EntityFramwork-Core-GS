using Microsoft.EntityFrameworkCore;
using SamuraiApp.Domain;

namespace SamuraiApp.Data
{
    public class SamuraiContext : DbContext
    {
        public DbSet<Samurai> Samurais;
        public DbSet<Quote> Quotes;
        public DbSet<Battle> Battles;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server = (localdb\\mssqllocaldb; Database = SamuraiData; Trusted_Connection = True; )");
        }
    }
}
