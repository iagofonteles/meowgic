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

        private EnemyAi _ai;

        public int Health => health;
        public int MaxHealth => health;
        public int CastAmount => castAmount;
        public Spell[] Spells => spells;

        IEnumerable<SpellPreparation> IActor.Ai(Actor actor) => _ai.GetPreparation(actor);

        private void OnEnable() => _ai ??= new EnemyAi(this);
    }
}