using System.Linq;
using Drafts;
using Drafts.DataView;
using UnityEngine;

namespace Meowgic.Match.UI
{
    public class PreparationController : MonoBehaviour
    {
        [SerializeField] private CollectionView preparation;

        private void Start()
        {
            ActivateAvailable(null, false);
        }

        public void SelectSpell(SpellPreparationView view)
        {
            var hand = view.Data.Caster.Side.Pool.Hand;
            view.AvailableSpells.Data = view.Data.Caster.Spells;

            ActivateAvailable(view, true);
            bool CanCast(Spell s) => s.Cost.All(c => hand.Any(h => h.Base == c));
        }

        public void SelectCatalyst(CatalystView view)
        {
            var prep = view.GetComponentInParent<SpellPreparationView>();
            if (!prep.Data.Spell.Value)
            {
                ActivateAvailable(null, false);
                return;
            }

            var index = prep.Catalysts.Views.IndexOf(view);
            var catalyst = prep.Data.Spell.Value.Cost[index];
            var hand = prep.Data.Caster.Side.Pool.Hand;

            prep.AvailableCatalysts.Data = hand.Where(Meets);

            ActivateAvailable(prep, false);
            bool Meets(Catalyst c) => c.Base == catalyst;
        }

        private void ActivateAvailable(SpellPreparationView view, bool spells)
        {
            foreach (var v in preparation.GetViews<SpellPreparationView>())
                v.AvailableSpells.transform.parent.gameObject.SetActive(false);

            if (!view) return;
            view.AvailableSpells.transform.parent.gameObject.SetActive(true);
            view.AvailableSpells.gameObject.SetActive(spells);
            view.AvailableCatalysts.gameObject.SetActive(!spells);
        }
    }
}