using Drafts.DataView;
using Meowgic.UI;
using UnityEngine;

namespace Meowgic.Match.UI
{
    public class EffectView : DataView<EffectScript>
    {
        [SerializeField]
        
        protected override void Subscribe()
        {
            throw new System.NotImplementedException();
        }
        protected override void Unsubscribe()
        {
            throw new System.NotImplementedException();
        }
    }
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