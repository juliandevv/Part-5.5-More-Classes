using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Media;
using System.Runtime.Remoting.Messaging;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Part_5._5_More_Classes
{
    internal class Program
    {
        enum Bet
        {
            doubles,
            notDoubles,
            oddSum,
            evenSum
        }
        static void Main(string[] args)
        {
            //Write title
            string bar = new string('=', Console.WindowWidth);
            string title = " WELCOME TO DICE MANIA! ";
            Console.WriteLine(bar);
            Console.SetCursorPosition((Console.WindowWidth / 2) - (title.Length / 2), Console.CursorTop - 2);
            animateString(title);

            //main command prompt loop
            bool exit = false;
            while (exit == false)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(">>>");
                if(Parse(Console.ReadLine()) == true)
                {
                    exit = true;
                }
            }
        }
        static bool Parse(string commandString)
        {
            //parse input to commands
            List<string> commandParts = commandString.Split(' ').ToList();
            string command = commandParts.First();
            List<string> arguments = commandParts.Skip(1).ToList();
            Die die = new Die(6, 1);

            //evaluate command
            switch (command){
                //display help screen
                case "help":
                    string[] lines = File.ReadAllLines(@"help.txt");
                    foreach(string line in lines)
                    {
                        Console.WriteLine(line);
                    }
                    break;
                //main game 
                case "play":
                    play(die);
                    break;
                //exit condition
                case "exit":
                    return true;
                default:
                    Console.WriteLine("Not a valid command. Use 'help' for list of valid commands");
                    break;
            }
            return false;
        }

        static void play(Die die)
        {
            //initialize gameplay vars
            Random genertaor = new Random();
            Bet betMode = Bet.doubles;
            Bet betResult = Bet.doubles;
            Console.ForegroundColor = ConsoleColor.Green;
            int balance = 100;
            int bet = 0;
            int roll1 = 0, roll2 = 0;

            //prompt betting choices
            animateString($"You currently have: {balance.ToString("C")}");
            animateString("Choose a betting option fom the following:");
            Console.WriteLine();
            string[] lines = File.ReadAllLines(@"bets.txt");

            foreach(string line in lines)
            {
                animateString(line);
            }

            ConsoleKey option = Console.ReadKey().Key;
            Console.WriteLine();
            switch (option)
            {
                case ConsoleKey.A:
                    animateString("\nYou are betting on doubles. If you win, your bet will be doubled.");
                    betMode = Bet.doubles;
                    break;
                case ConsoleKey.C:
                    animateString("\nYou are betting on odd sum. If you win, you will get your bet.");
                    betMode = Bet.notDoubles;
                    break;
                case ConsoleKey.B:
                    animateString("\nYou are betting on not doubles. If you win, you will get half your bet.");
                    betMode = Bet.oddSum;
                    break;
                case ConsoleKey.D:
                    animateString("\nYou are betting on even sum. If you win, you will get your bet.");
                    betMode = Bet.evenSum;
                    break;
                default:
                    animateString("\nNot a valid selection1");
                    break;
            }

            animateString("Enter the mount you would like to bet:");
            bet = ParseResponse(balance);
            animateString($"\nYou are betting: {bet.ToString("C")}");
            animateString("Press SPACE to roll dice!");

            //animate dice when space key pressed
            while (true)
            {
                if (Console.ReadKey().Key == ConsoleKey.Spacebar)
                {
                    animateDice(die, genertaor, roll1, roll2);
                    break;
                }
            }

            //final roll
            roll1 = die.RollDie();
            roll2 = die.RollDie();
            if (roll1 == roll2)
            {
                betResult = Bet.doubles;
            }
        }

        static int ParseResponse(int balance)
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

        static void animateDice(Die die, Random generator, int roll1, int roll2)
        {
            List<ConsoleColor> colors = new List<ConsoleColor>() { ConsoleColor.Red, ConsoleColor.Yellow, ConsoleColor.Blue, ConsoleColor.Cyan, ConsoleColor.Magenta};
            for (int i = 0; i < 20; i++)
            {
                roll1 = die.RollDie();
                roll2 = die.RollDie();
                Console.ForegroundColor = colors[generator.Next(0, 5)];
                drawDice(roll1, roll2);
                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 7);
                Thread.Sleep(100);
            }
        }

        static void drawDice(int roll1, int roll2)
        {
            Console.Write("\n ___________    ___________");
            Console.Write("\n|~---------~|  |~---------~|");
            Console.Write("\n|           |  |           |");
            Console.Write($"\n|     {roll1}     |  |     {roll2}     |");
            Console.Write("\n|           |  |           |");
            Console.Write("\n|___________|  |___________|\n");

        }

        static void animateString(string text)
        {
            foreach (char c in text)
            {
                Console.Write(c.ToString());
                Thread.Sleep(10);
            }
            Console.WriteLine();
        }
    }
}
