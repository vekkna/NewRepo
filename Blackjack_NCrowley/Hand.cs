﻿using System.Collections.Generic;
using System.Linq;

namespace Blackjack_NCrowley
{
    /// <summary>
    /// Player's (and Dealer's) hand, made up of cards, and which calculates cards' total value
    /// </summary>
    internal class Hand
    {
        // Properties

        // The contents of the hand
        public List<Card> Cards { get; }

        // The value of the hand
        public int Value
        {
            get
            {
                // Add the values of the cards in hand (faces are ten, ace is eleven)
                int value = (from card in Cards select Deck.Ranks[card.Rank]).Sum();
                // Count how many aces are in the hand
                int numAces = (from card in Cards where card.Rank is "Ace" select card).Count();
                // While hand is bust and contains an ace valued at 11
                while (numAces > 0 && value > 21)
                {
                    // change one ace's value to 1
                    numAces -= 1;
                    value -= 10;
                }
                return value;
            }
        }

        //ctor - no need for any others
        public Hand()
        {
            Cards = new List<Card>();
        }

        /// <summary>
        /// Add a card to the hand
        /// </summary>
        /// <param name="card">The Card to add</param>
        public void AddCard(Card card)
        {
            Cards.Add(card);
        }

        /// <summary>
        /// Discards the hand, clearing the card array and returning the cards discarded so they can be sent to the bottom of the deck.
        /// </summary>
        /// <returns>The discarded Cards</returns>
        public List<Card> Discard()
        {
            // copy the cards so we can return them after clearing them.
            var discards = new List<Card>(Cards);
            Cards.Clear();
            return discards;
        }

        /// <summary>
        /// Creates a string of the hand's cards, separated by commas or "and" before the last one.
        /// </summary>
        /// <returns>String representation of cards in hand</returns>
        public override string ToString()
        {
            if (Cards.Count <= 2)
            {
                return string.Join(" and ", Cards);
            }
            // Get strings of all cards from Cards except the last one (slicing like this needs C#8)
            var allExceptLast = Cards.Select(card => card.ToString()).ToArray()[..^1];
            // Get string of last one
            var last = Cards[^1].ToString();
            // join the first group with commas, then add the last one with an "and".
            return $"{string.Join(", ", allExceptLast)} and {last}";
        }
    }
}