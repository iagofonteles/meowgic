using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Meowgic.Match.EffectScripts
{
    [Serializable]
    public class AffectedCasts
    {
        [SerializeField] private bool targetEnemy;
        [SerializeField] private int castOffset;
        [SerializeField, Min(0)] private int affectedCasts = 1;

        public IEnumerable<SpellCastArgs> Resolve(int castIndex, TurnArgs turnArgs)
        {
            var list = targetEnemy ? turnArgs.EnemyCasts : turnArgs.AllyCasts;
            return list.Skip(castIndex + castOffset).Take(affectedCasts);
        }
    }
}