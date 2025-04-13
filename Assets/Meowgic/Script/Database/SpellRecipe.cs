using System.Collections.Generic;
using UnityEngine;

namespace Meowgic
{
    [CreateAssetMenu(menuName = "Meowgic/SpellRecipe")]
    public class SpellRecipe : DatabaseItemSO
    {
        [SerializeField] private Spell result;
        [SerializeField] private DatabaseItemSO[] ingredients;

        public override string DisplayName => string.IsNullOrEmpty(displayName) ? result.DisplayName : displayName;
        public override Sprite Icon => icon ? icon : result.Icon;
        public Spell Result => result;
        public IReadOnlyList<DatabaseItemSO> Ingredients => ingredients;
    }
}