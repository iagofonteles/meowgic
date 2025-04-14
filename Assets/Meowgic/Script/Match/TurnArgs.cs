using System.Collections.Generic;

namespace Meowgic.Match
{
    public class TurnArgs
    {
        public IReadOnlyList<SpellCastArgs> AllyCasts { get; }
        public IReadOnlyList<SpellCastArgs> EnemyCast { get; }

        public TurnArgs(IReadOnlyList<SpellCastArgs> allyCasts,
            IReadOnlyList<SpellCastArgs> enemyCast)
        {
            AllyCasts = allyCasts;
            EnemyCast = enemyCast;
        }
    }
}