using System.Collections.Generic;
using System.Linq;
using Drafts;

namespace Meowgic.Match
{
    public class EnemyAi
    {
        private readonly Enemy _enemy;

        public EnemyAi(Enemy enemy) => _enemy = enemy;

        public IEnumerable<SpellPreparation> GetPreparation(Actor actor)
        {
            var list = new ObservableList<SpellPreparation>();
            var item = new SpellPreparation(actor, list);
            list.Add(item);
            item.Spell.Value = _enemy.Spells.Random(1).First();
            return list;
        }
    }
}