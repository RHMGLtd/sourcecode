using System;
using System.Collections.Generic;
using System.Linq;
using Raven.Client;
using Raven.Client.Linq;
using Raven.Imports.Newtonsoft.Json;

namespace rhmg.StudioDiary
{
    public class BackFill : Entity
    {
        public string MainContactId { get; set; }
        [JsonIgnore]
        public Contact MainContact { get; set; }
        public DateTime Date { get; set; }
        public bool Upgraded { get; set; }
        

        public BackFill Save(IDocumentSession session)
        {
            session.Store(this);
            session.SaveChanges();
            return this;
        }

        public static BackFill Get(string id, IDocumentSession session)
        {
            var backfill = session.Include<BackFill>(x => x.MainContactId).Load(id);
            backfill.MainContact = session.Load<Contact>(backfill.MainContactId);
            return backfill;
        }
        public static List<BackFill> Get(DateTime effectiveDate, IDocumentSession session)
        {
            var backfills =session.Query<BackFill>().Where(x => x.Date == effectiveDate).ToList();
            if (backfills.Any())
            {
                var contactIds = backfills.Select(x => x.MainContactId);
                var contacts = session.Load<Contact>(contactIds);
                foreach (var backFill in backfills)
                {
                    var contact = contacts.FirstOrDefault(x => x.Id == backFill.MainContactId);
                    if (contact != null)
                        backFill.MainContact = contact;
                }
            }
            return backfills;
        }

        public void Upgrade()
        {
            Upgraded = true;
        }
    }
}