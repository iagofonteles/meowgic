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
            Data.Spell.OnChanged += SpellChanged;
            Data.Spell.OnChanged += spell.TrySetData;

            SpellChanged(null);
        }

        protected override void Unsubscribe()
        {
            Data.Spell.OnChanged -= SpellChanged;
            Data.Spell.OnChanged -= spell.TrySetData;

            spell.TrySetData(null);
            catalysts.TrySetActive(false);
        }

        private void SpellChanged(object _)
        {
            spell.TrySetData(Data.Spell.Value);
            catalysts.TrySetData(Data.Catalysts);
            catalysts.TrySetActive(Data.Spell.Value);

            if (Data?.Spell.Value)
                for (var i = 0; i < Data.Spell.Value.Cost.Length; i++)
                {
                    var baseCatalyst = Data.Spell.Value.Cost[i];
                    var icon = catalysts.GetView<DatabaseItemSOView>(i)?.Icon;
                    if (icon) icon.sprite = baseCatalyst.Icon;
                }
        }
    }
}