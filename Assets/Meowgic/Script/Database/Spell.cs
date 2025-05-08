using System.Collections.Generic;
using Drafts;
using Meowgic.Match;
using UnityEngine;

namespace Meowgic
{
    [CreateAssetMenu(menuName = "Meowgic/Spell")]
    public class Spell : DatabaseItemSO
    {
        [SerializeField, Prefab] private Transform prefab;
        [SerializeField] private Element element;
        [SerializeField] private int speed = 3;
        [SerializeField] private int damage;
        [SerializeField] private int heal;
        [SerializeField] private int shield;
        [SerializeField] private bool area;
        [SerializeField] private CatalystBase[] cost;
        [SerializeField] private TypeInstances<EffectScript> effects;

        public override Sprite Icon => icon ? icon : element.Icon;
        public Transform Prefab => prefab;
        public Element Element => element;
        public int Speed => speed;
        public int Damage => damage;
        public int Heal => heal;
        public int Shield => shield;
        public bool Area => area;
        public CatalystBase[] Cost => cost;
        public IReadOnlyList<EffectScript> Effects => effects;
    }
}