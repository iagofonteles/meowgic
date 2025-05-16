using Drafts;
using UnityEngine.Assertions;

namespace Meowgic.Match
{
    public partial class SpellCastArgs
    {
        ObservableList<Catalyst> Hand => caster.Side.Pool.Hand;

        public void SetCatalyst(int index, Catalyst value)
        {
            if (catalysts[index] == value) return;
            if (catalysts[index])
                Hand.Add(catalysts[index]);

            if (Hand.Remove(value))
                catalysts[index] = value;
        }

        private void ValidateCatalysts()
        {
            while (catalysts.Count > cost.Count)
            {
                if (catalysts[^1]) Hand.Add(catalysts[^1]);
                catalysts.RemoveAt(catalysts.Count - 1);
            }

            for (var i = 0; i < catalysts.Count; i++)
            {
                var c = catalysts[i];
                if (cost[i].Compatible(c)) continue;
                if(c) Hand.Add(c);
                catalysts[i] = null;
            }

            while (catalysts.Count < cost.Count)
                catalysts.Add(null);

            Assert.AreEqual(cost.Count, catalysts.Count);
        }
    }
}