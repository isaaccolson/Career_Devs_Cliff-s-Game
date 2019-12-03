using System;
using System.Collections.Generic;

namespace CliffGame
{
    public class Hand
    {
        
        public Deck deck;

        public List<Card> cards = new List<Card>();

        public Hand(List<Card> _cards)
        {
            cards = _cards;
            
        }

        public bool ValueExist(int _value) {

            foreach (Card card in cards)
            {
                if (card.value == _value)
                {
                    return true;
                }
            }

            return false;
        }

        public bool HasFaceCard() {

           return HasFaceCardStatic(cards);

        }

        public static bool HasFaceCardStatic(List<Card> cards)
        {

            foreach (Card card in cards)
            {
                if (card.value > 10)
                {
                    return true;
                }
            }

            return false;

        }

        public void DiscardSingleCard(Card card, Deck deck)
        {
            deck.discardPile.Add(card);
            cards.Remove(card);
        }

        public void DiscardSingleCardByValue(int value, Deck deck)
        {
            foreach (Card card in cards) {
                if (card.value == value) {
                    DiscardSingleCard(card, deck);
                    ScrollingText.ScrollUp("Royal Family Assassination: " + card.name);
                    return;
                }
            }
        }


        public static Hand SortHand(Hand _finalHand)
        {
            List<Card> tempHand = new List<Card>(_finalHand.cards);

            for (var i = 0; i < tempHand.Count - 1; i++)
            {

                Card lowestCard = new Card(tempHand[i]);

                for (var j = i + 1; j < tempHand.Count; j++)
                {
                    if (tempHand[j].value < lowestCard.value)
                    {

                        var tempLowest = new Card(lowestCard);
                        lowestCard = new Card(tempHand[j]);

                        tempHand[j] = tempLowest;
                        tempHand[i] = lowestCard;

                    }
                }

            }

            return new Hand(tempHand);
        }

    }
}
