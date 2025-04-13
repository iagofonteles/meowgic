using System;
using Drafts.DataView;
using UnityEngine;
using UnityEngine.Events;

namespace Drafts.Inventory
{
    public class ISlotView : DataView<ISlot>
    {
        [SerializeField] private DataView.DataView itemView;
        [SerializeField] private FormattedText countText;

        [Serializable]
        public class SlotCallbacks
        {
            public UnityEvent<int> onAmountChanged;
            public UnityEvent<object> onItemChanged;
            public UnityEvent<bool> onFavoriteChanged;
            public UnityEvent<bool> notZero;
            public UnityEvent onModified;
        }

        [Space] public SlotCallbacks slotCallbacks = new();

        private void Awake()
        {
            if (itemView)
                slotCallbacks.onItemChanged.AddListener(itemView.SetData);
            slotCallbacks.onItemChanged.AddListener(_ => slotCallbacks.onModified.Invoke());

            slotCallbacks.onAmountChanged.AddListener(i => slotCallbacks.notZero.Invoke(i != 0));
            slotCallbacks.onAmountChanged.AddListener(_ => slotCallbacks.onModified.Invoke());

            if (countText)
                slotCallbacks.onAmountChanged.AddListener(countText.SeValue);

            slotCallbacks.onFavoriteChanged.AddListener(_ => slotCallbacks.onModified.Invoke());
        }

        protected override void Subscribe()
        {
            if (Data is IDynamicSlot s)
            {
                s.OnItemChanged += slotCallbacks.onItemChanged.Invoke;
                s.OnAmountChanged += slotCallbacks.onAmountChanged.Invoke;
                s.OnFavoriteChanged += slotCallbacks.onFavoriteChanged.Invoke;
            }

            slotCallbacks.onItemChanged.Invoke(Data.Item);
            slotCallbacks.onAmountChanged.Invoke(Data.Amount);
            slotCallbacks.onFavoriteChanged.Invoke(Data.Favorite);
        }

        protected override void Unsubscribe()
        {
            if (Data is IDynamicSlot s)
            {
                s.OnItemChanged -= slotCallbacks.onItemChanged.Invoke;
                s.OnAmountChanged -= slotCallbacks.onAmountChanged.Invoke;
                s.OnFavoriteChanged -= slotCallbacks.onFavoriteChanged.Invoke;
            }

            slotCallbacks.onItemChanged.Invoke(default);
            slotCallbacks.onAmountChanged.Invoke(0);
            slotCallbacks.onFavoriteChanged.Invoke(false);
        }
    }
}