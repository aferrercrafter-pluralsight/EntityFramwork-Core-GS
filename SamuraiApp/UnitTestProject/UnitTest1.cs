using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamuraiApp.Data;
using SamuraiApp.Domain;

namespace UnitTestProject
{
    [TestClass]
    public class SamuraisTest
    {
        [TestMethod]
        public void CanInsertSamuraiIntoDatabase()
        {
            var options = new DbContextOptionsBuilder();
            options.UseSqlServer(ConfigurationManager.ConnectionStrings["testingDb"].ConnectionString);
            using (var context = new SamuraiContext(options.Options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                var samurai = new Samurai();
                Debug.WriteLine($"Default Samurai Id {samurai.Id}");
                context.Samurais.Add(samurai);
                var efDefaultId = samurai.Id;
                Debug.WriteLine($"EF Default Samurai Id {efDefaultId}");
                context.SaveChanges();
                Debug.WriteLine($"DB Assigned Samurai Id {samurai.Id}");
                Assert.AreNotEqual(efDefaultId, samurai.Id);
            }
        }
        [TestMethod]
        public void CanInsertSamuraiWithSaveChanges()
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseInMemoryDatabase(databaseName: "CanInsertSamuraiWithSaveChanges");
            using (var context = new SamuraiContext(optionsBuilder.Options))
            {
                var samurai = new Samurai();
                samurai.Name = "Zoro";
                context.Add(samurai);
                context.SaveChanges();
            }
            using (var context2 = new SamuraiContext(optionsBuilder.Options))
            {
                Assert.AreEqual(1, context2.Samurais.Count());
            }
        }
    }
}
