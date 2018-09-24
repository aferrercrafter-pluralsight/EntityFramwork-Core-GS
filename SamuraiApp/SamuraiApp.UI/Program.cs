using SamuraiApp.Domain;
using SamuraiApp.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SamuraiApp.UI
{
    class Program
    {
        private static SamuraiContext _context = new SamuraiContext();

        static void Main(string[] args)
        {
            _context.GetService<ILoggerFactory>().AddProvider(new MyLoggerProvider());
            RawSqlQuery();
            Console.ReadLine();
        }

        #region Insert Samurais
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
        #endregion
        #region Queys Samurais
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
        #endregion
        #region Delete Samurais
        private static void DeleteWhileTracked()
        {
            var samurai = _context.Samurais.FirstOrDefault();
            _context.Remove(samurai);
            //alternatives:
            //_context.Remove(samurai);
            //_context.Entry(samurai).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            //_context.Samurais.Remove(_context.Samurais.Find(1));
            _context.SaveChanges();
        }
        private static void DeleteMany()
        {
            var samurais = _context.Samurais.Where(x => x.Name.Contains("Zoro")).ToList();
            _context.Samurais.RemoveRange(samurais);
            _context.SaveChanges();
        }
        private static void DeleteWhileNotTracked()
        {
            var samurai = _context.Samurais.FirstOrDefault();
            using (var contextNewAppInstance = new SamuraiContext())
            {
                contextNewAppInstance.Samurais.Remove(samurai);
                contextNewAppInstance.SaveChanges();
            }
        }
        #endregion
        
        private static void RetrieveAndUpdateMultipleSamurais()
        {
            var samurais = _context.Samurais.ToList();
            samurais.ForEach(s => s.Name += " San");
            _context.SaveChanges();
        }

        private static void InsertBattle()
        {
            var battle = new Battle
            {
                Name = "Mount Fuji",
                StartDate = DateTime.Now.AddYears(-100),
                EndDate = DateTime.Now.AddYears(-80)
            };
            _context.Battles.Add(battle);
            _context.SaveChanges();
        }
        private static void QueryAndUpdateDisconnectedBattle()
        {
            var battle = _context.Battles.FirstOrDefault();
            battle.Name += " battle";
            using (var contextNewAppInstance = new SamuraiContext())
            {
                contextNewAppInstance.Battles.Update(battle);
                contextNewAppInstance.SaveChanges();
            }
        }

        #region Raw Sql
        private static void RawSqlQuery()
        {
            var samurais = _context.Samurais.FromSql("SELECT * FROM Samurais").OrderByDescending(s => s.Name).Where(s=>s.Name.Contains("San")).ToList();
            samurais.ForEach(s => Console.WriteLine(s.Name));
        }
        #endregion



    }
}
