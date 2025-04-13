using System;
using System.Collections.Generic;
using Drafts;
using UnityEngine;

namespace Meowgic.Match
{
    [Serializable]
    public class CardPool<T>
    {
        [SerializeField] private Observable<int> handLimit = new();
        [SerializeField] private ObservableList<T> deck;
        [SerializeField] private ObservableList<T> hand = new();
        [SerializeField] private ObservableList<T> discard = new();
        [SerializeField] private ObservableList<T> removed = new();

        public Observable<int> HandLimit => handLimit;
        public ObservableList<T> Deck => deck;
        public ObservableList<T> Hand => hand;
        public ObservableList<T> Discard => discard;
        public ObservableList<T> Removed => removed;

        public CardPool(IEnumerable<T> deck)
        {
            this.deck = new(deck);
        }

        public void Draw(int amount)
        {
            amount = Math.Min(amount, handLimit.Value - hand.Count);
            if (amount < 0) return;

            if (deck.Count < amount)
                Reshuffle();

            var list = deck.Random(amount);
            foreach (var item in list)
            {
                deck.Remove(item);
                hand.Add(item);
            }
        }

        public void DiscardHand()
        {
            foreach (var item in hand)
                discard.Add(item);
            hand.Clear();
        }

        public void Reshuffle()
        {
            foreach (var item in discard)
                deck.Add(item);
            discard.Clear();
        }
    }
}