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
        [SerializeField, Prefab] private int speed;
        [SerializeField, Prefab] private GameObject prefab;
        [SerializeField, Prefab] private CatalystBase[] cost;
        [SerializeReference, TypeInstance] private Effect[] effects;

        public override Sprite Icon => icon ? icon : element.Icon;
        public Element Element => element;
        public int Speed => speed;
        public GameObject Prefab => prefab;
        public CatalystBase[] Cost => cost;
        public IReadOnlyList<Effect> Effects => effects;
    }
}