using System;
using System.Collections.Generic;

namespace CliffGame
{
    public class Game
    {

        //card stuff
        private const int numOfSuits = 4;
        private const int highestCardValue = 13;
        private const int numJokers = 0;

        //rules
        private const int handLimit = 8;
        private const int turnNumber = 2;

        //players
        private const int numberPlayers = 4;

        List<Player> players;

        Deck deck;

        public Game()
        {
            
            players = new List<Player>();

            deck = new Deck(numOfSuits, highestCardValue, numJokers);

            for (var i = 0; i < numberPlayers; i++)
            {
                players.Add(new Player());
            }

            var counter = 1;
            foreach (Player player in players)
            {
                player.name = "Player " + counter;
                counter++;
            }

        }

        public static void WaitForEnter() {
            while (Console.ReadKey().Key != ConsoleKey.Enter)
            {
            }
        }

        public void SetUpGame() {


            Console.WriteLine("Press Enter to Shuffle and Deal out 8 cards per player.");

            WaitForEnter();

            //Initial Set Up
            deck.Shuffle();

            ScrollingText.ScrollUp("Shuffle shuffle shuffle");
       
            foreach (Player player in players)
            { 
                //Deal
                player.hand = deck.Deal(handLimit);

                //Sort
                player.hand = Hand.SortHand(player.hand);

            }

            ScrollingText.ScrollUp("Deal deal deal");

        }

        public void GameOver(Player player) {

            Console.Clear();

            player.DisplayHand();

            Console.WriteLine("\n"+player.name + " Bonuses:\n");

            player.currentScore = CalculateScore.Score(player.hand);
            player.currentScore += CalculateScore.FaceCardScore(player.hand);
            player.currentScore += CalculateScore.MatchScore(player.hand.cards);
            player.currentScore += CalculateScore.StraightScore(player.hand);

            Console.WriteLine("\nScore: " + player.currentScore + "\n");

            WaitForEnter();

        }

        public void Discarding(Player player) {

            List<Card> newHand;
            
            while (true)
            {

            Console.WriteLine("Choose Cards to Discard. \n(1 for first card, 23 for second and third card, etc.)");
            Console.WriteLine("\tCan only discard 1,2,3,5, or 8 cards.");
            Console.WriteLine("\tNo discarding duplicate numbers.");
            Console.WriteLine("\tIf you discard 3 or more, one card must be a face card, if present.");

            var discardInput = Console.ReadLine();

            Console.Clear();

            List<Card> tempHand = new List<Card>(player.hand.cards);
            List<Card> tempDiscard = new List<Card>();

            
                foreach (char input in discardInput)
                {

                    int index = (int)Char.GetNumericValue(input) - 1;
                    Console.WriteLine("\tDiscarded: " + player.hand.cards[index].name);
                    tempDiscard.Add(player.hand.cards[index]);
                    tempHand.Remove(player.hand.cards[index]);

                }

                var validHandMessage = CheckForValidHand(tempDiscard, player.hand.HasFaceCard());

                if (validHandMessage == "")
                {
                    newHand = tempHand;
                    foreach (Card card in tempDiscard) {
                        deck.discardPile.Add(card);
                    }
                    break;
                }
                else {
                    Console.WriteLine(validHandMessage);
                    WaitForEnter();
                    Console.Clear();
                    player.DisplayHand();
                    
                }

            }
            

            player.hand = new Hand(newHand);

            while (player.hand.cards.Count < handLimit)
            {
                player.hand.cards.Add(deck.DealSingleCard());
            }

            player.hand = Hand.SortHand(player.hand);

            player.DisplayHand();

            WaitForEnter();

        }

        public string CheckForValidHand(List<Card> _cards, bool hasFaceCard) {

            var returnMessage = "";

            if (_cards.Count == 4 || _cards.Count == 6 || _cards.Count == 7) {
                returnMessage = "Fib numbers only. \nTry Again.";
                return returnMessage;
            }


            if (_cards.Count > 2 && hasFaceCard && !Hand.HasFaceCardStatic(_cards)) {

                    returnMessage = "Need to discard face card when discarding more than 2 cards. \nTry Again.";
                    return returnMessage;
               
            }

            if (CalculateScore.MatchScore(_cards) > 0) {

                returnMessage = "Can't discard Duplicates. \nTry Again.";
                return returnMessage;

            }

            return returnMessage;
        }

        public void Play()
        {

            SetUpGame();

            for (var turn = 0; turn < turnNumber; turn++)
            {

                foreach (Player player in players)
                {
                    Console.Clear();

                    Console.WriteLine("Ready " + player.name);

                    WaitForEnter();

                    Console.Clear();

                    Console.WriteLine(player.name + " turn: " + (turn + 1));

                    player.DisplayHand();

                    WaitForEnter();

                    Discarding(player);

                }

            }

            //preparing end game
            var finalHands = new Hand[numberPlayers];
            for (var i = 0; i < numberPlayers; i++) {
                finalHands[i] = players[i].hand;
            }
            CalculateScore.RemoveFaceCards(finalHands,deck);

            foreach (Player player in players) {
                GameOver(player);
            }

            Console.ForegroundColor = ConsoleColor.Green;
            var winner = DetermineWinner.WhoWon(players);
            WaitForEnter();


            ScrollingText.ScrollUp(winner.name + " won! with points: " + winner.currentScore + "\n");
            ScrollingText.ScrollUp(winner.name + " won! with points: " + winner.currentScore + "\n");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Bye, bye now.");

            WaitForEnter();

        }


    }
}
