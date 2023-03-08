using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part_5._5_More_Classes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //frequency();

            string bar = new string('=', Console.WindowWidth);
            string title = " Welcome to DICE MANIA! ";
            Console.WriteLine(bar);
            Console.SetCursorPosition((Console.WindowWidth / 2) - (title.Length / 2), 0);
            Console.WriteLine(title);

            Console.ReadLine();

            void frequency()
            {
                int[] loads = new int[] { 2, 4 }; //loaded numbers should not be consecutive
                Die dice6Sides = new Die(6, 1, loads);
                int[] rolls = new int[1000];
                int roll = 0;

                for (int i = 0; i < 1000; i++)
                {
                    roll = dice6Sides.RollWeightedDie();
                    rolls[i] = roll;
                }

                foreach (int unique in rolls.Distinct<int>())
                {
                    Console.WriteLine("Roll: {0} Frequency: {1}", unique, rolls.Count(x => x == unique));
                }

                Console.ReadLine();
            }
            
        }
    }
}
