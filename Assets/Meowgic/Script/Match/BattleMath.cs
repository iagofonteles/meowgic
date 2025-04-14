using System;
using System.Collections.Generic;

namespace Meowgic.Match
{
    public static class BattleMath
    {
        public static void Damage(Actor caster, Actor target, int damage)
        {
            if (damage <= 0) return;
            if (caster.IsDead || target.IsDead) return;

            //shield
            var delta = Math.Min(damage, target.Shield.Value);
            if (delta > 0) target.Shield.Value -= delta;
            damage -= delta;

            //health
            delta = Math.Min(damage, target.Health.Value);
            if (delta > 0) target.Health.Value -= delta;
            damage -= delta;
        }

        public static void Heal(IEnumerable<Actor> targets, int heal)
        {
            if (heal <= 0) return;

            foreach (var target in targets)
            {
                if (target.IsDead) continue;
                var maxDelta = target.MaxHealth.Value - target.Health.Value;
                var delta = Math.Min(heal, maxDelta);
                if (delta > 0) target.Health.Value += delta;
            }
        }

        public static void Shield(IEnumerable<Actor> targets, int shield)
        {
            if (shield <= 0) return;

            foreach (var target in targets)
            {
                if (target.IsDead) continue;
                target.Shield.Value += shield;
            }
        }
    }
}