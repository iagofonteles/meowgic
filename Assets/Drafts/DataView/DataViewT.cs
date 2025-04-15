namespace Drafts.DataView
{
    public abstract class DataView<T> : DataView
    {
        private T _dataT;

        public virtual T Data
        {
            get => _dataT;
            set => SetData(value);
        }

        public override object GetData() => _dataT;

        public override void SetData(object data)
        {
            if (_dataT is not null) Unsubscribe();
            _dataT = data is T v ? v : default;
            if (_dataT is not null) Subscribe();
            base.SetData(data);
        }

        protected abstract void Subscribe();
        protected abstract void Unsubscribe();

        protected virtual void OnDestroy()
        {
            if (_dataT != null) Unsubscribe();
        }
    }
}