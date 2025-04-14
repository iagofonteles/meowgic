using System.Collections.Generic;
using System.Linq;
using Meowgic.Match;
using UnityEngine;

namespace Meowgic
{
    [CreateAssetMenu(menuName = "Meowgic/Enemy")]
    public class Enemy : DatabaseItemSO, IActor
    {
        [SerializeField] private int health;
        [SerializeField] private int castAmount = 1;
        [SerializeField] private Spell[] spells;

        public int Health => health;
        public int MaxHealth => health;
        public int CastAmount => castAmount;
        public Spell[] Spells => spells;

        IEnumerable<SpellPreparation> IActor.Ai(Actor actor)
        {
            var list = new List<SpellPreparation>();
            list.Add(new(0, list)
            {
                caster = actor,
                spell = spells.Random(1).First(),
            });
            return list;
        }
    }
}