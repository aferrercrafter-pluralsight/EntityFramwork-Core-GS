using System.Collections.Generic;

namespace SamuraiAppCore.Domain
{
    public class Samurai
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Quote> Quotes { get; set; }
        public SecretIdentity SecretIdentity { get; set; }
        public List<SamuraiBattle> SamuraiBattles { get; set; }
    }
}
