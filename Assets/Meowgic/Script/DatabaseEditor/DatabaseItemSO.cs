using UnityEngine;

namespace Meowgic
{
    public abstract class DatabaseItemSO : ScriptableObject, IDatabaseItem
    {
        [SerializeField] protected string displayName;
        [SerializeField] protected string description;
        [SerializeField] protected Sprite icon;

        public virtual string DisplayName
            => string.IsNullOrEmpty(displayName) ? name : displayName;

        public virtual string Description
            => string.IsNullOrEmpty(displayName) ? $"{name}: {GetType().Name}" : description;

        public virtual Sprite Icon => icon;
    }
}