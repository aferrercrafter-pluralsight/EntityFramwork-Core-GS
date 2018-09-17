using System.Collections.Generic;

namespace SamuraiAppCore.Domain
{
    public class Samurai
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Quote> Quotes { get; set; }

        public int BattleId { get; set; }
    }
}
