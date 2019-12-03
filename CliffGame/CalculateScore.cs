using System;
using System.Collections.Generic;

namespace CliffGame
{
    public static class CalculateScore
    {
        public static int faceCardBonus = 5;
        public static int matchCardBonus = 2;
        public static int straightCardBonus = 3;

        public static int Score(Hand _finalHand)
        {

            var returnSum = 0;

            foreach (Card card in _finalHand.cards)
            {
                returnSum += card.scoreValue;
            }

            return returnSum;
        }

        public static List<int> ConvertHandToIntList(List<Card> listOfCards)
        {

            List<int> tempHand = new List<int>();

            foreach (Card card in listOfCards)
            {
                tempHand.Add(card.value);
            }

            return tempHand;

        }

        public static int StraightScore(Hand _finalHand)
        {

            List<int> tempHand = ConvertHandToIntList(_finalHand.cards);

            var straightBonusPoints = 0;

            for (var i = 0; i < tempHand.Count; i++)
            {
                var straightCounter = 0;
                int initialValue = tempHand[i];

                for (var j = 0; j < tempHand.Count; j++)
                {
                    if (tempHand[j] == initialValue + 1)
                    {
                        tempHand.Remove(tempHand[j]);
                        j -= 1;
                        straightCounter++;
                        initialValue++;
                    }

                }

                straightCounter -= 1;
                if (straightCounter > 0)
                {
                    straightBonusPoints += straightCounter * straightCardBonus;
                }

            }

            Console.WriteLine("\tStraights: " + straightBonusPoints);

            return straightBonusPoints;

        }

        public static int MatchScore(List<Card> _finalHand)
        {
            //Only count bonus points on second card and beyond

            List<int> tempHand = ConvertHandToIntList(_finalHand);
            var matchBonusPoints = 0;

            for (var i = 0; i < tempHand.Count; i = 0)
            {

                var cardValue = tempHand[i];
                var matchCounter = 0;

                for (var j = 0; j < tempHand.Count; j++)
                {
                    if (tempHand[j] == cardValue)
                    {
                        tempHand.Remove(tempHand[j]);
                        j -= 1;
                        matchCounter++;
                    }

                }

                matchBonusPoints += (matchCounter - 1) * matchCardBonus;

            }

            Console.WriteLine("\tMatches: " + matchBonusPoints);

            return matchBonusPoints;
        }

        public static int FaceCardScore(Hand _finalHand)
        {
            var scoreToAdd = 0;

            foreach (Card card in _finalHand.cards)
            {
                if (card.value > 10)
                {
                    scoreToAdd += faceCardBonus;
                }
            }

            scoreToAdd -= faceCardBonus;

            if (scoreToAdd <= 0)
            {
                scoreToAdd = 0;
            }
            Console.WriteLine("\tFace Cards: " + scoreToAdd);
            return scoreToAdd;
        }

        public static void RemoveFaceCards(Hand[] playerFinalHands, Deck deck)
        {
            for (var run = 0; run < 2; run++)
            {

                for (var value = 11; value <= 13; value++)
                {

                    var playerIndex = playerFinalHands.Length;

                    for (var player = 0; player < playerFinalHands.Length - 1; player++)
                    {
                        if (playerFinalHands[player].ValueExist(value))
                        {
                            playerIndex = player;
                            break;
                        }
                    }

                    bool removedCard = false;
                    for (var i = playerIndex + 1; i < playerFinalHands.Length; i++)
                    {

                        if (playerFinalHands[i].ValueExist(value))
                        {

                            playerFinalHands[i].DiscardSingleCardByValue(value, deck);
                            removedCard = true;

                        }
                    }
                    if (removedCard)
                    {
                        playerFinalHands[playerIndex].DiscardSingleCardByValue(value, deck);
                    }
                }
            }
        }
    }
}
