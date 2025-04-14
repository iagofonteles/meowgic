using System;
using System.Collections.Generic;

namespace Meowgic.Match
{
    /// <summary>
    /// Player combination of Spell + Catalysts used
    /// </summary>
    [Serializable]
    public class SpellCastArgs
    {
        public Actor caster;
        public Actor target;
        public Spell spell;
        public List<Catalyst> catalysts;
        public int damage;
    }
}