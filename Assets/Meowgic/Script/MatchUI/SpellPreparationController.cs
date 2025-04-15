using System.Linq;
using Drafts;
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
            GetComponentInParent<PreparationController>().DeactivateAll();
            if (!root.Data.Spell.Value) return;

            _catalystIndex = root.Catalysts.Views.IndexOf(view);
            var catalyst = root.Data.Spell.Value.Cost[_catalystIndex];
            var hand = root.Data.Caster.Side.Pool.Hand;

            root.AvailableCatalysts.Data = hand.Where(catalyst.Compatible);
            ActivateAvailable(2);
        }

        private void ActivateAvailable(int kind)
        {
            root.AvailableSpells.transform.parent.gameObject.SetActive(kind > 0);
            root.AvailableSpells.gameObject.SetActive(kind == 1);
            root.AvailableCatalysts.gameObject.SetActive(kind == 2);
        }

        public void SetSpell(SpellView view) => root.Data.Spell.Value = view.Data;
        public void SetCatalyst(CatalystView view) => root.Data.Catalysts[_catalystIndex] = view.Data;
    }
}