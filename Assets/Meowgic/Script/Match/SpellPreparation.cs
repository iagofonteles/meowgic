using System;
using System.Linq;
using Drafts;
using UnityEngine;

namespace Meowgic.Match
{
    /// <summary>
    /// Player combination of Spell + Catalysts used
    /// </summary>
    [Serializable]
    [Obsolete]
    public class SpellPreparation
    {
        [SerializeField] private SpellCastArgs args;
        public SpellCastArgs CastArgs => args;
        public ObservableList<Catalyst> Hand => args.Caster.Side.Pool.Hand;
        public event Action OnChanged;

        public SpellPreparation(Actor actor) => args = new(actor);

        public void SetSpell(Spell value)
        {
            if (args.spell == value) return;

            // refund catalysts
            foreach (var c in args.catalysts.Where(c => c))
                Hand.Add(c);

            args.catalysts.Clear();
            if (value) args.catalysts.AddRange(Enumerable.Repeat<Catalyst>(null, value.Cost.Length));

            args.spell = value;
            OnChanged?.Invoke();
        }

        public void SetCatalyst(int index, Catalyst value)
        {
            if (args.catalysts[index])
                Hand.Add(args.catalysts[index]);

            Hand.Remove(value);
            args.catalysts[index] = value;
            OnChanged?.Invoke();
        }
    }
}