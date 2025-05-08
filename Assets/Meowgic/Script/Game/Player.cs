using System;
using System.Collections.Generic;
using Meowgic.Match;
using UnityEngine;

namespace Meowgic
{
    [Serializable]
    public class Player : IActor
    {
        [SerializeField] private string displayName;
        [SerializeField] private int health;
        [SerializeField] private List<Catalyst> inventory = new();

        public string DisplayName => displayName;
        public int Health => health;
        public List<Catalyst> Inventory => inventory;
        public int MaxHealth => 100;
        public int CastAmount => 3;
        public IEnumerable<Spell> Spells => Game.Database.GetAll<Spell>();

        public Spell Ai(Actor actor) => throw new InvalidOperationException();
    }
}