using System;
using Snooze;

namespace blackpoolgigs.web.Urls
{
    public class YearUrl : Url
    {
        public string Year { get; set; }
        public YearUrl()
        {
            
        }

        public YearUrl(string year)
        {
            Year = year;
        }
        public bool IsValid()
        {
            try
            {
                new DateTime(Int32.Parse(Year), 1, 1);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}