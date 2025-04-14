using Meowgic.Match;
using UnityEngine;

namespace Meowgic
{
    public static class Game
    {
        private static Database _database;

        public static Database Database => _database ??= Resources.Load<Database>("Database");
        public static Player Player { get; set; } = new();
        public static Battle CurrentBattle { get; set; }
    }
}