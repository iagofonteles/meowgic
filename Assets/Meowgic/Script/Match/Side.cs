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
        [SerializeField] private ObservableList<SpellPreparation> preparation = new();
        [SerializeField] private CardPool<Catalyst> pool;
        [SerializeField] List<SpellCastArgs> casts = new();

        public Battle Battle { get; }
        public Side OtherSide => this == Battle.PlayerSide ? Battle.EnemySide : Battle.PlayerSide;
        public IReadOnlyList<Actor> Actors => actors;
        public CardPool<Catalyst> Pool => pool;
        public IObservableList<SpellPreparation> Preparation => preparation;
        public IReadOnlyList<SpellCastArgs> Casts => casts;
        public TurnArgs TurnArgs { get; private set; }

        public Side(Battle battle, IEnumerable<IActor> actors, IEnumerable<Catalyst> inventory)
        {
            Battle = battle;
            this.actors = actors.Select(a => new Actor(this, a)).ToList();
            pool = new(inventory);
            TurnArgs = new(this);
        }

        public void ResetPreparations()
        {
            preparation.Clear();
            preparation.AddRange(actors.SelectMany(GetPreps));
            casts.Clear();
            casts.AddRange(preparation.Select(p => p.CastArgs));
        }

        private IEnumerable<SpellPreparation> GetPreps(Actor a) =>
            Enumerable.Range(0, a.CastAmount.Value).Select(_ => new SpellPreparation(a));
    }
}