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
            item.SetSpell(_enemy.Spells.ToList().Random());
            return list;
        }
    }
}