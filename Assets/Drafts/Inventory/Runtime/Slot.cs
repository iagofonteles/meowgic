using UnityEngine;
using System;

namespace Drafts.Inventory {

	public interface ISlot {
		object Item { get; }
		int Amount { get; }
		bool Favorite { get; }
	}

	public interface IDynamicSlot : ISlot {
		event Action<object> OnItemChanged;
		event Action<int> OnAmountChanged;
		event Action<bool> OnFavoriteChanged;
	}

	public interface IReadOnlySlot<T> {
		T Item { get; }
		int Amount { get; }
		bool Favorite { get; }
	}

	/// <summary>Slot containing item, count and favorite flag for IInventory.</summary>
	[Serializable]
	public class Slot<T> : IDynamicSlot, IReadOnlySlot<T> {
		[SerializeField] int amount;
		[SerializeField] T item;
		[SerializeField] bool favorite;
		/// <summary>Delta count. When delta = 0, the favorite or item has changed.</summary>
		public event Action<bool> OnFavoriteChanged;
		public event Action<object> OnItemChanged;
		public event Action<int> OnAmountChanged;

		/// <summary>Actual item on this inventory slot.</summary>
		public T Item {
			get => item;
			set => OnItemChanged?.Invoke(item = value);
		}
		/// <summary>Quanty of the item.</summary>
		public virtual int Amount {
			get => amount;
			set {
				if(amount == value) return;
				OnAmountChanged?.Invoke(amount = value);
				if(IsEmpty) Item = default;
			}
		}
		/// <summary>Favorited itens are suposed the ocupy a slot even when Count is 0.</summary>
		public bool Favorite { get => favorite; set => OnFavoriteChanged?.Invoke(favorite = value); }
		/// <summary>Count is 0 and not favorited.</summary>
		public bool IsEmpty => Amount == 0 && !Favorite;

		object ISlot.Item => Item;

		public Slot(T item = default, int amount = 0, bool favorite = false) {
			this.item = item;
			this.amount = amount;
			this.favorite = favorite;
		}

		public override string ToString() => $"Slot<{typeof(T).Name}>: {Amount}x {Item}";
	}

	/// <summary>Reflect the current amount of a given item in on inventory. OnRemove and OnChange are called too.</summary>
	[Serializable, Obsolete]
	public class MirrorItem<T> : ISlot {
		IInventory Inventory { get; }
		[SerializeField] T item;
		[SerializeField] int amount;
		[SerializeField] bool favorite;

		public T Item => item;
		public int Amount => amount;
		public bool Favorite => favorite;
		public event Action OnModified;
		public event Action<object> OnItemChanged;
		public event Action<int> OnAmountChanged;
		object ISlot.Item => Item;

		public MirrorItem(IInventory inventory, T item) {
			this.item = item;
			amount = inventory.Count(item);
			Inventory.OnItemChanged += Mirror;
		}

		~MirrorItem() => Inventory.OnItemChanged -= Mirror;

		void Mirror(object sender, object item, int delta) {
			if(!item.Equals(Item)) return;
			amount += delta;
			OnAmountChanged?.Invoke(delta);
			OnModified.Invoke();
		}
	}
}
