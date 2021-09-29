using System;

namespace Infrastructure.Helpers
{
    public class Randomizer
    {
        private readonly Random _random = new Random();

        public char GetRandomLetterFromRange(char firstLetter, char lastLetter)
        {
            firstLetter = Char.ToUpper(firstLetter);
            lastLetter = Char.ToUpper(lastLetter);

            var firstLetterAsciiIndex = Convert.ToInt32((byte)firstLetter);
            var secondLetterAsciiIndex = Convert.ToInt32((byte)lastLetter);

            var randomLetterAsciiIndex = _random.Next(firstLetterAsciiIndex, secondLetterAsciiIndex);

            return Convert.ToChar(randomLetterAsciiIndex);
        }

        public int GetRandomNumberFromRange(int min, int max) =>
            _random.Next(min, max);

        public int GetRandomNumberFromRange(int max) =>
            _random.Next(max);
    }
}
