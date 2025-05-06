using System;

namespace Meowgic.Match.EffectScripts
{
    [Serializable]
    public abstract class CastArgsStat
    {
        public abstract int GetStat(SpellCastArgs args);
        public abstract void SetStat(SpellCastArgs args, int value);
    }
}

namespace Meowgic.Match.EffectScripts.CastArgsStats
{
    public class Damage : CastArgsStat
    {
        public override int GetStat(SpellCastArgs args) => args.damage;
        public override void SetStat(SpellCastArgs args, int value) => args.damage = value;
    }

    public class Heal : CastArgsStat
    {
        public override int GetStat(SpellCastArgs args) => args.heal;
        public override void SetStat(SpellCastArgs args, int value) => args.heal = value;
    }

    public class Shield : CastArgsStat
    {
        public override int GetStat(SpellCastArgs args) => args.shield;
        public override void SetStat(SpellCastArgs args, int value) => args.shield = value;
    }

    public class Speed : CastArgsStat
    {
        public override int GetStat(SpellCastArgs args) => args.speed;
        public override void SetStat(SpellCastArgs args, int value) => args.speed = value;
    }
}