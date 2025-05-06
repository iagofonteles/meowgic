using System;
using System.Collections.Generic;
using UnityEngine;

namespace Meowgic.Match
{
    public static class BattleMath
    {
        public static bool LogMessages = true;

        private static void Log(string message, UnityEngine.Object obj = null)
        {
            if (!LogMessages) return;
            Debug.Log(message, obj);
        }

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

            Log($"{caster.DisplayName} dealt {delta} damage to {target.DisplayName}");
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

                Log($"{target.DisplayName} healed by {delta}");
            }
        }

        public static void Shield(IEnumerable<Actor> targets, int shield)
        {
            if (shield <= 0) return;

            foreach (var target in targets)
            {
                if (target.IsDead) continue;
                target.Shield.Value += shield;
                
                Log($"{target.DisplayName} shielded by {shield}");
            }
        }
    }
}