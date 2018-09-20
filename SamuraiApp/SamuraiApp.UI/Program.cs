using SamuraiApp.Domain;
using SamuraiApp.Data;

namespace SamuraiApp.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            InsertSamurai();
        }

        public static void InsertSamurai()
        {
            var samurai = new Samurai { Name = "Julie" };
            using (var context = new SamuraiContext())
            {
                context.Samurais.Add(samurai);
                //contexxt.Add(samurai);
                context.SaveChanges();
            }
        }
    }
}
