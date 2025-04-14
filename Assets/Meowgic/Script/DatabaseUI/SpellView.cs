using Drafts.DataView;

namespace Meowgic.UI
{
    public class SpellView : DataView<Spell>
    {
        public DatabaseItemSOView baseView;

        protected override void Subscribe()
        {
            baseView.TrySetData(Data);
        }

        protected override void Unsubscribe()
        {
            baseView.TrySetData(null);
        }
    }
}