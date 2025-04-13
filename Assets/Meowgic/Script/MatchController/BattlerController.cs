using UnityEngine;

namespace Meowgic.Match
{
    public class BattlerController : MonoBehaviour
    {
        [SerializeField] private Battle battle;

        public void StarBattle(Battle battle)
        {
            Game.CurrentBattle = battle;
            this.battle = battle;

            battle.PlayerSide.Pool.HandLimit.Value = 5;
            battle.PlayerSide.Pool.Draw(9);
        }
    }
}