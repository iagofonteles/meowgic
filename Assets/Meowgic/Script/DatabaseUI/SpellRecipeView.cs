using Drafts.DataView;
using UnityEngine;

namespace Meowgic.UI
{
    public class SpellRecipeView : DataView<SpellRecipe>
    {
        [SerializeField] private DatabaseItemSOView baseView;

        protected override void Subscribe()
        {
            baseView.TrySetData(Data);
        }

        protected override void Unsubscribe()
        {
            baseView.TrySetData(null);
        }
    }
}