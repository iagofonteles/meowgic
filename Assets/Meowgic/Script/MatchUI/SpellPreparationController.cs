using System.Linq;
using Drafts;
using Drafts.DataView;
using Meowgic.UI;
using UnityEngine;

namespace Meowgic.Match.UI
{
    public class SpellPreparationController : MonoBehaviour
    {
        [SerializeField] private SpellPreparationView root;

        private int _catalystIndex;

        public void SelectSpell()
        {
            GetComponentInParent<PreparationController>().DeactivateAll();
            // var hand = root.Data.Caster.Side.Pool.Hand;
            root.AvailableSpells.Data = root.Data.Caster.Spells;

            ActivateAvailable(1);
            // bool CanCast(Spell s) => s.Cost.All(c => hand.Any(h => h.Base == c));
        }

        public void SelectCatalyst(CatalystView view)
        {
            SelectCatalyst(root.Catalysts.Views.IndexOf(view));
        }

        public void SelectCatalyst(int index)
        {
            GetComponentInParent<PreparationController>().DeactivateAll();
            if (!root.Data.Spell.Value) return;

            _catalystIndex = index;
            var catalyst = root.Data.Spell.Value.Cost[index];
            var hand = root.Data.Caster.Side.Pool.Hand;

            root.AvailableCatalysts.Data = hand.Where(catalyst.Compatible);
            ActivateAvailable(2);
            root.Catalysts.GetView<CatalystView>(index).Selection.TrySetActive(true);
        }

        public void ActivateAvailable(int kind)
        {
            foreach (var view in root.Catalysts.GetViews<CatalystView>())
                view.Selection.TrySetActive(false);

            root.AvailableSpells.transform.parent.gameObject.SetActive(kind > 0);
            root.AvailableSpells.gameObject.SetActive(kind == 1);
            root.AvailableCatalysts.gameObject.SetActive(kind == 2);
        }

        public void SetSpell(SpellView view)
        {
            root.Data.SetSpell(view.Data);
            SelectCatalyst(0);
        }

        public void SetCatalyst(CatalystView view)
        {
            root.Data.SetCatalyst(_catalystIndex, view.Data);

            //select next
            if (_catalystIndex < root.Data.Spell.Value.Cost.Length - 1)
                SelectCatalyst(_catalystIndex + 1);
            else ActivateAvailable(0);
        }
    }
}