namespace SamuraiAppCore.Domain
{
    public class SamuraiBattle : ClientChangeTracker
    {
        public int BattleId { get; set; }
        public Battle Battle { get; set; }
        public int SamuraiId { get; set; }
        public Samurai Samurai { get; set; }

    }
}
