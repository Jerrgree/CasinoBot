using Common.Extensions;
using Domain.Interfaces;
using System.Collections;

namespace Domain.Models.Hands
{
    public class Hand<T> : IHand<T> where T : ICard
    {
        private List<T> _cards;

        public int Count => _cards.Count;

        public Hand()
        {
            _cards = new List<T>();
        }

        public T this[int index] => _cards[index];

        public void AddCard(T card)
        {
            _cards.Add(card);
        }

        public T RemoveAt(int index)
        {
            var card = _cards[index];
            _cards.RemoveAt(index);
            return card;
        }

        public void Shuffle()
        {
            if (Count == 0) throw new InvalidOperationException("The hand is currently empty");
            _cards.Shuffle();
        }

        #region IEnumerable

        public IEnumerator<T> GetEnumerator()
        {
            return _cards.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
