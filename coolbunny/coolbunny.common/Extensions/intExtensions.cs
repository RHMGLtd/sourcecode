using System;
using System.Threading;

namespace coolbunny.common.Extensions
{
    public static class intExtensions
    {
        public static bool Between(this int input, int lower, int upper)
        {
            return input <= upper && input >= lower;
        }
        public static int RandomFromZeroTo(this int to)
        {
            return 0.RandomFromTo(to);
        }
        public static int RandomFromTo(this int from, int to)
        {
            Thread.Sleep(10);
            return new Random().Next(from, to); 
        }
        public static string Dateify(this int input)
        {
            if (!input.Between(1, 31))
                throw new ArgumentOutOfRangeException("input", "You cannot have a date before the 1st or after the 31st foooool");
            if (input == 1)
                return "1st";
            if (input == 2)
                return "2nd";
            if (input == 3)
                return "3rd";
            if (input == 21)
                return "21st";
            if (input == 22)
                return "22nd";
            if (input == 23)
                return "23rd";
            if (input == 31)
                return "31st";
            return string.Format("{0}th", input);
        }
    }
}