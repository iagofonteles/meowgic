// using Drafts.Inventory;
// using System.Linq;
// using UnityEngine;
//
// namespace Drafts.UI {
//
// 	public class InventoryTotalCountView : DataView<IInventory> {
//
// 		[SerializeField] FormattedText countText;
// 		int count;
//
// 		protected override void Subscribe() {
// 			count = Data.Slots.Sum(s => s.Item != null ? s.Amount : 0);
// 			Data.OnItemChanged += OnItemChanged;
// 		}
//
// 		protected override void Unsubscribe() {
// 			count = 0;
// 			Data.OnItemChanged += OnItemChanged;
// 		}
//
// 		private void OnItemChanged(object sender, object item, int amount) {
// 			count += amount;
// 			Refresh();
// 		}
//
// 		protected override void Repaint() => countText.TrySetValue(count);
// 	}
// }
