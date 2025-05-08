using System;
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

            // reset preparation
            battle.ResetPreparations();

            // enemy preparation
            var index = 0;
            foreach (var actor in battle.EnemySide.Actors)
            {
                for (var i = 0; i < actor.CastAmount.Value; i++)
                {
                    var args = battle.EnemySide.Casts[index++];
                    args.spell = actor.Ai(actor);
                    args.target = battle.Player;
                }
            }
        }

        public void ConfirmPreparation()
        {
            // refund unused catalysts
            foreach (var args in battle.PlayerSide.Casts.Where(c => !c.IsValidCast()))
                battle.PlayerSide.Pool.Hand.AddRange(args.catalysts.Where(c => c));

            // set target
            var target = battle.EnemySide.Actors.First(a => !a.IsDead);
            foreach (var args in battle.PlayerSide.Casts)
                args.target = target;

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

            battle.RecalculateCasts();

            foreach (var (cast, index, turnArgs) in battle.EnumerateCasts())
            {
                if (cast.cancelled) continue;

                foreach (var effect in cast.EnumerateEffects())
                    effect.OnTurnExecute(index, turnArgs);

                BattleMath.Heal(cast.Caster.Side.Actors, cast.heal);
                BattleMath.Shield(cast.Caster.Side.Actors, cast.shield);
                BattleMath.Damage(cast.Caster, cast.target, cast.damage);
            }

            foreach (var (cast, index, turnArgs) in battle.EnumerateCasts())
            foreach (var effect in cast.EnumerateEffects())
                effect.OnTurnEnd(index, turnArgs);

            return battle.GetBattleResult();
        }
    }
}