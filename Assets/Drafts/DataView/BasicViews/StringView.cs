using TMPro;
using UnityEngine;

namespace Drafts.DataView
{
    public class StringView : DataView<string>
    {
        [SerializeField] private TMP_Text value;
        private string _default;

        private void Awake() => _default = value.text;

        public override void SetData(object data)
        {
            if (data is not string) data = data.ToString();
            base.SetData(data);
        }
        
        protected override void Subscribe() => value.text = Data;
        protected override void Unsubscribe() => value.text = _default;
    }
}