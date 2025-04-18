using System.Collections.Generic;
using Meowgic.Match.UI;
using UnityEngine;

namespace Meowgic.Match
{
    public class BattleSetup : MonoBehaviour
    {
        [SerializeField] private BattlerController controller;
        [SerializeField] private BattleView battleView;

        public Player player;
        public List<Enemy> enemies;

        public void StartBattle()
        {
            var battle = new Battle(
                new IActor[] { player }, player.Inventory,
                enemies, new List<Catalyst>()
            );
            
            battleView.SetData(battle);
            controller.StarBattle(battle);
        }
    }
}