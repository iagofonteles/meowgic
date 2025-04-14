using Drafts.DataView;
using Meowgic.UI;
using UnityEngine;

namespace Meowgic.Match.UI
{
    public class EffectView : DataView<Effect>
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