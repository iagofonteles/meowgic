using System;
using Drafts;
using UnityEngine;

namespace Meowgic.Match.EffectScripts
{
    public interface ICastArgsProcessor
    {
        void Process(SpellCastArgs castIndex);
    }

    [Serializable]
    public class CastArgsProcessor : EffectScript
    {
        [SerializeReference, TypeInstance] private ICastArgsProcessor processor;
        [SerializeField] private string method;
        [SerializeField] private AffectedCasts affectedCasts;

        public override void OnTurnExecute(int castIndex, TurnArgs turnArgs)
        {
            foreach (var castArgs in affectedCasts.Resolve(castIndex, turnArgs))
                processor.Process(castArgs);
        }
    }
}