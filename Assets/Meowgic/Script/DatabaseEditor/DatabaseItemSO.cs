using UnityEngine;

namespace Meowgic
{
    public abstract class DatabaseItemSO : ScriptableObject, IDatabaseItem
    {
        [SerializeField] protected string displayName;
        [SerializeField] protected Sprite icon;

        public virtual string DisplayName => string.IsNullOrEmpty(displayName) ? name : displayName;
        public virtual string Description => $"{name}: {GetType().Name}";
        public virtual Sprite Icon => icon;
    }
}