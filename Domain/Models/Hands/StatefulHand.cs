using CasinoBot.Domain.Interfaces;
using CasinoBot.Common.Extensions;
using System.Collections;

namespace CasinoBot.Domain.Models.Hands
{
    public class StatefulHand<T, U> : IStatefulHand<T, U> where T : ICard where U : class, new()
    {
        private List<(T Card, U State)> _cards;

        public int Count => _cards.Count;

        public StatefulHand()
        {
            _cards = new List<(T, U)>();
        }

        public (T Card, U State) this[int index] => _cards[index];

        T IHand<T>.this[int index] => _cards[index].Card;

        public void AddCard(T card)
        {
            AddCard(card, new U());
        }

        public void AddCard(T card, U state)
        {
            _cards.Add((card, state));
        }

        public (T Card, U State) RemoveAt(int index)
        {
            var card = _cards[index];
            _cards.RemoveAt(index);
            return card;
        }

        T IHand<T>.RemoveAt(int index)
        {
            var card = _cards[index].Card;
            _cards.RemoveAt(index);
            return card;
        }


        public IStatefulHand<T, U> Shuffle()
        {
            if (Count == 0) throw new InvalidOperationException("The hand is currently empty");
            _cards.Shuffle();
            return this;
        }

        IHand<T> IHand<T>.Shuffle()
        {
            return Shuffle();
        }


        #region IEnumerable

        public IEnumerator<(T Card, U State)> GetEnumerator()
        {
            return _cards.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return _cards.Select(x => x.Card).GetEnumerator();
        }

        #endregion
    }
}
