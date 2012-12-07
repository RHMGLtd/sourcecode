using blackpoolgigs.common.Interfaces;
using Raven.Client;

namespace blackpoolgigs.ravendal.Providers
{
    public class Exporterer : IExport
    {
        protected readonly ISaveBackups backups;
        protected readonly IDocumentSession session;
        protected readonly ISummariseGigs gigs;
        protected readonly ISummariseVenues venues;
        protected readonly ISummariseStolenGear stolen;
        protected readonly IDoOtherThingsWithGigs otherGigs;
        protected readonly ISummariseBands bands;

        public Exporterer(IDocumentSession session, ISaveBackups backups, ISummariseGigs gigs, ISummariseVenues venues, ISummariseStolenGear stolen, IDoOtherThingsWithGigs otherGigs, ISummariseBands bands)
        {
            this.session = session;
            this.bands = bands;
            this.otherGigs = otherGigs;
            this.stolen = stolen;
            this.venues = venues;
            this.gigs = gigs;
            this.backups = backups;
        }

        public void Export()
        {
            var temp = session.Advanced.MaxNumberOfRequestsPerSession;
            session.Advanced.MaxNumberOfRequestsPerSession = 2000;
            backups.Save(gigs.GigDiaries(), 
                gigs.GetCountCollection(), 
                gigs.MonthlyCounts(), 
                venues.VenueDiaries(), 
                gigs.BandDiaries(),
                bands.BandMetadata(),
                venues.Metadata(), 
                stolen.AllCurrentGear(),
                otherGigs.RecentlyUpdated());
            session.Advanced.MaxNumberOfRequestsPerSession = temp;
        }
    }
}