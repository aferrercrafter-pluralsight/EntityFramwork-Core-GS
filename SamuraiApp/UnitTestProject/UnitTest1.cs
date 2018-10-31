using System;
using System.Diagnostics;
using System.Linq;
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
            using (var context = new SamuraiContext())
            {
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
            using (var context = new SamuraiContext())
            {
                var samurai = new Samurai();
                samurai.Name = "Zoro";
                context.Add(samurai);
                context.SaveChanges();
            }
            using (var context2 = new SamuraiContext())
            {
                Assert.AreEqual(1, context2.Samurais.Count());
            }
        }
    }
}
