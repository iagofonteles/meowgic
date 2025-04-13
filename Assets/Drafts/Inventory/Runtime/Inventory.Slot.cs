using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Drafts.Inventory {

	/// <summary>Inventory with finite slots. Can Favorite item to reserve the slot.
	/// Favorited slots will show "Item x0" instead of removing the item from the slot.</summary>
	[Serializable]
	public class SlotInventory<ITEM> : IInventory, IEnumerable<IReadOnlySlot<ITEM>> {
		[SerializeField] List<Slot<ITEM>> slots;

		readonly Func<ITEM, int> maxStack; // function to extract max stack size of a specific item

		IEnumerable<Slot<ITEM>> _freeSlots => slots.Where(s => s.IsEmpty);
		IEnumerable<Slot<ITEM>> _slotsWith(ITEM item) => slots.Where(s => !s.IsEmpty && s.Item.Equals(item));

		public IEnumerable<IReadOnlySlot<ITEM>> FreeSlots => _freeSlots;
		public IEnumerable<IReadOnlySlot<ITEM>> SlotsWith(ITEM item) => _slotsWith(item);
		public IReadOnlySlot<ITEM> this[int index] => slots[index];

		public event IInventory.ItemChangedHandler OnItemChanged;
		public event NotifyCollectionChangedEventHandler CollectionChanged;

		/// <param name="slotCount">Fixed Number of slots</param>
		/// <param name="maxStack">If you dont want to specify for each item, just put "i=>99" for fixed stack size or leave it null for unlimited.</param>
		public SlotInventory(int slotCount, Func<ITEM, int> maxStack = null) {
			slots = new List<Slot<ITEM>>();
			for(int i = 0; i < slotCount; i++) slots.Add(new());
			this.maxStack = maxStack ?? (i => int.MaxValue);
		}

		public int Add(ITEM item, int count = 1) {
			if(count < 0) return Remove(item, -count);
			if(item == null) throw new ArgumentNullException("item cannot be null.");
			var initCount = count;

			int stackSize = maxStack(item); // max stack value

			// insert in slots current with the item before using free slots
			foreach(var slot in _slotsWith(item)) {
				if(count == 0) break;
				var v = Math.Min(stackSize - slot.Amount, count);
				slot.Amount += v;
				count -= v;
			}

			// try empty slots
			foreach(var slot in _freeSlots) {
				if(count == 0) break;
				var v = Math.Min(stackSize, count);
				slot.Item = item;
				slot.Amount = v;
				count -= v;
			}

			var delta = initCount - count;
			if(delta != 0) OnItemChanged?.Invoke(this, item, delta);
			return count; // return the amout that could not be inserted
		}

		public int Remove(ITEM item, int count = 1) {
			if(count < 0) return Add(item, -count);
			if(item == null) throw new ArgumentNullException("item cannot be null.");
			var initCount = count;

			// slots current with the item
			foreach(var s in _slotsWith(item)) {
				if(count == 0) break;
				var v = Math.Min(s.Amount, count);
				s.Amount -= v;
				count -= v;
			}

			var delta = initCount - count;
			if(delta != 0) OnItemChanged?.Invoke(this, item, -delta);
			return count; // return the amout that could not be removed
		}

		/// <summary>If Count >= amout, return true and remove items from inventory.</summary>
		public bool Use(ITEM item, int amount = 1) { if(Count(item) < amount) return false; Remove(item, amount); return true; }
		public int Count(ITEM item) => _slotsWith(item).Sum(s => s.Amount);
		public int SpaceFor(ITEM item) => _freeSlots.Count() * maxStack(item) + _slotsWith(item).Sum(s => maxStack(item) - s.Amount);
		public bool Fits(ITEM item) => slots.Any(s => s.IsEmpty || (item.Equals(s.Item) && s.Amount < maxStack(item)));
		public bool Contains(ITEM item) => _slotsWith(item).Any();
		public bool Contains(ITEM item, int amount) => Count(item) >= amount;

		/// <summary>Number of slots current in inventory.</summary>
		public int Length {
			get => slots.Count;
			set {
				var delta = value - slots.Count;
				if(delta < -_freeSlots.Count()) throw new Exception("Not enough free slots to remove.");
				for(; delta < 0; delta--) slots.Add(new());

				bool RemoveIf(Slot<ITEM> slot) {
					if(delta > 0 || !slot.IsEmpty) return false;
					delta++; return true;
				}
				slots.RemoveAll(RemoveIf);
			}
		}

		public void Clear(bool clearFavorites = false) {
			for(int i = slots.Count - 1; i >= 0; i--) {
				if(clearFavorites) slots[i].Favorite = false;
				slots[i].Amount = 0;
			}
		}

		public void Sort(Comparer<Slot<ITEM>> comparer, bool executeUpdate) => throw new NotImplementedException();

		/// <summary>WARNING: this does not trigger any callbacks.</summary>
		public void LoadSlots(IEnumerable<Slot<ITEM>> slots) {
			this.slots.Clear();
			this.slots.AddRange(slots);
		}

		public IEnumerator<IReadOnlySlot<ITEM>> GetEnumerator() => slots.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => slots.GetEnumerator();
		IEnumerable<ISlot> IInventory.Slots => slots;

		int IInventory.Count(object item) => item is ITEM i ? Count(i) : 0;
	}
}
