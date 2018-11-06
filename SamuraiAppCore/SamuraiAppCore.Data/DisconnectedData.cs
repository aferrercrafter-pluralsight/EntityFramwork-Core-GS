using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SamuraiAppCore.Domain;
using System.Collections.Generic;
using System.Linq;

namespace SamuraiAppCore.Data
{
    public class DisconnectedData
    {
        private SamuraiContext _context;

        public DisconnectedData (SamuraiContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        //For Dropdown List, More efficent across the web
        public List<KeyValuePair<int,string>> GetSamuraiReferenceList()
        {
            var samurais = _context.Samurais.OrderBy(s => s.Name)
                .Select(s => new { s.Id, s.Name })
                .ToDictionary(t => t.Id, t => t.Name)
                .ToList();
            return samurais;
        }

        public Samurai LoadSamuraiGraph(int id)
        {
            var samurai = _context.Samurais
                .Include(s => s.SecretIdentity)
                .Include(s => s.Quotes)
                .FirstOrDefault(s => s.Id == id);
            return samurai;
        }

        public void SaveSamuraiGraph(Samurai samurai)
        {
            _context.ChangeTracker.TrackGraph(samurai, e => ApplyStateUsingIsKeySet(e.Entry));
            _context.SaveChanges();
        }

        private static void ApplyStateUsingIsKeySet(EntityEntry entry)
        {
            if (entry.IsKeySet)
            {
                if (((ClientChangeTracker)entry.Entity).IsDirty)
                {
                    entry.State = EntityState.Modified;
                }
                else
                {
                    entry.State = EntityState.Unchanged;
                }
            }
            else
            {
                entry.State = EntityState.Added;
            }
        }

        public void DeleteSamuraiGraph(int id)
        {
            //goal: delete samurai, quotes, secret identity and any joins with battles
            //EF Core support Cascade Delete by convention
            //Even if full Graph is not in memory, db is defined to delete
            //But always doble check
            var samurai = _context.Samurais.Find(id); //NoTracking
            _context.Entry(samurai).State = EntityState.Deleted;
            _context.SaveChanges();            
        }
    }
}
