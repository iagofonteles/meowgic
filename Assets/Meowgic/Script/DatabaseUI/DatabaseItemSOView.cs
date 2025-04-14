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

        public Image Icon => icon;

        private void Awake()
        {
            if (displayName) _defaultName = displayName.text;
            if (description) _defaultDesc = description.text;
            icon.TrySetColor(new(1, 1, 1, .5f));
        }

        protected override void Subscribe()
        {
            displayName.TrySetText(Data.DisplayName);
            description.TrySetText(Data.Description);
            icon.TryOverrideSprite(Data.Icon);
            icon.TrySetColor(Color.white);
        }

        protected override void Unsubscribe()
        {
            displayName.TrySetText(_defaultName);
            description.TrySetText(_defaultDesc);
            icon.TryOverrideSprite(null);
            icon.TrySetColor(new(1, 1, 1, .5f));
        }
    }
}