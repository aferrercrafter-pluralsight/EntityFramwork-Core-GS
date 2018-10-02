using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using SamuraiApp.Data;
using SamuraiApp.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace SamuraiApp.UI
{
    public class Module4Method
    {
        private static SamuraiContext _context = new SamuraiContext();

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
            //var samurais = _context.Samurais.FromSql("SELECT * FROM Samurais").OrderByDescending(s => s.Name).Where(s=>s.Name.Contains("San")).ToList();
            var samurais = _context.Samurais.FromSql("SELECT * FROM Samurais").OrderByDescending(s => s.Name).ToList();
            samurais.ForEach(s => Console.WriteLine(s.Name));
        }
        private static void RawSqlSpCall()
        {
            var namepart = "san";
            var samurais = _context.Samurais.FromSql("EXEC FilterSamuraiByNamepart {0}", namepart).OrderByDescending(s => s.Name).ToList();
            samurais.ForEach(s => Console.WriteLine(s.Name));

        }
        private static void QueryWithNonSql()
        {
            var samurais = _context.Samurais.Select(s => new { Name = ReverseString(s.Name) }).ToList();
            samurais.ForEach(s => Console.WriteLine(s.Name));
        }
        private static void RawSqlCommand()
        {
            var affected = _context.Database.ExecuteSqlCommand(
                "UPDATE Samurais SET Name = REPLACE(Name,'San','Nan')");
            Console.WriteLine("Rows affecred {0}", affected);

        }
        private static void RawSqlCommandWithOutput()
        {
            var procResult = new SqlParameter()
            {
                ParameterName = @"procResult",
                SqlDbType = System.Data.SqlDbType.VarChar,
                Direction = System.Data.ParameterDirection.Output,
                Size = 50
            };

            _context.Database.ExecuteSqlCommand("EXEC FindLongestName @procResult OUT", procResult);
            Console.WriteLine($"Longest Name: {procResult.Value}");
        }
        #endregion
        private static string ReverseString(string value)
        {
            var stringChar = value.AsEnumerable();
            return string.Concat(stringChar.Reverse());
        }
    }
}
