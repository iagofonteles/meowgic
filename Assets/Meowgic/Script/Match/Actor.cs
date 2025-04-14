using System;
using System.Collections.Generic;
using Drafts;
using UnityEngine;

namespace Meowgic.Match
{
    public interface IActor
    {
        public string DisplayName { get; }
        public int Health { get; }
        public int MaxHealth { get; }
        public int CastAmount { get; }
        IEnumerable<SpellPreparation> Ai(Actor actor);
    }

    /// <summary>Player cat and enemies</summary>
    [Serializable]
    public class Actor
    {
        [SerializeField] private string displayName;
        [SerializeField] private Observable<int> maxHealth;
        [SerializeField] private Observable<int> health;
        [SerializeField] private Observable<int> shield = new(0);
        [SerializeField] private Observable<int> castAmount;

        public Func<Actor, IEnumerable<SpellPreparation>> Ai { get; }
        public Side Side { get; }
        public Battle Battle => Side.Battle;
        public Side OtherSide => Side.OtherSide;
        public string DisplayName => displayName;
        public Observable<int> MaxHealth => maxHealth;
        public Observable<int> Health => health;
        public Observable<int> Shield => shield;
        public Observable<int> CastAmount => castAmount; // amount of casts per turn
        public bool IsDead => health.Value <= 0;

        public Actor(Side side, IActor actor)
        {
            Side = side;
            Ai = actor.Ai;
            displayName = actor.DisplayName;
            maxHealth = new(actor.MaxHealth);
            health = new(actor.Health);
            castAmount = new(actor.CastAmount);
        }
    }
}