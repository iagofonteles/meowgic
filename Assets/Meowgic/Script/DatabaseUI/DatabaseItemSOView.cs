using Drafts.DataView;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Meowgic.UI
{
    public class DatabaseItemSOView : DataView<DatabaseItemSO>
    {
        [SerializeField] private TMP_Text displayName;
        [SerializeField] private TMP_Text description;
        [SerializeField] private Image icon;

        private string _defaultName;
        private string _defaultDesc;

        private void Awake()
        {
            if (displayName) _defaultName = displayName.text;
            if (description) _defaultDesc = description.text;
        }

        protected override void Subscribe()
        {
            displayName.TrySetText(Data.DisplayName);
            description.TrySetText(Data.Description);
            icon.TrySetSprite(Data.Icon);
        }

        protected override void Unsubscribe()
        {
            displayName.TrySetText(_defaultName);
            description.TrySetText(_defaultDesc);
            icon.TrySetSprite(null);
        }
    }
}