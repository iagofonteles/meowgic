using UnityEngine;

namespace Meowgic
{
    [CreateAssetMenu(menuName = "Meowgic/Spell")]
    public class Spell : DatabaseItemSO
    {
        [SerializeField] private Element element;

        public override Sprite Icon => icon ? icon : element.Icon;
        public Element Element => element;
    }
}