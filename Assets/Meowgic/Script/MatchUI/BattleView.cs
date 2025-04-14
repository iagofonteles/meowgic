using Drafts.DataView;
using UnityEngine;

namespace Meowgic.Match.UI
{
    public class BattleView : DataView<Battle>
    {
        [SerializeField] private SideView playerSide;
        [SerializeField] private SideView enemySide;
        [SerializeField] private CollectionView preparation;

        protected override void Subscribe()
        {
            playerSide.TrySetData(Data.PlayerSide);
            enemySide.TrySetData(Data.EnemySide);
            preparation.TrySetData(Data.Preparation);
        }

        protected override void Unsubscribe()
        {
            playerSide.TrySetData(null);
            enemySide.TrySetData(null);
            preparation.TrySetData(null);
        }
    }
}