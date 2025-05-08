using System;
using System.Collections.Generic;
using UnityEngine;

namespace Meowgic.Match
{
    [Serializable]
    public class SpellCastArgs
    {
        [SerializeField] private Actor caster;
        [SerializeField] private List<CatalystBase> cost = new();
        
        public Actor Caster => caster;
        public Actor target;
        public Spell spell;
        public List<CatalystBase> Cost => cost;
        public List<Catalyst> catalysts = new();

        public int speed;
        public int damage;
        public int heal;
        public int shield;
        private bool area;
        public bool cancelled;

        public Action<SpellCastArgs> PreProcess;
        public Action<SpellCastArgs> PostProcess;
        public Action OnModified;

        public SpellCastArgs(Actor caster) => this.caster = caster;

        public void ResetValues()
        {
            Cost.Clear();
            
            if (spell)
            {
                speed = spell.Speed;
                damage = spell.Damage;
                heal = spell.Heal;
                shield = spell.Shield;
                area = spell.Area;
                Cost.AddRange(spell.Cost);
            }
            else
            {
                speed = 0;
                damage = 0;
                heal = 0;
                shield = 0;
                area = false;
            }

            cancelled = false;
            PreProcess = null;
            PostProcess = null;
        }
    }
}