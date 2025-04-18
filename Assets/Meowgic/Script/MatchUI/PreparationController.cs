using Drafts.DataView;
using UnityEngine;

namespace Meowgic.Match.UI
{
    public class PreparationController : MonoBehaviour
    {
        [SerializeField] private CollectionView preparation;

        private void Start() => DeactivateAll();

        public void DeactivateAll()
        {
            foreach (var v in preparation.GetViews<SpellPreparationController>())
                v.ActivateAvailable(0);
        }
    }
}