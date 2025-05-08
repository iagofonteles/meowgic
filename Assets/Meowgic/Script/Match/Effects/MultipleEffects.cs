using System;
using Drafts;
using UnityEngine;

namespace Meowgic.Match.EffectScripts
{
    [Serializable]
    public class MultipleEffects : EffectScript
    {
        [SerializeField] private TypeInstances<EffectScript> effects;

        public override void Setup(int castIndex, TurnArgs turnArgs)
        {
            foreach (var effect in effects) effect.Setup(castIndex, turnArgs);
        }

        public override void OnTurnExecute(int castIndex, TurnArgs turnArgs)
        {
            foreach (var effect in effects) effect.OnTurnExecute(castIndex, turnArgs);
        }

        public override void OnTurnEnd(int castIndex, TurnArgs turnArgs)
        {
            foreach (var effect in effects) effect.OnTurnEnd(castIndex, turnArgs);
        }
    }
}