﻿using CasinoBot.Common.Extensions;
using CasinoBot.Domain.Interfaces;

namespace CasinoBot.Domain.Models.Decks
{
    public class Deck<T> : IDeck<T> where T : ICard
    {
        private List<T> _cards;
        public int Count => _cards.Count;

        public Deck()
        {
            _cards = new List<T>();
        }

        public T Draw()
        {
            if (Count == 0) throw new InvalidOperationException("The deck is currently empty");

            var cardToDeal = _cards[0];
            _cards.RemoveAt(0);
            return cardToDeal;
        }

        public IEnumerable<T> Draw(int numberOfCards)
        {
            if (_cards.Count < numberOfCards) throw new InvalidOperationException($"There are not enough cards in the deck to draw {numberOfCards} cards. Current count: {Count}");

            var cardsToDeal = _cards.Take(numberOfCards);
            _cards.RemoveRange(0, numberOfCards);
            return cardsToDeal;
        }

        public IEnumerable<IEnumerable<T>> Deal(int numberOfPlayers, int numberOfCards)
        {
            var totalNumberOfCardsToDeal = numberOfPlayers * numberOfCards;
            if (_cards.Count < totalNumberOfCardsToDeal) throw new InvalidOperationException($"There are not enough cards in the deck to draw {numberOfCards} cards for {numberOfPlayers}. Current count: {Count}");

            List<List<T>> handsToDeal = new List<List<T>>(numberOfPlayers);

            for (int i = 0; i < totalNumberOfCardsToDeal; i++)
            {
                if (i < numberOfPlayers)
                {
                    handsToDeal.Add(new List<T>());
                }
                handsToDeal[i % numberOfPlayers].Add(_cards[i]);
            }

            _cards.RemoveRange(0, totalNumberOfCardsToDeal);
            return handsToDeal;
        }

        public IDeck<T> Shuffle()
        {
            if (Count == 0) throw new InvalidOperationException("The deck is currently empty");
            _cards = (List<T>)_cards.Shuffle();
            return this;
        }

        public void Add(T card)
        {
            _cards.Add(card);
        }

        public void Add(IEnumerable<T> cards)
        {
            _cards.AddRange(cards);
        }
    }
}
