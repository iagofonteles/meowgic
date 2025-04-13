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
        int IActor.MaxHealth => 100;
    }
}