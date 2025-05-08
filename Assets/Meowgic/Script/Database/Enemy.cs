using System.Collections.Generic;
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

        private readonly EnemyAi _ai = new();

        public int Health => health;
        public int MaxHealth => health;
        public int CastAmount => castAmount;
        public IEnumerable<Spell> Spells => spells;

        Spell IActor.Ai(Actor actor) => _ai.GetSpell(actor);
    }
}