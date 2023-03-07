using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part_5._5_More_Classes
{
    public class Die
    {
        private Random _generator;
        private int _roll;
        private int _sides;

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

        public int Roll { get { return _roll; } }

        public int RollDie()
        {
            _roll = _generator.Next(1, _sides + 1);
            return _roll;
        }

        public void DrawFace()
        {
            string path = (@"DieFace.txt");
        }
    }
}
