using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Meowgic.Match
{
    public enum BattleResult
    {
        None,
        Win,
        Lose
    }

    /// <summary>
    /// Everything the battle state need
    /// </summary>
    [Serializable]
    public class Battle
    {
        [SerializeField] private Side[] sides;

        public IReadOnlyList<Side> Sides => sides;
        public Side PlayerSide => sides[0];
        public Side EnemySide => sides[1];
        public Actor Player => PlayerSide.Actors[0];

        public Battle(
            IEnumerable<IActor> players, IEnumerable<Catalyst> playerInventory,
            IEnumerable<IActor> enemies, IEnumerable<Catalyst> enemyInventory)
        {
            sides = new Side[]
            {
                new(this, players, playerInventory),
                new(this, enemies, enemyInventory),
            };
        }

        public BattleResult GetBattleResult()
        {
            var playerHealth = PlayerSide.Actors.Sum(a => a.Health.Value);
            if (playerHealth == 0) return BattleResult.Lose;
            var enemyHealth = PlayerSide.Actors.Sum(a => a.Health.Value);
            return enemyHealth == 0 ? BattleResult.Win : BattleResult.None;
        }

        public void ResetPreparations()
        {
            foreach (var prep in sides.SelectMany(s => s.Preparation))
                prep.OnChanged -= RecalculateCasts;

            foreach (var side in Sides)
                side.ResetPreparations();

            foreach (var prep in sides.SelectMany(s => s.Preparation))
                prep.OnChanged += RecalculateCasts;
        }

        public void RecalculateCasts()
        {
            foreach (var args in sides.SelectMany(s => s.Casts))
                args.ResetValues();

            foreach (var (cast, index, turnArgs) in this.EnumerateCasts())
            foreach (var effect in cast.EnumerateEffects())
                effect.Setup(index, turnArgs);

            foreach (var args in sides.SelectMany(s => s.Casts))
                args.OnModified?.Invoke();
        }
    }
}