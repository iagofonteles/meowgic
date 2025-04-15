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
        public Observable<Spell> Spell => spell;
        public ObservableList<Catalyst> Catalysts => catalysts;

        public int GetIndex() => Preparation.IndexOf(this);

        public SpellPreparation(Actor actor, ObservableList<SpellPreparation> preparation)
        {
            caster = actor;
            Preparation = preparation;
            spell.OnChanged += UpdateCatalysts;
        }

        private void UpdateCatalysts(object value)
        {
            catalysts.Clear();
            if (value is not Spell s) return;
            foreach (var _ in s.Cost) catalysts.Add(null);
        }
    }

    public static class SpellPreparationExtensions
    {
        public static SpellCastArgs[] GetCasts(this IEnumerable<SpellPreparation> preparations, Actor target)
            => preparations.Where(IsValidCast).Select(p => new SpellCastArgs
                {
                    caster = p.Caster,
                    target = target,
                    spell = p.Spell.Value,
                    speed = p.Spell.Value.Speed,
                    catalysts = p.Catalysts.Where(c => c is not null).ToList(),
                }
            ).ToArray();

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