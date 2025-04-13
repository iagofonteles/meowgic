using System.Collections.Generic;
using System.Collections.Specialized;
namespace Drafts.Inventory {

	public interface IInventory : INotifyCollectionChanged {
		int Count(object item);
		delegate void ItemChangedHandler(object sender, object item, int amount);
		event ItemChangedHandler OnItemChanged;
		IEnumerable<ISlot> Slots { get; }
	}

}
