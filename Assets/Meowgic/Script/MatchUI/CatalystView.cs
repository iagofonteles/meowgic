using Drafts.DataView;
using Meowgic.UI;
using UnityEngine;

namespace Meowgic.Match.UI
{
    public class CatalystView : DataView<Catalyst>
    {
        [SerializeField] private CatalystBaseView baseView;

        protected override void Subscribe()
        {
            baseView.TrySetData(Data.Base);
        }

        protected override void Unsubscribe()
        {
            baseView.TrySetData(null);
        }
    }
}