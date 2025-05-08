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
                for (var i = 0; i < removeSlots; i++)
                    if (castArgs.Cost.Count > 0)
                        castArgs.Cost.RemoveAt(0);

                castArgs.Cost.AddRange(addSlots);
            }
        }
    }
}