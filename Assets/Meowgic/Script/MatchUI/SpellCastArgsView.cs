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
        [SerializeField] private CollectionView cost;
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
            cost.TrySetData(Data?.Cost);
            catalysts.TrySetData(Data?.catalysts);
            catalysts.TrySetActive(Data?.spell);

            speed.TrySetText(Data?.speed);
            damage.TrySetText(Data?.damage);
            heal.TrySetText(Data?.heal);
            shield.TrySetText(Data?.shield);

            cancelled.TrySetActive(Data?.cancelled ?? false);
        }
        
        private void UpdateCosts()
        {
            for (var i = 0; i < Data.Cost.Count; i++)
            {
                var cost = Data.Cost[i];
                var icon = catalysts.GetView<DatabaseItemSOView>(i)?.Icon;
                if (icon) icon.sprite = cost.Icon;
            }
        }
    }
}