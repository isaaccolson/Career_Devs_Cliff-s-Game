using System;
using System.Threading;

namespace CliffGame
{
    public static class ScrollingText
    {
        public static void ScrollUp(string text) {

            var space = "\n";
            for (int i = 0; i < 25; i++)
            {
                Console.Clear();
                space += "\n";
                Console.WriteLine(space + "\t\t\t" + text);
                Thread.Sleep(100);
               
            }
            Console.Clear();
        }
    }
}
