using Microsoft.EntityFrameworkCore;
using SamuraiApp.Domain;

namespace SamuraiApp.Data
{
    public class SamuraiContext : DbContext
    {
        public DbSet<Samurai> Samurais;
        public DbSet<Quote> Quotes;
        public DbSet<Battle> Battles;
    }
}
