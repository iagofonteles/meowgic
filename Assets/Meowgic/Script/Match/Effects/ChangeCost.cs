using System;
using UnityEngine;

namespace Meowgic.Match.EffectScripts
{
    [Serializable]
    public class ChangeCost : EffectScript
    {
        [SerializeField] private AffectedCasts affectedCasts;
        [SerializeField] private int removeSlots;
        [SerializeField] private CatalystBase[] addSlots;

        public override void Setup(int castIndex, TurnArgs turnArgs)
        {
            foreach (var castArgs in affectedCasts.Resolve(castIndex, turnArgs))
            {
                var min = Math.Min(castArgs.cost.Count, removeSlots);
                for (var i = 0; i < min; i++)
                    castArgs.cost.RemoveAt(castArgs.cost.Count - 1);
                castArgs.cost.AddRange(addSlots);
            }
        }
    }
}