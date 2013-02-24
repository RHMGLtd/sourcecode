using System;
using System.Collections.Generic;
using System.Linq;
using Raven.Client;
using Raven.Client.Linq;

namespace rhmg.StudioDiary
{
    public class BackFill : Entity
    {
        public Contact Contact { get; set; }

        public DateTime Date { get; set; }

        public bool Upgraded { get; set; }

        public static BackFill Create(Contact contact, DateTime date)
        {
            return new BackFill
                       {
                           Contact = contact,
                           Date = date
                       };
        }

        public BackFill Save(IDocumentSession session)
        {
            session.Store(this);
            session.SaveChanges();
            return this;
        }

        public static BackFill Get(string id, IDocumentSession session)
        {
            return session.Load<BackFill>(id);
        }
        public static List<BackFill> Get(DateTime effectiveDate, IDocumentSession session)
        {
            return session.Query<BackFill>().Where(x => x.Date == effectiveDate).ToList();
        }

        public void Upgrade()
        {
            Upgraded = true;
        }
    }
}