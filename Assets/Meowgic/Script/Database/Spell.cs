using System.Collections.Generic;
using Drafts;
using Meowgic.Match;
using UnityEngine;

namespace Meowgic
{
    [CreateAssetMenu(menuName = "Meowgic/Spell")]
    public class Spell : DatabaseItemSO
    {
        [SerializeField] private Element element;
        [SerializeField] private int speed = 3;
        [SerializeField, Prefab] private Transform prefab;
        [SerializeField] private CatalystBase[] cost;
        [SerializeField] private TypeInstances<EffectScript> effects;

        public override Sprite Icon => icon ? icon : element.Icon;
        public Element Element => element;
        public int Speed => speed;
        public Transform Prefab => prefab;
        public CatalystBase[] Cost => cost;
        public IReadOnlyList<EffectScript> Effects => effects;
    }
}