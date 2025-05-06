using Meowgic.Match;
using UnityEngine;

namespace Meowgic
{
    [CreateAssetMenu(menuName = "Meowgic/Catalyst")]
    public class CatalystBase : DatabaseItemSO
    {
        [SerializeField] private Element element;
        [SerializeField] private int grade = 1;

        public override Sprite Icon => icon ? icon : element.Icon;
        public Element Element => element;
        public int Grade => grade;

        public bool Compatible(Catalyst other)
        {
            if (!other?.Base) return false;
            return other.Base.grade >= grade && element.Compatible(other.Base.element);
        }
    }
}