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
        static void Main(string[] args)
        {
            SimpleSamurayQuery();
            Console.ReadLine();
        }

        private static void InsertSamurai()
        {
            var samurai = new Samurai { Name = "Julie" };
            using (var context = new SamuraiContext())
            {
                context.GetService<ILoggerFactory>().AddProvider(new MyLoggerProvider());
                context.Samurais.Add(samurai);
                //contexxt.Add(samurai);
                context.SaveChanges();
            }
        }
        private static void InsertMultipleSamurais()
        {
            var samurai = new Samurai { Name = "Brook" };
            var samurai2 = new Samurai { Name = "Zoro" };
            var samurai3 = new Samurai { Name = "Pica" };
            var samurai4 = new Samurai { Name = "Gintama" };

            using (var context = new SamuraiContext())
            {
                context.GetService<ILoggerFactory>().AddProvider(new MyLoggerProvider());
                //context.AddRange(samurai, samurai2);
                context.Samurais.AddRange(new List<Samurai>() { samurai, samurai2, samurai3, samurai4 });                
                context.SaveChanges();
            }
        }
        private static void SimpleSamurayQuery()
        {
            using (var context = new SamuraiContext())
            {
                var samurais = context.Samurais.ToList();

                var query = context.Samurais;
                //var samurais = query.ToList()

                foreach (var samurai in query)
                {
                    Console.WriteLine(samurai.Name);
                }
                //foreach (var samurai in context.Samurais)
                //{
                    //Console.WriteLine(samurai.Name);
                //}

            }
        }

    }
}
