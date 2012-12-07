using System;
using System.Linq;
using blackpoolgigs.common.Interfaces;
using blackpoolgigs.common.Models;
using coolbunny.common.Extensions;
using coolbunny.common.Interfaces;
using Raven.Client;

namespace blackpoolgigs.ravendal.Providers
{
    public class TestDataProvider : ICreateTestData
    {
        protected readonly IStoreGigs gigs;
        protected readonly IDocumentSession session;

        private string[] venues
        {
            get
            {
                return new[]
                           {
                               "The Blue Room",
                               "The Royal Oak",
                               "Beat",
                               "Gillespies",
                               "Fridge"
                           };
            }
        }
        private string[] bandNames
        {
            get
            {
                return new[]
                           {
                               "The Silvertones",
                               "Peter Simple",
                               "The Foo Fighters",
                               "Nirvana",
                               "Kings X",
                               "Bar Room Boogie",
                               "Imelda May",
                               "Rammstein",
                               "Alabama 3",
                               "The Beatles",
                               "Pink Floyd",
                               "ZoomZoomZoom",
                               "Bon Jovi",
                               "Red Hot Chilli Peppers"
                           };
            }
        }
        private string[] prices
        {
            get
            {
                return new[]
                           {
                               "Free",
                               "£2.00",
                               "£5.00",
                               "£1.00"
                           };
            }
        }

        public TestDataProvider(IStoreGigs gigs, IDocumentSession session)
        {
            this.gigs = gigs;
            this.session = session;
        }

        public void Run(string applicationMessage)
        {
            if (session.Advanced.LuceneQuery<Gig>().WaitForNonStaleResults().Any())
                return;
            for (var i = 0; i < 200; i++)
            {
                var gig = new Gig
                              {
                                  Venue = venues[venues.Count().RandomFromZeroTo()],
                                  BandNames = new[]
                                                  {
                                                      new BandName
                                                          {
                                                              Value = bandNames[bandNames.Count().RandomFromZeroTo()]
                                                          },
                                                      new BandName
                                                          {
                                                              Value = bandNames[bandNames.Count().RandomFromZeroTo()]
                                                          },
                                                      new BandName
                                                          {
                                                              Value = bandNames[bandNames.Count().RandomFromZeroTo()]
                                                          }
                                                    },
                                  StartTime = "18:00",
                                  Price = prices[prices.Count().RandomFromZeroTo()],
                                  Date = DateTime.Now.AddDays(i).Date,
                                  Source = "auto generated test data",
                                  MoreInfo = "This is a test record only. Please make sure none of these appear in the real deployed code " + applicationMessage,
                              };
                session.Store(gig);
            }
            session.SaveChanges();
        }
    }
}