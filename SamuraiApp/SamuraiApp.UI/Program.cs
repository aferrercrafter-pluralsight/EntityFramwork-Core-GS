using SamuraiApp.Domain;
using SamuraiApp.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace SamuraiApp.UI
{
    class Program
    {
        private static SamuraiContext _context = new SamuraiContext();

        static void Main(string[] args)
        {
            _context.Database.EnsureCreated();
            _context.GetService<ILoggerFactory>().AddProvider(new MyLoggerProvider());
            AddManyToManyWithObject();
            Console.ReadLine();
        }

        private static void InsertNewPkFkGraph()
        {
            var samurai = new Samurai
            {
                Name = "Saitama",
                Quotes = new List<Quote>() { new Quote() { Text = "U maiba shinderu"} }
            };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }
        private static void InsertNewOneToOneGraph()
        {
            var samurai = new Samurai
            {
                Name = "Endevor",
                SecretIdentity = new SecretIdentity() { RealName = "Yagamy" }
            };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }
        private static void AddChildToExistingObject()
        {
            var samurai = _context.Samurais.First();            
            _context.Samurais.Include(s => s.Quotes).FirstOrDefault().Quotes.Add(new Quote { Text = "Gomu gomu nooo Jet Pistol" });            
            _context.SaveChanges();
        }
        private static void AddOneToOneToExistingObjectWhileTracked()
        {
            var samurai = _context.Samurais.FirstOrDefault(s => s.SecretIdentity == null);
            samurai.SecretIdentity = new SecretIdentity() { RealName = "Yagamy 1" };
            _context.SaveChanges();
        }                       

        private static void AddBattles()
        {
            _context.Battles.AddRange(                 
                    new Battle() {Name = "Battle of Shiroyama", StartDate = new DateTime(1877, 9, 24), EndDate = new DateTime(1877, 9, 24)},
                    new Battle() {Name = "Siegue of Osaka", StartDate = new DateTime(1614, 1, 1), EndDate = new DateTime(1615, 12, 30)},
                    new Battle() {Name = "Boshin War", StartDate = new DateTime(1868, 1, 1), EndDate = new DateTime(1869, 1, 1)}               
                );
            _context.SaveChanges();
        }
        private static void AddManyToManyWithFKs()
        {
            _context.SamuraiBattles.Add(
                new SamuraiBattle()
                {
                    SamuraiId = 1,
                    BattleId = 1
                }
                );
            _context.SaveChanges();
        }
        private static void AddManyToManyWithObject()
        {
            var samurai = _context.Samurais.FirstOrDefault();
            var battle = _context.Battles.FirstOrDefault();

            _context.SamuraiBattles.Add(
                new SamuraiBattle() { Samurai = samurai, Battle = battle}
                );
            _context.SaveChanges();
        }

    }
}
