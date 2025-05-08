using System.Collections.Generic;
using System.Linq;

namespace Meowgic.Match
{
    public static class BattleUtility
    {
        public static bool IsValidCast(this SpellCastArgs args)
        {
            if (!args.spell) return false;
            for (var i = 0; i < args.Cost.Count; i++)
            {
                var used = args.catalysts.ElementAtOrDefault(i);
                var cost = args.Cost[i];
                if (cost.Compatible(used)) continue;
                return false;
            }

            return true;
        }

        public static IEnumerable<(SpellCastArgs, int, TurnArgs)> EnumerateCasts(this Battle battle)
        {
            var player = battle.PlayerSide.TurnArgs;
            var enemy = battle.EnemySide.TurnArgs;
            var castIndex = 0;
            Execute:

            var pCast = player.AllyCasts.ElementAtOrDefault(castIndex);
            var eCast = enemy.AllyCasts.ElementAtOrDefault(castIndex);
            if (pCast is null && eCast is null) yield break;

            var playerFirst = (pCast?.speed ?? 0) >= (eCast?.speed ?? 0);
            var (a, aArgs) = playerFirst ? (pCast, player) : (eCast, enemy);
            var (b, bArgs) = playerFirst ? (eCast, enemy) : (pCast, player);

            if (a is not null) yield return (a, castIndex, aArgs);
            if (b is not null) yield return (b, castIndex, bArgs);

            castIndex++;
            goto Execute;
        }

        public static IEnumerable<EffectScript> EnumerateEffects(this SpellCastArgs cast)
            => cast.spell.Effects.Concat(cast.catalysts
                .SelectMany(c => c.Effects).Where(e => e).Select(e => e.Script));
    }
}