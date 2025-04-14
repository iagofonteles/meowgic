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
    public partial class Battle
    {
        [SerializeField] private Side playerSide;
        [SerializeField] private Side enemySide;

        public Side PlayerSide => playerSide;
        public Side EnemySide => enemySide;
        public Actor Player => PlayerSide.Actors[0];

        public Battle(
            IEnumerable<IActor> players, IEnumerable<Catalyst> playerInventory,
            IEnumerable<IActor> enemies, IEnumerable<Catalyst> enemyInventory)
        {
            playerSide = new(this, players, playerInventory);
            enemySide = new(this, enemies, enemyInventory);
        }

        public void SetupTurn()
        {
            var prep = enemySide.Actors.SelectMany(a => a.Ai(a));
            enemySide.Casts = prep.GetCasts(Player);
            playerSide.Casts = null;
        }

        [Obsolete]
        public void PlayerInput(SpellPreparation[] casts)
        {
            playerSide.Casts = casts.GetCasts(enemySide.Actors.First());
        }

        public BattleResult GetBattleResult()
        {
            var playerHealth = playerSide.Actors.Sum(a => a.Health.Value);
            if (playerHealth == 0) return BattleResult.Lose;
            var enemyHealth = playerSide.Actors.Sum(a => a.Health.Value);
            return enemyHealth == 0 ? BattleResult.Win : BattleResult.None;
        }
    }
}