using System;
using UnityEngine;

namespace Meowgic.Match.EffectScripts
{
    [Serializable]
    public class ChangeTarget : EffectScript
    {
        public enum Mode
        {
            TargetCaster,
        }

        [SerializeField] private AffectedCasts affectedCasts;
        [SerializeField] private Mode changeMode;

        public override void Setup(int castIndex, TurnArgs turnArgs)
        {
            foreach (var castArgs in affectedCasts.Resolve(castIndex, turnArgs))
                castArgs.target = changeMode switch
                {
                    Mode.TargetCaster => castArgs.Caster,
                    _ => throw new ArgumentOutOfRangeException()
                };
        }
    }
}