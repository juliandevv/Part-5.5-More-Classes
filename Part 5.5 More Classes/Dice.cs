using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Part_5._5_More_Classes
{
    public class Die
    {
        private Random _generator;
        private int _roll;
        private int _sides;
        private int[] _loadedNums;

        public Die(int sides, int roll)
        {
            _sides = sides;
            if (roll < 1 || roll > sides)
            {
                throw new ArgumentException("Roll must be within die range", "roll");
            }
            _roll = roll;
            _generator = new Random();
        }

        public Die(int sides, int roll, int[] loadedNums)
        {
            _sides = sides;
            if (roll < 1 || roll > sides)
            {
                throw new ArgumentException("Roll must be within die range", "roll");
            }
            _roll = roll;
            _generator = new Random();
            _loadedNums = loadedNums;
        }

        public int Roll { get { return _roll; } }

        public int RollDie()
        {
            _roll = _generator.Next(1, _sides + 1);
            return _roll;
        }

        public int RollWeightedDie()
        {
            double factor = _generator.NextDouble();
            _roll = _generator.Next(1, _sides + 1);
            //Console.WriteLine(_roll);
            foreach (int loadedNum in _loadedNums)
            {
                if (loadedNum == _roll - 1)
                {
                    //Console.WriteLine(Convert.ToDouble(_roll) - factor);
                    _roll = Convert.ToInt32(Math.Round(Convert.ToDouble(_roll) - factor));
                }
                else if (loadedNum == _roll + 1)
                {
                    //Console.WriteLine(Convert.ToDouble(_roll) + factor);
                    _roll = Convert.ToInt32(Math.Round(Convert.ToDouble(_roll) + factor));
                }
            }
            //Console.WriteLine(_roll);
            return _roll;
        }

        public void DrawFace()
        {
            string path = (@"Die Faces.txt");
            string[] lines = File.ReadAllLines(path);

            lines[3] = $" \\   {_roll}   /";

            foreach(string line in lines)
            {
                Console.WriteLine(line);
            }
        }
    }
}
