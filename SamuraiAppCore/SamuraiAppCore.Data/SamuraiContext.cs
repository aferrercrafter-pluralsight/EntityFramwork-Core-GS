using Microsoft.EntityFrameworkCore;
using SamuraiAppCore.Domain;

namespace SamuraiAppCore.Data
{
    public class SamuraiContext : DbContext
    {
        public DbSet<Battle> Battles { get; set; }
        public DbSet<Samurai> Samurais { get; set; }        
        public DbSet<Quote> Quotes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SamuraiBattle>().HasKey(sb => new { sb.BattleId, sb.SamuraiId });
            //modelBuilder.Entity<Samurai>().Property(s => s.SecretIdentity).IsRequired();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Data Source = (localdb)\\MSSQLLocalDB; Database = SamuraiDataCore; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");
        }
    }
}
