using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Meowgic.Match
{
    public class BattlerController : MonoBehaviour
    {
        [SerializeField] private Battle battle;
        [SerializeField] private int drawAmount = 9;

        public void StarBattle(Battle newBattle)
        {
            Game.CurrentBattle = newBattle;
            battle = newBattle;
            newBattle.PlayerSide.Pool.HandLimit.Value = drawAmount;

            SetupTurn();
        }

        private void SetupTurn()
        {
            // player draw
            battle.PlayerSide.Pool.DiscardHand();
            battle.PlayerSide.Pool.Draw(drawAmount);

            // enemy preparation
            var prep = battle.EnemySide.Actors.SelectMany(a => a.Ai(a));
            battle.EnemySide.Casts = prep.GetCasts(battle.Player);

            // player preparation
            battle.PlayerSide.Casts = null;
            battle.Preparation.Clear();
            var amount = battle.PlayerSide.Actors.Sum(a => a.CastAmount.Value);
            for (var i = 0; i < amount; i++)
                battle.Preparation.Add(new SpellPreparation(battle.Player, battle.Preparation));
        }

        public void ConfirmPreparation()
        {
            var target = battle.EnemySide.Actors.First(a => !a.IsDead);
            battle.PlayerSide.Casts = battle.Preparation.GetCasts(target);

            var result = ExecuteTurn();
            if (result == BattleResult.None)
                SetupTurn();
            else
                Debug.Log(result);
        }

        private BattleResult ExecuteTurn()
        {
            if (battle.PlayerSide.Casts is null || battle.EnemySide.Casts is null)
                throw new Exception("Spell cast input missing");

            var player = new TurnArgs(battle.PlayerSide.Casts, battle.EnemySide.Casts);
            var enemy = new TurnArgs(battle.EnemySide.Casts, battle.PlayerSide.Casts);

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

            return battle.GetBattleResult();
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

        private static IEnumerable<EffectScript> EnumerateEffects(SpellCastArgs cast)
        {
            return cast.catalysts.SelectMany(c => c.Effects.Select(e => e.Script)).Concat(cast.spell.Effects);
        }
    }
}