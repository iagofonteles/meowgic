using Meowgic.Match;

namespace Meowgic
{
    public static class Game
    {
        public static Player Player { get; set; } = new();
        public static Battle CurrentBattle { get; set; }
    }
}