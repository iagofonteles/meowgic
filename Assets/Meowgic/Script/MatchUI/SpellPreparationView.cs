using Drafts.DataView;
using UnityEngine;

namespace Meowgic.Match.UI
{
    public class SpellPreparationView : DataView<SpellPreparation>
    {
        [SerializeField] private SpellCastArgsView args;
        [SerializeField] private CollectionView availableSpells;
        [SerializeField] private CollectionView availableCatalysts;

        public SpellCastArgsView Args => args;
        public CollectionView AvailableSpells => availableSpells;
        public CollectionView AvailableCatalysts => availableCatalysts;
        
        protected override void Subscribe()
        {
            args.TrySetData(Data.CastArgs);
        }

        protected override void Unsubscribe()
        {
            args.TrySetData(null);
        }
    }
}