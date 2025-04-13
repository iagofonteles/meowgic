using Meowgic.Match;
using UnityEngine;

namespace Meowgic
{
    [CreateAssetMenu(menuName = "Meowgic/Enemy")]
    public class Enemy : DatabaseItemSO, IActor
    {
        [SerializeField] private int health;

        public int Health => health;
        int IActor.MaxHealth => health;
    }
}