using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace coolbunny.common.Extensions
{
    public static class stringExtensions
    {
        public static string Tidy(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "";
            return input.Replace("_", " ").Replace("-", " ").Replace(",", "").Replace("?", "").Replace("/", "").Replace("&", "and");
        }

        public static int AsInt(this string input)
        {
            switch (input)
            {
                case "one":
                    return 1;
                case "two":
                    return 2;
                default:
                    throw new ArgumentOutOfRangeException(input);
            }
        }

        public static string EnsureHttp(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;
            input = input.Trim();
            if (input.StartsWith("http://"))
                return input;
            return "http://" + input;
        }

        public static int AsMonth(this string input)
        {
            switch (input.ToLower())
            {
                case "january":
                case "jan":
                    return 1;
                case "february":
                case "feb":
                    return 2;
                case "march":
                case "mar":
                    return 3;
                case "april":
                case "apr":
                    return 4;
                case "may":
                    return 5;
                case "june":
                case "jun":
                    return 6;
                case "july":
                case "jul":
                    return 7;
                case "august":
                case "aug":
                    return 8;
                case "september":
                case "sep":
                    return 9;
                case "october":
                case "oct":
                    return 10;
                case "november":
                case "nov":
                    return 11;
                case "december":
                case "dec":
                    return 12;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static string makeRHMGalink(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;
            //TODO: update this to be regex match
            if (input.Contains("rhmg"))
                input = input.Replace("rhmg", "<a href='http://www.rhmg.co.uk' target='_rhmg'>rhmg</a>");
            return input;
        }
        public static string Linkify(this string data)
        {
            var regx = new Regex("http(s)?://([\\w+?\\.\\w+])+([a-zA-Z0-9\\~\\!\\@\\#\\$\\%\\^\\&amp;\\*\\(\\)_\\-\\=\\+\\\\\\/\\?\\.\\:\\;\\'\\,]*)?", RegexOptions.IgnoreCase);

            var matches = regx.Matches(data);
            return matches.Cast<Match>().Aggregate(data, (current, match) => current.Replace(match.Value, "<a href='" + match.Value + "' target='_blank'>" + match.Value + "</a>")).Replace(Environment.NewLine, "<br />"); 
        }

        public static string SafeTrim(this string input)
        {
            return string.IsNullOrEmpty(input) ? input : input.Trim();
        }

        public static string EnsureStartsWith(this string input, string startWith)
        {
            if (input.ToLower().StartsWith(startWith.ToLower()))
                return input;
            return startWith + input;
        }
        public static string RemoveStartsWith(this string input, string startWith)
        {
            if (input.ToLower().StartsWith(startWith.ToLower()))
                return input.Replace(startWith, "");
            return input;
        }
    }
}