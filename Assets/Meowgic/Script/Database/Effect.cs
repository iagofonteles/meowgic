using Drafts;
using Meowgic.Match;
using UnityEngine;

namespace Meowgic
{
    [CreateAssetMenu(menuName = "Meowgic/Effect")]
    public class Effect : DatabaseItemSO
    {
        [SerializeReference, TypeInstance] private EffectScript script;

        public EffectScript Script => script;
    }
}