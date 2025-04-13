using System;
using System.Collections.Generic;
using UnityEngine;

namespace Meowgic.Match
{
    /// <summary>
    /// Player combination of Spell + Catalysts used
    /// </summary>
    [Serializable]
    public class SpellCast
    {
        [SerializeField] private Spell spell;
        [SerializeField] private List<Catalyst> catalysts = new();

        public Spell Spell => spell;
        public List<Catalyst> Catalysts => catalysts;
    }
}