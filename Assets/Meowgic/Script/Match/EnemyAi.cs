using System.Collections.Generic;
using System.Linq;

namespace Meowgic.Match
{
    public class EnemyAi
    {
        private readonly Enemy _enemy;
        
        public EnemyAi(Enemy enemy) => _enemy = enemy;

        public IEnumerable<SpellPreparation> GetPreparation(Actor actor)
        {
            var list = new List<SpellPreparation>();
            list.Add(new(0, list)
            {
                caster = actor,
                spell = _enemy.Spells.Random(1).First(),
            });
            return list;
        }
    }
}