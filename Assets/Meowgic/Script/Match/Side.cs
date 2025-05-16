using System;
using System.Collections.Generic;
using System.Linq;
using Drafts;
using UnityEngine;

namespace Meowgic.Match
{
    /// <summary>Player cat and enemies</summary>
    [Serializable]
    public class Side
    {
        [SerializeField] private List<Actor> actors;
        [SerializeField] private CardPool<Catalyst> pool;
        [SerializeField] private ObservableList<SpellCastArgs> casts = new();

        public Battle Battle { get; }
        public Side OtherSide => this == Battle.PlayerSide ? Battle.EnemySide : Battle.PlayerSide;
        public IReadOnlyList<Actor> Actors => actors;
        public CardPool<Catalyst> Pool => pool;
        public IReadOnlyList<SpellCastArgs> Casts => casts;
        public TurnArgs TurnArgs { get; private set; }

        public Side(Battle battle, IEnumerable<IActor> actors, IEnumerable<Catalyst> inventory)
        {
            Battle = battle;
            this.actors = actors.Select(a => new Actor(this, a)).ToList();
            pool = new(inventory);
            TurnArgs = new(this);
        }

        public void ResetCasts()
        {
            casts.Clear();
            casts.AddRange(actors.SelectMany(GetPreps));
        }

        private IEnumerable<SpellCastArgs> GetPreps(Actor a) =>
            Enumerable.Range(0, a.CastAmount.Value).Select(_ => new SpellCastArgs(a));
    }
}