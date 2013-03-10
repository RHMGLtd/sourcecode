using System.Collections.Generic;
using System.Linq;

namespace rhmg.StudioDiary.Extensions
{
    public static class ListExtensions
    {
        public static string ToStringSeparatedWithAnd(this List<string> input)
        {
            if (input.Any())
            {
                if (input.Count() == 1)
                    return input.First();
                var first = input.Count() == 2 ? input.First() : string.Join(", ", input.Take(input.Count() - 1));
                return string.Format("{0} and {1}", first, input.Last());
            }
            return string.Empty;
        }
    }
}