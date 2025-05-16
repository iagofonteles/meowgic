using Drafts.DataView;
using Meowgic.UI;
using TMPro;
using UnityEngine;

namespace Meowgic.Match.UI
{
    public class SpellCastArgsView : DataView<SpellCastArgs>
    {
        [SerializeField] private ActorView caster;
        [SerializeField] private ActorView target;
        [SerializeField] private SpellView spell;
        [SerializeField] private CollectionView catalysts;
        [SerializeField] private TMP_Text speed;
        [SerializeField] private TMP_Text damage;
        [SerializeField] private TMP_Text heal;
        [SerializeField] private TMP_Text shield;
        [SerializeField] private GameObject cancelled;

        public CollectionView Catalysts => catalysts;

        protected override void Subscribe()
        {
            Data.OnModified += Repaint;
            Repaint();
        }

        protected override void Unsubscribe()
        {
            Data.OnModified -= Repaint;
            Repaint();
        }

        private void Repaint()
        {
            caster.TrySetData(Data?.Caster);
            target.TrySetData(Data?.target);
            spell.TrySetData(Data?.spell);
            catalysts.TrySetData(Data?.Catalysts);
            catalysts.TrySetActive(Data?.spell);

            speed.TrySetText(Data?.speed);
            damage.TrySetText(Data?.damage);
            heal.TrySetText(Data?.heal);
            shield.TrySetText(Data?.shield);

            speed.TrySetActive(Data?.speed != 0);
            damage.TrySetActive(Data?.damage != 0);
            heal.TrySetActive(Data?.heal != 0);
            shield.TrySetActive(Data?.shield != 0);

            cancelled.TrySetActive(Data?.cancelled ?? false);

            UpdateCosts();
        }

        private void UpdateCosts()
        {
            for (var i = 0; i < Data.cost.Count; i++)
            {
                var cost = Data.cost[i];
                var icon = catalysts.GetView<DatabaseItemSOView>(i)?.Icon;
                if (icon) icon.sprite = cost.Icon;
            }
        }
    }
}