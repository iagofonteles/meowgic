using System;
using UnityEngine;

namespace Meowgic.Match
{
    public enum EffectType
    {
        Neutral,
        Buff,
        Debuff,
    }

    [Serializable]
    public abstract class EffectScript
    {
        [SerializeField] private EffectType type;
        public EffectType Type => type;

        /// <summary>Invoked after all preparations and before any cast.</summary>
        public virtual void Setup(int castIndex, TurnArgs turnArgs) { }

        /// <summary>Invoked after all OnTurnBegin.</summary>
        public virtual void OnTurnExecute(int castIndex, TurnArgs turnArgs) { }

        /// <summary>Invoked after all OnTurnExecute.</summary>
        public virtual void OnTurnEnd(int castIndex, TurnArgs turnArgs) { }
    }
}