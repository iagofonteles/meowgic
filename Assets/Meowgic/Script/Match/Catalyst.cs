using System;
using System.Collections.Generic;
using Drafts;
using UnityEngine;

namespace Meowgic.Match
{
    /// <summary>
    /// The instance of a CatalystBase that player can be modify
    /// </summary>
    [Serializable]
    public class Catalyst
    {
        [SerializeField] private CatalystBase catalystBase;
        [SerializeField] private TypeInstances<EffectScript> effects = new();

        public CatalystBase Base => catalystBase;
        public IList<EffectScript> Effects => effects;

        public Catalyst(CatalystBase catalystBase)
        {
            this.catalystBase = catalystBase;
        }
    }
}