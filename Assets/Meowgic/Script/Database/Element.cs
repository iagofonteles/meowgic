using UnityEngine;

namespace Meowgic
{
    [CreateAssetMenu(menuName = "Meowgic/Element")]
    public class Element : DatabaseItemSO
    {
        [SerializeField] private Color color;

        public Color Color => color;

        public bool Compatible(Element other)
        {
            if (name == "Any") return other;
            return this == other;
        }
    }
}