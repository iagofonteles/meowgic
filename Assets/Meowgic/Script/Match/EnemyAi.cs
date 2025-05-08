namespace Meowgic.Match
{
    public class EnemyAi
    {
        public Spell GetSpell(Actor actor) => actor.Spells.Random();
    }
}