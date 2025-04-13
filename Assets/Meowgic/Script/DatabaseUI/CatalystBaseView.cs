using Drafts.DataView;
using TMPro;
using UnityEngine;

namespace Meowgic.UI
{
    public class CatalystBaseView : DataView<CatalystBase>
    {
        [SerializeField] private DatabaseItemSOView baseView;
        [SerializeField] private ElementView element;
        [SerializeField] private TMP_Text grade;

        protected override void Subscribe()
        {
            baseView.TrySetData(Data);
            element.TrySetData(Data.Element);
            grade.TrySetText(Data.Grade);
        }

        protected override void Unsubscribe()
        {
            baseView.TrySetData(null);
            element.TrySetData(null);
            grade.TrySetText(0);
        }
    }
}