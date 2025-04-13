using System.Collections.Specialized;
using Drafts.DataView;
using TMPro;
using UnityEngine;

namespace Meowgic.Match.UI
{
    /// <summary>Player cat and enemies</summary>
    public class CatalystPoolView : CardPoolView<Catalyst> { }

    public abstract class CardPoolView<T> : DataView<CardPool<T>>
    {
        [SerializeField] private TMP_Text handLimit;
        [SerializeField] private CollectionView deck;
        [SerializeField] private CollectionView hand;
        [SerializeField] private CollectionView discard;
        [SerializeField] private CollectionView removed;

        protected override void Subscribe()
        {
            Data.Hand.CollectionChanged += UpdateHandLimit;
            Data.HandLimit.OnChanged += UpdateHandLimit;

            deck.TrySetData(Data.Deck);
            hand.TrySetData(Data.Hand);
            discard.TrySetData(Data.Discard);
            removed.TrySetData(Data.Removed);

            UpdateHandLimit(null);
        }

        protected override void Unsubscribe()
        {
            Data.Hand.CollectionChanged -= UpdateHandLimit;
            Data.HandLimit.OnChanged -= UpdateHandLimit;

            deck.TrySetData(null);
            hand.TrySetData(null);
            discard.TrySetData(null);
            removed.TrySetData(null);

            handLimit.TrySetText($"-/-");
        }

        private void UpdateHandLimit(object obj) => UpdateHandLimit(null, null);

        private void UpdateHandLimit(object sender, NotifyCollectionChangedEventArgs e)
        {
            handLimit.TrySetText($"{Data.Hand.Count}/{Data.HandLimit.Value}");
        }
    }
}