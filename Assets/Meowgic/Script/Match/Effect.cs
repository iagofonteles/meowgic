namespace Meowgic.Match
{
    public abstract class Effect
    {
        // /// <summary>Execute when preparing the spells, should not modify targets yet.</summary>
        // public abstract void ModifyPreview(int index, IReadOnlyList<SpellPreparation> args);
        //
        // /// <summary>Execute when preparing the spells.</summary>
        // public abstract void Prepare(int index, IReadOnlyList<SpellPreparation> preparation);

        /// <summary>Invoked after all preparations and before any cast.</summary>
        public virtual void OnTurnBegin(int castIndex, TurnArgs turnArgs) { }

        /// <summary>Invoked after all OnTurnBegin.</summary>
        public virtual void OnTurnExecute(int castIndex, TurnArgs turnArgs) { }

        /// <summary>Invoked after all OnTurnExecute.</summary>
        public virtual void OnTurnEnd(int castIndex, TurnArgs turnArgs) { }
    }
}