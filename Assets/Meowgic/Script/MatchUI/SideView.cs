using Drafts.DataView;
using UnityEngine;

namespace Meowgic.Match.UI
{
    public class SideView : DataView<Side>
    {
        [SerializeField] private CollectionView actors;
        [SerializeField] private CatalystPoolView pool;
        [SerializeField] private CollectionView casts;

        protected override void Subscribe()
        {
            actors.TrySetData(Data.Actors);
            pool.TrySetData(Data.Pool);
            casts.TrySetData(Data.Casts);
        }

        protected override void Unsubscribe()
        {
            actors.TrySetData(null);
            pool.TrySetData(null);
            casts.TrySetData(null);
        }
    }
}