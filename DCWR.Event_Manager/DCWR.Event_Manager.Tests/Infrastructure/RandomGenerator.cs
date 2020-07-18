using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCWR.Event_Manager.Tests.Infrastructure
{
    public static class RandomGenerator
    {
        private static readonly Random rnd = new Random(DateTime.UtcNow.Second);
        private static readonly string letters = "qwertyuiopasdfghjklzxcvbnm";

        public static int GetInt(int minValue = 0, int maxValue = 1000)
        {
            return rnd.Next(minValue, maxValue);
        }

        public static string GetWord(int length = 6)
        {
            return new string(Enumerable.Range(0, length).Select(x => letters[rnd.Next(letters.Length)]).ToArray());
        }

        public static string GetWords(string separator = " ", int wordCount = 10, int minLength = 3, int maxLength = 8)
        {
            return string.Join(separator, Enumerable.Range(0, wordCount).Select(x => GetWord(rnd.Next(minLength, maxLength + 1))));
        }
    }
}
