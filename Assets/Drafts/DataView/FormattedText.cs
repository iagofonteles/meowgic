using TMPro;
using UnityEngine;

namespace Drafts.DataView
{
    public class FormattedText : DataView<string>
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private string format = "{0}";
        private string _defaultText;

        private void Awake() => _defaultText = text.text;
        private void Set(object value) => text.text = string.Format(format, value);

        public void SetFormat(string f)
        {
            format = f;
            Set(Data);
        }

        public override void SetData(object data)
        {
            if (data is not string) data = data.ToString();
            base.SetData(data);
        }

        public void SeValue(object value) => Set(value ?? _defaultText);
        public void SeValue(string value) => Set(value ?? _defaultText);
        public void SeValue(int value) => Set(value);
        public void SeValue(float value) => Set(value);

        protected override void Subscribe() => Set(Data);
        protected override void Unsubscribe() => Set(_defaultText);
    }
}