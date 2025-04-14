using System;
using System.Collections.Generic;
using System.Linq;

namespace Meowgic.Match
{
    public partial class Battle
    {
        public BattleResult ExecuteTurn()
        {
            if (playerSide.Casts is null || enemySide.Casts is null)
                throw new Exception("Spell cast input missing");

            var player = new TurnArgs(playerSide.Casts, enemySide.Casts);
            var enemy = new TurnArgs(enemySide.Casts, playerSide.Casts);

            foreach (var (cast, index, turnArgs) in EnumerateCasts(player, enemy))
            foreach (var effect in EnumerateEffects(cast))
                effect.OnTurnBegin(index, turnArgs);

            foreach (var (cast, index, turnArgs) in EnumerateCasts(player, enemy))
            {
                BattleMath.Heal(cast.caster.Side.Actors, cast.heal);
                BattleMath.Shield(cast.caster.Side.Actors, cast.shield);
                BattleMath.Damage(cast.caster, cast.target, cast.damage);

                foreach (var effect in EnumerateEffects(cast))
                    effect.OnTurnExecute(index, turnArgs);
            }
            
            foreach (var (cast, index, turnArgs) in EnumerateCasts(player, enemy))
            foreach (var effect in EnumerateEffects(cast))
                effect.OnTurnEnd(index, turnArgs);

            return GetBattleResult();
        }

        private static IEnumerable<(SpellCastArgs, int, TurnArgs)> EnumerateCasts(TurnArgs player, TurnArgs enemy)
        {
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

        private static IEnumerable<Effect> EnumerateEffects(SpellCastArgs cast)
        {
            return cast.catalysts.SelectMany(c => c.Effects).Concat(cast.spell.Effects);
        }
    }
}