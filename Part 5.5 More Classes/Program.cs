using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Media;
using System.Runtime.Remoting.Messaging;

namespace Part_5._5_More_Classes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string bar = new string('=', Console.WindowWidth);
            string title = " WELCOME TO DICE MANIA! ";
            Console.WriteLine(bar);
            Console.SetCursorPosition((Console.WindowWidth / 2) - (title.Length / 2), Console.CursorTop - 2);
            Console.WriteLine(title);

            bool exit = false;
            while (exit == false)
            {
                Console.Write(">>>");
                Parse(Console.ReadLine());
            }
            
            void Parse(string commandString)
            {
                List<string> commandParts = commandString.Split(' ').ToList();
                string command = commandParts.First();
                List<string> arguments = commandParts.Skip(1).ToList();

                switch (command){

                    case "help":
                        string[] lines = File.ReadAllLines(@"help.txt");
                        foreach(string line in lines)
                        {
                            Console.WriteLine(line);
                        }
                        break;
                    case "play":
                        play();
                        break;
                    case "exit":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Not a valid command. Use 'help' for list of valid commands");
                        break;
                }
            }

            void play()
            {
                Console.ForegroundColor = ConsoleColor.Green;
                int balance = 100;
                Console.WriteLine($"You currently have: {balance.ToString("C")}");
                Console.WriteLine("Choose a betting option fom the following:");
                string[] lines = File.ReadAllLines(@"bets.txt");
                foreach(string line in lines)
                {
                    Console.WriteLine(line);
                }

                ConsoleKey option = Console.ReadKey().Key;
                switch (option)
                {
                    case ConsoleKey.A:
                        break;
                }
            }

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
