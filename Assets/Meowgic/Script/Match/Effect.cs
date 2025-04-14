using System;

namespace Meowgic.Match
{
    [Serializable]
    public abstract class Effect
    {
        /// <summary>Invoked after all preparations and before any cast.</summary>
        public virtual void OnTurnBegin(int castIndex, TurnArgs turnArgs) { }

        /// <summary>Invoked after all OnTurnBegin.</summary>
        public virtual void OnTurnExecute(int castIndex, TurnArgs turnArgs) { }

        /// <summary>Invoked after all OnTurnExecute.</summary>
        public virtual void OnTurnEnd(int castIndex, TurnArgs turnArgs) { }
    }
}