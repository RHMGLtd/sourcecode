using System;
using System.Collections.Generic;
using System.Linq;
using Raven.Client;

namespace rhmg.StudioDiary
{
    public class Rate : Entity
    {
        public TimeSpan Per { get; set; }
        public double PoundsAmount { get; set; }
        public string Description { get; set; }

        public double For(TimeSpan length)
        {
            var factor = length.TotalHours / Per.TotalHours;
            return PoundsAmount*factor;
        }

        public Rate Save(IDocumentSession session)
        {
            session.Store(this);
            session.SaveChanges();
            return this;
        }

        public static List<Rate> All(IDocumentSession session)
        {
            return session.Query<Rate>().ToList();
        }
    }
}