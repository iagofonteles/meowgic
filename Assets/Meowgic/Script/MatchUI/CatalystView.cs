using Drafts.DataView;
using Meowgic.UI;
using UnityEngine;

namespace Meowgic.Match.UI
{
    public class CatalystView : DataView<Catalyst>
    {
        [SerializeField] private CatalystBaseView baseView;
        [SerializeField] private CollectionView effects;

        protected override void Subscribe()
        {
            baseView.TrySetData(Data.Base);
            effects.TrySetData(Data.Effects);
        }

        protected override void Unsubscribe()
        {
            baseView.TrySetData(null);
        }
    }
}