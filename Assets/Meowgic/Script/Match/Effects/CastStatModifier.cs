using System;
using Drafts;
using UnityEngine;

namespace Meowgic.Match.EffectScripts
{
    [Serializable]
    public class CastStatModifier : EffectScript
    {
        [SerializeReference, TypeInstance] private CastArgsStat stat;
        [SerializeField] private float flatBonus;
        [SerializeField] private float multiplier = 1;
        [SerializeField] private AffectedCasts affectedCasts;

        public override void OnTurnBegin(int castIndex, TurnArgs turnArgs)
        {
            foreach (var castArgs in affectedCasts.Resolve(castIndex, turnArgs))
            {
                var value = (stat.GetStat(castArgs) + flatBonus) * multiplier;
                stat.SetStat(castArgs, Mathf.RoundToInt(value));
            }
        }
    }
}