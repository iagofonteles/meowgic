using System;
using System.Collections.Generic;

namespace Meowgic.Match
{
    [Serializable]
    public class SpellCastArgs
    {
        public Actor caster;
        public Actor target;
        public Spell spell;
        public List<Catalyst> catalysts;
        public int speed;
        public int damage;
        public int heal;
        public int shield;
        public Action<SpellCastArgs> Effect;
        public bool cancelled;
    }
}