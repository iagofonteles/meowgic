using System.Collections.Generic;
using System.Linq;

namespace Meowgic.Match
{
    public class TurnArgs
    {
        private readonly Side side;
        public IEnumerable<SpellCastArgs> AllyCasts => side.Casts.Where(BattleUtility.IsValidCast);
        public IEnumerable<SpellCastArgs> EnemyCasts => side.OtherSide.Casts.Where(BattleUtility.IsValidCast);
        public TurnArgs(Side side) => this.side = side;
    }
}