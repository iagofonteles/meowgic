using System.Collections.Generic;

namespace Meowgic.Match
{
    public class TurnArgs
    {
        public IReadOnlyList<SpellCastArgs> AllyCasts { get; }
        public IReadOnlyList<SpellCastArgs> EnemyCasts { get; }

        public TurnArgs(IReadOnlyList<SpellCastArgs> allyCasts,
            IReadOnlyList<SpellCastArgs> enemyCasts)
        {
            AllyCasts = allyCasts;
            EnemyCasts = enemyCasts;
        }
    }
}