using Drafts.DataView;
using Meowgic.UI;

namespace Meowgic.Match.UI
{
    public class SpellPreparationView : DataView<SpellPreparation>
    {
        public SpellView spell;
        public CollectionView catalysts;

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
                var icon = catalysts.Views[i].GetComponent<DatabaseItemSOView>()?.Icon;
                if (icon) icon.sprite = baseCatalyst.Icon;
            }
        }
    }
}