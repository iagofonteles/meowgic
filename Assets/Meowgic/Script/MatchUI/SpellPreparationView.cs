using Drafts.DataView;
using Meowgic.UI;
using UnityEngine;

namespace Meowgic.Match.UI
{
    public class SpellPreparationView : DataView<SpellPreparation>
    {
        [SerializeField] private SpellView spell;
        [SerializeField] private CollectionView catalysts;
        [SerializeField] private CollectionView availableSpells;
        [SerializeField] private CollectionView availableCatalysts;

        public CollectionView Catalysts => catalysts;
        public CollectionView AvailableSpells => availableSpells;
        public CollectionView AvailableCatalysts => availableCatalysts;

        
        protected override void Subscribe()
        {
            spell.TrySetData(Data.Spell.Value);
            catalysts.TrySetData(Data.Catalysts);

            Data.Spell.OnChanged += SpellChanged;
            Data.Spell.OnChanged += spell.TrySetData;
        }

        protected override void Unsubscribe()
        {
            spell.TrySetData(null);
            catalysts.TrySetData(null);

            Data.Spell.OnChanged -= SpellChanged;
            Data.Spell.OnChanged -= spell.TrySetData;
        }

        private void SpellChanged(object obj)
        {
            catalysts.TrySetData(Data.Catalysts);
            for (var i = 0; i < Data.Spell.Value.Cost.Length; i++)
            {
                var baseCatalyst = Data.Spell.Value.Cost[i];
                var icon = catalysts.GetView<DatabaseItemSOView>(i)?.Icon;
                if (icon) icon.sprite = baseCatalyst.Icon;
            }
        }
    }
}