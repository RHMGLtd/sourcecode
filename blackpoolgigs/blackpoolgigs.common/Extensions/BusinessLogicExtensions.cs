using System;
using System.Collections.Generic;
using System.Linq;
using blackpoolgigs.common.Models;

namespace blackpoolgigs.common.Extensions
{
    public static class BusinessLogicExtensions
    {
        public static bool YearTooFarInFuture(this int year)
        {
            return year > DateTime.Now.AddYears(2).Year;
        }

        public static bool YearTooFarInFuture(this DateTime input)
        {
            return input.Year.YearTooFarInFuture();
        }

        public static bool YearIsArchived(this int year)
        {
            return year < DateTime.Now.AddYears(-1).Year;
        }
        public static bool YearIsArchived(this DateTime input)
        {
            return input.Year.YearIsArchived();
        }

        public static List<VenueDiary> SortGigsInVenues(this List<VenueDiary> input)
        {
            foreach (var diary in input)
            {
                diary.Gigs = diary.Gigs.OrderBy(x => x.Date).ThenBy(x => x.StartTime).ToList();
            }
            return input;
        }
    }
}