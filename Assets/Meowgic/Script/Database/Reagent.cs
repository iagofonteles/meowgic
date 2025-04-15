using UnityEngine;

namespace Meowgic
{
    [CreateAssetMenu(menuName = "Meowgic/Reagent")]
    public class Reagent : DatabaseItemSO
    {
        [SerializeField] private Effect effect;

        public Effect Effect => effect;
    }
}