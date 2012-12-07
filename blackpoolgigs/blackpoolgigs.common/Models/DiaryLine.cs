using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace blackpoolgigs.common.Models
{
    public class DiaryLine
    {
        public bool HasGigs()
        {
            foreach (var diaryEntry in Entries)
            {
                if (diaryEntry.HasGigs())
                    return true;
            }
            return false;
        }
        public DiaryLine()
        {
            Entries = new List<DiaryEntry>();
        }
        public List<DiaryEntry> Entries { get; set; }
        #region by day accessors
        [JsonIgnore]
        public DiaryEntry Monday
        {
            get { return Entries.Where(e => e.Date.DayOfWeek == DayOfWeek.Monday).FirstOrDefault(); }
        }
        [JsonIgnore]
        public DiaryEntry Tuesday
        {
            get { return Entries.Where(e => e.Date.DayOfWeek == DayOfWeek.Tuesday).FirstOrDefault(); }
        }
        [JsonIgnore]
        public DiaryEntry Wednesday
        {
            get { return Entries.Where(e => e.Date.DayOfWeek == DayOfWeek.Wednesday).FirstOrDefault(); }
        }
        [JsonIgnore]
        public DiaryEntry Thursday
        {
            get { return Entries.Where(e => e.Date.DayOfWeek == DayOfWeek.Thursday).FirstOrDefault(); }
        }
        [JsonIgnore]
        public DiaryEntry Friday
        {
            get { return Entries.Where(e => e.Date.DayOfWeek == DayOfWeek.Friday).FirstOrDefault(); }
        }
        [JsonIgnore]
        public DiaryEntry Saturday
        {
            get { return Entries.Where(e => e.Date.DayOfWeek == DayOfWeek.Saturday).FirstOrDefault(); }
        }
        [JsonIgnore]
        public DiaryEntry Sunday
        {
            get { return Entries.Where(e => e.Date.DayOfWeek == DayOfWeek.Sunday).FirstOrDefault(); }
        }
        #endregion
        public DiaryEntry ForDate(DateTime check)
        {
            return Entries.FirstOrDefault(diaryEntry => diaryEntry.Date.Day == check.Day && diaryEntry.Date.Month == check.Month && diaryEntry.Date.Year == check.Year);
        }
    }
}