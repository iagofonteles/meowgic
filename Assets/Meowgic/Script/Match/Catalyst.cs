using System;
using Drafts;
using UnityEngine;

namespace Meowgic.Match
{
    /// <summary>
    /// The instance of a CatalystBase that player can be modified
    /// </summary>
    [Serializable]
    public class Catalyst
    {
        [SerializeField] private CatalystBase catalystBase;
        [SerializeField] private ObservableArray<Effect> effects = new(2);

        public CatalystBase Base => catalystBase;
        public ObservableArray<Effect> Effects => effects;

        public Catalyst(CatalystBase catalystBase)
        {
            this.catalystBase = catalystBase;
        }

        public static implicit operator bool(Catalyst c) => c != null && c.catalystBase;
    }
}