using System;
using Drafts;
using UnityEngine;

namespace Meowgic.Match
{
    public interface IActor
    {
        public string DisplayName { get; }
        public int Health { get; }
        public int MaxHealth { get; }
    }

    /// <summary>Player cat and enemies</summary>
    [Serializable]
    public class Actor
    {
        [SerializeField] private string displayName;
        [SerializeField] private Observable<int> maxHealth;
        [SerializeField] private Observable<int> health;

        public Side Side { get; }
        public Battle Battle => Side.Battle;
        public Side OtherSide => Side.OtherSide;
        public string DisplayName => displayName;
        public Observable<int> MaxHealth => maxHealth;
        public Observable<int> Health => health;

        public Actor(Side side, IActor actor)
        {
            Side = side;
            displayName = actor.DisplayName;
            maxHealth = new(actor.MaxHealth);
            health = new(actor.Health);
        }
    }
}