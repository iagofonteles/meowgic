namespace Drafts.DataView
{
    public class IntView : UnityEventView<int>
    {
        public override void SetData(object data)
        {
            if (data is float f) data = (int)f;
            base.SetData(data);
        }
    }
}