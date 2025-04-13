#if USE_NETCODE_GAMEOBJECT
using Unity.Netcode;
using UnityEngine;

namespace BlueGravity.DataView
{
    public class DataViewNetworkData : NetworkBehaviour
    {
        [SerializeField] DataView view;
        [SerializeField] Object data;
        private object Data => data is DataView v ? v.GetData() : data;
        public override void OnNetworkSpawn() => view.SetData(Data);
    }
}
#endif