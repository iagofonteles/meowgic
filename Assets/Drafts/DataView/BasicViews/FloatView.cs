namespace Drafts.DataView
{
    public class FloatView : UnityEventView<float>
    {
        public override void SetData(object data)
        {
            if (data is int i) data = (float)i;
            base.SetData(data);
        }
    }
}