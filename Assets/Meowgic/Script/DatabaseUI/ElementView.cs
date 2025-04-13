using Drafts.DataView;
using UnityEngine;
using UnityEngine.UI;

namespace Meowgic.UI
{
    public class ElementView : DataView<Element>
    {
        [SerializeField] private DatabaseItemSOView baseView;
        [SerializeField] private Image color;

        protected override void Subscribe()
        {
            baseView.TrySetData(Data);
            color.TrySetColor(Data.Color);
        }

        protected override void Unsubscribe()
        {
            baseView.TrySetData(null);
            color.TrySetColor(Color.clear);
        }
    }
}