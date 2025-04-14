using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Meowgic.Match
{
    /// <summary>Player cat and enemies</summary>
    [Serializable]
    public class Side
    {
        [SerializeField] private List<Actor> actors;
        [SerializeField] private CardPool<Catalyst> pool;
        [SerializeField] private SpellCastArgs[] casts;

        public Battle Battle { get; }
        public Side OtherSide => this == Battle.PlayerSide ? Battle.EnemySide : Battle.PlayerSide;
        public IReadOnlyList<Actor> Actors => actors;
        public CardPool<Catalyst> Pool => pool;

        public SpellCastArgs[] Casts
        {
            get => casts;
            set => casts = value;
        }

        public Side(Battle battle, IEnumerable<IActor> actors, IEnumerable<Catalyst> inventory)
        {
            Battle = battle;
            this.actors = actors.Select(a => new Actor(this, a)).ToList();
            pool = new(inventory);
        }
    }
}