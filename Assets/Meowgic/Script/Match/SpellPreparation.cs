using System;
using System.Collections.Generic;
using System.Linq;

namespace Meowgic.Match
{
    /// <summary>
    /// Player combination of Spell + Catalysts used
    /// </summary>
    [Serializable]
    public class SpellPreparation
    {
        public IReadOnlyList<SpellPreparation> PreparationList { get; }
        [Obsolete] public int Index { get; }
        public Actor caster;
        public Spell spell;
        public Catalyst[] catalysts = new Catalyst[2];

        public SpellPreparation(int index, List<SpellPreparation> preparationList)
        {
            Index = index;
            PreparationList = preparationList;
        }
    }

    public static class SpellPreparationExtensions
    {
        public static void SetCastAmount(this List<SpellPreparation> preparations, int amount)
        {
            preparations.Clear();
            for (var i = 0; i < amount; i++)
            {
                var cast = new SpellPreparation(preparations.Count, preparations);
                preparations.Add(cast);
            }
        }

        public static SpellCastArgs[] GetCasts(this IEnumerable<SpellPreparation> preparations, Actor target)
            => preparations.Select(p => new SpellCastArgs
                {
                    caster = p.caster,
                    target = target,
                    spell = p.spell,
                    speed = p.spell.Speed,
                    catalysts = p.catalysts.Where(c => c is not null).ToList(),
                }
            ).ToArray();
    }
}