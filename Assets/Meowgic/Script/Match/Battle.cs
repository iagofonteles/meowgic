using System;
using System.Collections.Generic;
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

        public BattleResult ExecutePlayerAction(IEnumerable<SpellCast> spells)
        {
            //TODO execute spells and see if game is over
            throw new NotImplementedException();
        }
    }
}