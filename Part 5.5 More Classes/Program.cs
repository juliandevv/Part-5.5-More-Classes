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
using System.Reflection;
using System.ComponentModel;

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
            animateString(title, 30);
            int balance = 100;
            List<Die> dice = new List<Die>();
            dice.Add(new Die(6, 1));
            dice.Add(new Die(6, 1));

            dice[0].RollDie();
            dice[1].RollDie();
            Console.WriteLine($"One {dice[0].Roll} Two {dice[1].Roll}");


            //main command prompt loop
            bool exit = false;
            while (exit == false)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(">>>");
                if(Parse(Console.ReadLine(), ref balance, ref dice) == true)
                {
                    exit = true;
                }
            }
        }
        static bool Parse(string commandString, ref int balance, ref List<Die> dice)
        {
            //parse input to commands
            List<string> commandParts = commandString.Split(' ').ToList();
            string command = commandParts.First();
            List<string> arguments = commandParts.Skip(1).ToList();

            

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
                    play(dice, ref balance);
                    break;
                case "dice":
                    config(commandParts, ref dice);
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

        static void config(List<String> commandParts, ref List<Die> dice)
        {
            //Console.WriteLine("configuring dice...");
            
            if (commandParts[1] == "configure")
            {
                if (int.TryParse(commandParts[2], out int sides))
                {
                    dice.Clear();
                    dice.Add(new Die(sides, 1));
                    dice.Add(new Die(sides, 1));
                    Console.WriteLine($"Succesfully created {sides} sided dice");
                }
                else
                {
                    Console.WriteLine("Invalid command. Usage: command [options] [parameters]");
                }
            }
            else if (commandParts[1] == "reset")
            {
                dice.Clear();
                dice.Add(new Die(6, 1));
                dice.Add(new Die(6, 1));
                Console.WriteLine("Succesfully reset dice");
            }
        }

        static void play(List<Die> dice, ref int balance)
        {
            //initialize gameplay vars
            Random generator = new Random();
            Bet betMode = Bet.doubles;
            List<Bet> diceResults = new List<Bet>();
            Console.ForegroundColor = ConsoleColor.Green;
            int bet = 0;
            int payout;
            int diceSum;
            bool win = false;

            //prompt betting choices
            animateString($"You currently have: {balance.ToString("C")}");
            animateString("Choose a betting option fom the following:");
            Console.WriteLine();
            string[] lines = File.ReadAllLines(@"bets.txt");

            foreach(string line in lines)
            {
                Console.WriteLine(line);
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
                    betMode = Bet.oddSum;
                    break;
                case ConsoleKey.B:
                    animateString("\nYou are betting on not doubles. If you win, you will get half your bet.");
                    betMode = Bet.notDoubles;
                    break;
                case ConsoleKey.D:
                    animateString("\nYou are betting on even sum. If you win, you will get your bet.");
                    betMode = Bet.evenSum;
                    break;
                default:
                    animateString("\nDefaulted bet to doubles. If you win, your bet will be doubled.");
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
                    animateDice(dice, generator);
                    break;
                }
            }

            //win conditions
            diceSum = (dice[0].Roll + dice[1].Roll);

            if (dice[0].Roll == dice[1].Roll)
            {
                diceResults.Add(Bet.evenSum);
                diceResults.Add(Bet.doubles);
            }
            else
            {
                if (diceSum % 2 == 1)
                {
                    diceResults.Add(Bet.oddSum);
                }
                else
                {
                    diceResults.Add(Bet.evenSum);
                }
                diceResults.Add(Bet.notDoubles);
            }
            
            foreach (Bet result in diceResults)
            {
                if(result == betMode)
                {
                    win = true;
                }
            }
            diceResults.Clear();

            if (win)
            {
                if (betMode == Bet.doubles)
                {
                    payout = bet * 2;
                }
                else if (betMode == Bet.notDoubles)
                {
                    payout = bet / 2;
                }
                else
                {
                    payout = bet;
                }
                animateString($"You Win {payout.ToString("C")}!!!");
            }
            else
            {
                payout = (-1) * bet;
                animateString($"You Lose {(payout * -1).ToString("C")}!!!");
            }

            balance += payout;
            animateString($"Your balance is now {balance.ToString("C")}");

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

        static void animateDice(List<Die> dice, Random generator)
        {
            //Console.WriteLine(dice[0].Roll);
            //Console.WriteLine(dice[1].Roll);
            //Console.WriteLine(dice.Count);

            List<ConsoleColor> colors = new List<ConsoleColor>() { ConsoleColor.Red, ConsoleColor.Yellow, ConsoleColor.Blue, ConsoleColor.Cyan, ConsoleColor.Magenta};
            for (int i = 0; i < 20; i++)
            {
                Console.ForegroundColor = colors[generator.Next(0, 5)];
                for (int j = 0; j < dice .Count; j++) 
                {
                    dice[j].RollDie();
                    dice[j].DrawFace(j * 13, j * (-7));
                }
                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 7);
                Thread.Sleep(30);
            }
            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop + 8);
            //Console.WriteLine(dice[0].Roll);
            //Console.WriteLine(dice[1].Roll);
        }

        static void animateString(string text)
        {
            animateString(text, 10);
        }

        static void animateString(string text, int speed)
        {
            foreach (char c in text)
            {
                Console.Write(c.ToString());
                Thread.Sleep(speed);
            }
            Console.WriteLine();
        }
    }
}
