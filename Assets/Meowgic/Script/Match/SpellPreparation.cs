using System;
using System.Collections.Generic;
using System.Linq;
using Drafts;
using UnityEngine;

namespace Meowgic.Match
{
    /// <summary>
    /// Player combination of Spell + Catalysts used
    /// </summary>
    [Serializable]
    public class SpellPreparation
    {
        [SerializeField] private Actor caster;
        [SerializeField] private Observable<Spell> spell = new();
        [SerializeField] private ObservableList<Catalyst> catalysts = new();

        public ObservableList<SpellPreparation> Preparation { get; }
        public Actor Caster => caster;
        public Drafts.IObservable<Spell> Spell => spell;
        public IObservableList<Catalyst> Catalysts => catalysts;

        private ObservableList<Catalyst> Hand => caster.Side.Pool.Hand;

        public int GetIndex() => Preparation.IndexOf(this);

        public SpellPreparation(Actor actor, ObservableList<SpellPreparation> preparation)
        {
            caster = actor;
            Preparation = preparation;
        }

        public void SetSpell(Spell value)
        {
            if (spell.Value == value) return;

            // refund catalysts
            foreach (var c in catalysts)
                if (c != null)
                    Hand.Add(c);

            catalysts.Clear();
            if (value)
                foreach (var _ in value.Cost)
                    catalysts.Add(null);
            
            spell.Value = value;
        }

        public void SetCatalyst(int index, Catalyst value)
        {
            if (catalysts[index] != null)
                Hand.Add(catalysts[index]);

            Hand.Remove(value);
            catalysts[index] = value;
        }
    }

    public static class SpellPreparationExtensions
    {
        public static SpellCastArgs[] GetCasts(this IEnumerable<SpellPreparation> preparations
            , Actor target, bool validOnly)
        {
            if (validOnly) preparations = preparations.Where(IsValidCast);
            return preparations.Select(p => new SpellCastArgs
                {
                    caster = p.Caster,
                    target = target,
                    spell = p.Spell.Value,
                    speed = p.Spell.Value.Speed,
                    catalysts = p.Catalysts.Where(c => c is not null).ToList(),
                }
            ).ToArray();
        }

        public static bool IsValidCast(this SpellPreparation prep)
        {
            if (!prep.Spell.Value) return false;
            for (var i = 0; i < prep.Spell.Value.Cost.Length; i++)
            {
                var used = prep.Catalysts[i];
                var cost = prep.Spell.Value.Cost[i];
                if (!cost.Compatible(used)) return false;
            }

            return true;
        }
    }
}