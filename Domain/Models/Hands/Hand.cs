using Common.Extensions;
using Domain.Interfaces;
using System.Collections;

namespace Domain.Models.Hands
{
    public class Hand<T> : IHand<T> where T : ICard
    {
        private List<(T Card, bool isPublic)> _cards;

        public int Count => _cards.Count;

        public Hand()
        {
            _cards = new List<(T, bool)>();
        }

        public (T Card, bool isPublic) this[int index] 
        { 
            get => _cards[index];
        }

        public void AddCard(T card)
        {
            AddCard(card, false);
        }

        public void AddCard(T card, bool isPublic)
        {
            _cards.Add((card, isPublic));
        }

        public T RemoveAt(int index)
        {
            var card = _cards[index].Card;
            _cards.RemoveAt(index);
            return card;
        }

        public void Shuffle()
        {
            if (Count == 0) throw new InvalidOperationException("The hand is currently empty");
            _cards.Shuffle();
        }

        #region IEnumerable

        public IEnumerator<(T Card, bool isPublic)> GetEnumerator()
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
