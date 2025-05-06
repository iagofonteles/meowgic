using System;
using UnityEngine;

namespace Meowgic.Match.EffectScripts
{
    [Serializable]
    public class CancelCast : EffectScript
    {
        [SerializeField] private AffectedCasts affectedCasts;

        public override void OnTurnBegin(int castIndex, TurnArgs turnArgs)
        {
            foreach (var castArgs in affectedCasts.Resolve(castIndex, turnArgs))
                castArgs.cancelled = true;
        }
    }
}