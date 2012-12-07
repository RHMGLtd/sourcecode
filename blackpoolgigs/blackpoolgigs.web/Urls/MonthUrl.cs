using System;
using System.Globalization;
using coolbunny.common.Extensions;
using Snooze;

namespace blackpoolgigs.web.Urls
{
    public class MonthUrl : Url
    {
        public MonthUrl()
        {
            
        }

        public MonthUrl(DateTime date)
        {
            Month = new DateTimeFormatInfo().GetMonthName(date.Month);
            Year = date.Year.ToString();
        }
        public string Month { get; set; }
        public string Year { get; set; }
        public DateTime AsDate()
        {
            return new DateTime(Int32.Parse(Year), Month.AsMonth(), 1);
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