using UnityEngine;

namespace Meowgic.Match.Spells
{
    public class Default : Effect
    {
        [SerializeField] private int damage;
        [SerializeField] private int heal;
        [SerializeField] private int shield;
        [SerializeField] private bool area;

        public override void OnTurnBegin(int castIndex, TurnArgs turnArgs)
        {
            var args = turnArgs.AllyCasts[castIndex];
            if (args.caster.IsDead) return;

            args.damage += damage;
            args.heal += heal;
            args.shield += shield;
        }
    }
}