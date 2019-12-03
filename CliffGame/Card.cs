using System;
using System.Collections.Generic;

namespace CliffGame
{
    public class Card
    {
        public string name { get; private set; }
        string suit;
        string type;
        public int value { get; private set; }
        public int scoreValue { get; private set; }

        private readonly Dictionary<int, string> FACE_MAP = new Dictionary<int, string>() {
        {0, "Joker"},
        { 1, "Ace"},
        {11, "Jack"},
        {12, "Queen"},
        {13, "King"}
    };
        private readonly Dictionary<int, string> SUIT_MAP = new Dictionary<int, string>() {
        {-1, "No Suit"},
        { 0, "Hearts"},
        {1, "Diamonds"},
        {2, "Clubs"},
        {3, "Spades"}
    };

        public Card(int _suit, int _value)
        {
            suit = SUIT_MAP[_suit];
            value = _value;
            type = cardNameDisplay();
            name = type + " of " + suit;
            scoreValue = GenerateScoreValue();


        }

        public Card(Card _card)
        {

            suit = _card.suit;
            value = _card.value;
            type = _card.type;
            name = _card.name;
            scoreValue = _card.scoreValue;


        }

        private int GenerateScoreValue() {

            switch (value)
            {
                case 1:

                    return 11;

                case 11:
                case 12:
                case 13:

                    return 10;
                default:
                    return value;
            }

        }

        public string cardNameDisplay()
        {

            if (FACE_MAP.ContainsKey(this.value))
            {
                return this.FACE_MAP[this.value];
            }
            return this.value.ToString();
        }
    }
}
