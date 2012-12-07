using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using blackpoolgigs.common.Interfaces;
using blackpoolgigs.common.Models;
using coolbunny.common.Extensions;
using coolbunny.common.Helpers;
using Raven.Client;

namespace blackpoolgigs.ravendal.Providers
{
    public class StolenGearHandler : IProcessStolenGear, IDealInStolenGear, ISummariseStolenGear
    {
        protected readonly IDocumentSession session;
        protected readonly IDocumentStore documentStore;
        protected readonly ISaveImages imagerer;

        public StolenGearHandler(IDocumentSession session, IDocumentStore documentStore, ISaveImages imagerer)
        {
            this.session = session;
            this.imagerer = imagerer;
            this.documentStore = documentStore;
        }

        public StolenGear Save(StolenGear input, params HttpPostedFileBase[] images)
        {
            imagerer.Save(images, input.ImageNames, input.Headline.Tidy());
            session.Store(input);
            session.SaveChanges();
            return input;
        }

        public StolenGear Delete(string id)
        {
            var bob = session.Load<StolenGear>(id);
            if (bob != null)
                session.Delete(bob);
            session.SaveChanges();
            return bob;
        }

        public PageOfResults<StolenGear> Get(PagingParams paging)
        {
            var result = session.Advanced.LuceneQuery<StolenGear>()
                .Skip(((paging.PageNumber - 1) * paging.PageSize))
                .Take(paging.PageSize);
            return new PageOfResults<StolenGear>
            {
                CurrentPageNumber = paging.PageNumber,
                NumberOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(result.QueryResult.TotalResults) / Convert.ToDouble(paging.PageSize))),
                TotalNumberOfRecords = result.QueryResult.TotalResults,
                Results = result.ToList()
            };
        }

        public StolenGear Get(string id)
        {
            return session.Load<StolenGear>(id);
        }
        public IEnumerable<StolenGear> AllCurrentGear()
        {
            return session.Advanced.LuceneQuery<StolenGear>()
                .WhereEquals("Archived", false).
                AndAlso().WhereEquals("Recovered", false).WaitForNonStaleResults().Take(2000).ToList();
        }
    }
}