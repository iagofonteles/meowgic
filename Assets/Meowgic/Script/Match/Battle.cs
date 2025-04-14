using System;
using System.Collections.Generic;
using System.Linq;
using Drafts;
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
        [SerializeField] private Side playerSide;
        [SerializeField] private Side enemySide;
        [SerializeField] private ObservableList<SpellPreparation> preparation = new();

        public Side PlayerSide => playerSide;
        public Side EnemySide => enemySide;
        public Actor Player => PlayerSide.Actors[0];
        public ObservableList<SpellPreparation> Preparation => preparation;

        public Battle(
            IEnumerable<IActor> players, IEnumerable<Catalyst> playerInventory,
            IEnumerable<IActor> enemies, IEnumerable<Catalyst> enemyInventory)
        {
            playerSide = new(this, players, playerInventory);
            enemySide = new(this, enemies, enemyInventory);
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