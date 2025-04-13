using System.Collections.Generic;
using UnityEngine;

namespace Meowgic
{
    [CreateAssetMenu(menuName = "Meowgic/CatalystRecipe")]
    public class CatalystRecipe : DatabaseItemSO
    {
        [SerializeField] private CatalystBase result;
        [SerializeField] private DatabaseItemSO[] ingredients;

        public override string DisplayName => string.IsNullOrEmpty(displayName) ? result.DisplayName : displayName;
        public override Sprite Icon => icon ? icon : result.Icon;
        public CatalystBase Result => result;
        public IReadOnlyList<DatabaseItemSO> Ingredients => ingredients;
    }
}