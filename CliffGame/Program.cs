using System;
using System.Threading;

namespace CliffGame
{//should handle input 
    class MainClass
    {


        public static void Main(string[] args)
        {
          //40[] in writeLine

            Console.WriteLine("\t\t\tCliff's Game");
            Game.WaitForEnter();

            ScrollingText.ScrollUp("Cliff's Game");
            ScrollingText.ScrollUp("Programmed by Isaac Colson");
       
            var game = new Game();

            game.Play();

        }
    }
}
