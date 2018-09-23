using SamuraiApp.Domain;
using SamuraiApp.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System;
using System.Linq;

namespace SamuraiApp.UI
{
    class Program
    {
        private static SamuraiContext _context = new SamuraiContext();

        static void Main(string[] args)
        {
            _context.GetService<ILoggerFactory>().AddProvider(new MyLoggerProvider());
            RetrieveAndUpdateMultipleSamurais();
            Console.ReadLine();
        }

        private static void InsertSamurai()
        {
            var samurai = new Samurai { Name = "Julie" };

            _context.GetService<ILoggerFactory>().AddProvider(new MyLoggerProvider());
            _context.Samurais.Add(samurai);
            //_contexxt.Add(samurai);
            _context.SaveChanges();
        }
        private static void InsertMultipleSamurais()
        {
            var samurai = new Samurai { Name = "Brook" };
            var samurai2 = new Samurai { Name = "Zoro" };
            var samurai3 = new Samurai { Name = "Pica" };
            var samurai4 = new Samurai { Name = "Gintama" };
            
            //_context.AddRange(samurai, samurai2);
            _context.Samurais.AddRange(new List<Samurai>() { samurai, samurai2, samurai3, samurai4 });
            _context.SaveChanges();
        }
        private static void SimpleSamurayQuery()
        {
            var samurais = _context.Samurais.ToList();

            var query = _context.Samurais;
            //var samurais = query.ToList()

            foreach (var samurai in query)
            {
                Console.WriteLine(samurai.Name);
            }
            //foreach (var samurai in _context.Samurais)
            //{
            //Console.WriteLine(samurai.Name);
            //}
        }
        private static void MoreQueries()
        {
            var name = "Soro";
            _context.Samurais.FirstOrDefault(s => s.Name == name);
        }
        private static void RetrieveAndUpdateMultipleSamurais()
        {
            var samurais = _context.Samurais.ToList();
            samurais.ForEach(s => s.Name += " San");
            _context.SaveChanges();
        }

    }
}
