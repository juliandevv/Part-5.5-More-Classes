using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Media;
using System.Runtime.Remoting.Messaging;
using System.Runtime.CompilerServices;

namespace Part_5._5_More_Classes
{
    internal class Program
    {
        enum BetMode
        {
            doubles,
            notDoubles,
            oddSum,
            evenSum
        }
        static void Main(string[] args)
        {
            BetMode betMode = BetMode.doubles;
            string bar = new string('=', Console.WindowWidth);
            string title = " WELCOME TO DICE MANIA! ";
            Console.WriteLine(bar);
            Console.SetCursorPosition((Console.WindowWidth / 2) - (title.Length / 2), Console.CursorTop - 2);
            Console.WriteLine(title);

            bool exit = false;
            while (exit == false)
            {
                Console.ForegroundColor = ConsoleColor.White;
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
                int bet = 0;
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
                        Console.WriteLine("\nYou are betting on doubles. If you win, your bet will be doubled.");
                        betMode = BetMode.doubles;
                        break;
                    case ConsoleKey.C:
                        Console.WriteLine("\nYou are betting on odd sum. If you win, you will get your bet.");
                        betMode = BetMode.notDoubles;
                        break;
                    case ConsoleKey.B:
                        Console.WriteLine("\nYou are betting on not doubles. If you win, you will get half your bet.");
                        betMode = BetMode.oddSum;
                        break;
                    case ConsoleKey.D:
                        Console.WriteLine("\nYou are betting on even sum. If you win, you will get your bet.");
                        betMode = BetMode.evenSum;
                        break;
                    default:
                        Console.WriteLine("\nNot a valid selection1");
                        break;
                }

                Console.WriteLine("Enter the mount you would like to bet:");
                bet = ParseResponse(balance);
                Console.WriteLine($"You are betting: {bet.ToString("C")}");
                animateDice();
                //Console.SetCursorPosition(Console.CursorLeft + 13, Console.CursorTop - 7);
                //drawDice(0);
            }

            int ParseResponse(int balance)
            {
                while (true)
                {
                    string response = Console.ReadLine();
                    if (int.TryParse(response, out int num))
                    {
                        if (num < 0)
                        {
                            num = 0;
                        }
                        else if (num > balance)
                        {
                            num = balance;
                        }
                        return num;
                    }
                    else
                    {
                        Console.WriteLine("Not a valid Input!");
                    }
                }
            }

            void animateDice()
            {
                int[]
                drawDie(0);
            }

            void drawDie(int[] rolls)
            {
                Console.Write("\n ___________    ___________");
                Console.Write("\n|~---------~|  |~---------~|");
                Console.Write("\n|           |  |           |");
                Console.Write($"\n|     {rolls[0]}     |  |     {rolls[1]}     |");
                Console.Write("\n|           |  |           |");
                Console.Write("\n|___________|  |___________|\n");

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
