using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace Drafts.DataView
{
    public class DataView : MonoBehaviour
    {
        public UnityEvent<object> onDataChanged;
        private object _data;
           
        public virtual object GetData() => _data;

        public virtual void SetData(object data)
        {
            _data = data;
            onDataChanged?.Invoke(data);
        }

        [ContextMenu("Reset Data")]
        public void ResetData() => SetData(null);

        [ContextMenu("Log Data")]
        public void LogData()
        {
            var data = GetData();
            Debug.Log(data, data as Object);
        }
    }
}