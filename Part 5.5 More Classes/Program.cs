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
            Die dice6Sides = new Die(6, 6);

            Console.WriteLine(dice6Sides.Roll);

            for(int i = 0; i < 20; i++)
            {
                Console.WriteLine(dice6Sides.RollDie());
            }
            Console.ReadLine();
        }
    }
}
