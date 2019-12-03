using System;
namespace CliffGame
{
    public class Player
    {
        public string name { get; set; }
        public Hand hand;
        public int currentScore = 0;

        public Player()
        {
            name = "";
        }

        public void DisplayHand() {

            Console.WriteLine("\n"+name+"'s Hand:\n");
            //Display
            foreach (Card card in hand.cards)
            {
                Console.WriteLine("\t"+card.name);
            }
            
        }
    }
}
