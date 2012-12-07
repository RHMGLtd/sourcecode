using System.Linq;
using blackpoolgigs.common.Interfaces;
using blackpoolgigs.common.Models;
using blackpoolgigs.ravendal.Indexes;
using coolbunny.common.Extensions;
using Newtonsoft.Json;
using Raven.Client;

namespace blackpoolgigs.ravendal.Providers
{
    public class SuggestMe : ISuggestThings
    {
        protected readonly IDocumentSession session;
        protected readonly IWillProvideBands bands;

        public SuggestMe(IDocumentSession session, IWillProvideBands bands)
        {
            this.session = session;
            this.bands = bands;
        }

        public string[] Venues(string hint)
        {
            var hints = hint.Split(' ');
            return session.Advanced.LuceneQuery<Venues.ReduceResult>("Venues")
                .WhereContains("Venue", hints.Select(s => "*" + s.ToLower() + "*").ToArray())
                .Take(20)
                .Select(x => x.Venue.SafeTrim())
                .ToArray();
        }

        public string[] Bands(string hint)
        {
            var options = bands.BandNames();

            return (from option in options
                    where option.Value.ToLower().Contains(hint.ToLower())
                    select option.Value.SafeTrim()).OrderBy(x => x).Distinct().Take(20).ToArray();
        }
    }
}