using Microsoft.EntityFrameworkCore.ChangeTracking;
using SamuraiApp.Domain;
using System.Collections.Generic;
using System.Linq;

namespace SamuraiApp.Data
{
    public class ConnectedData
    {
        private SamuraiContext _context;

        public ConnectedData()
        {
            _context = new SamuraiContext();
        }

        public IEnumerable<Samurai> SamuraisListInMemory()
        {
            if (_context.Samurais.Local.Count == 0)
            {
                _context.Samurais.ToList();
            }
            return _context.Samurais.Local.ToBindingList();
        }

        public Samurai LoadSamuraiGraph(int samuraiId)
        {
            var samurai = _context.Samurais.Find(samuraiId);
            _context.Entry(samurai).Reference(s => s.SecretIdentity).Load();
            _context.Entry(samurai).Collection(s => s.Quotes).Load();

            return samurai;
        }

        public Samurai NewSamurai()
        {
            var samurai = new Samurai() { Name = "New Samurai" };
            _context.Samurais.Add(samurai);
            return samurai;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

    }
}
