using System;
using System.Globalization;
using coolbunny.common.Extensions;
using Snooze;

namespace blackpoolgigs.web.Urls
{
    public class DayUrl : Url
    {
        public DayUrl()
        {

        }

        public DayUrl(DateTime date)
        {
            Month = new DateTimeFormatInfo().GetMonthName(date.Month);
            Year = date.Year.ToString();
            Date = date.Day.ToString();
        }
        public string Month { get; set; }
        public string Year { get; set; }
        public string Date { get; set; }

        public DateTime AsDate()
        {
            return new DateTime(Int32.Parse(Year), Month.AsMonth(), Int32.Parse(Date));
        }
        public bool IsValid()
        {
            try
            {
                AsDate();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}