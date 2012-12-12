using blackpoolgigs.common.Interfaces;
using coolbunny.common;
using coolbunny.common.Extensions;
using coolbunny.common.Helpers;
using rhmg.AdministrateBlackpoolGigs.web.Models;
using rhmg.AdministrateBlackpoolGigs.web.Models.ViewModels;
using rhmg.AdministrateBlackpoolGigs.web.Urls;
using Snooze;

namespace rhmg.AdministrateBlackpoolGigs.web.Controllers
{
    public class ManageGigsController : ResourceController
    {
        protected readonly IGiveYouGigs gigs;
        protected readonly IStoreGigs gigStorer;
        protected readonly IHandleRepeatingGigs repeatingGigs;

        public ManageGigsController(IGiveYouGigs gigs, IStoreGigs gigStorer, IHandleRepeatingGigs repeatingGigs)
        {
            this.gigs = gigs;
            this.repeatingGigs = repeatingGigs;
            this.gigStorer = gigStorer;
        }

        public ResourceResult Get(AddUrl url)
        {
            return OK(new ManageViewModel()).AsHtml();
        }
        public ResourceResult Get(EditUrl url)
        {
            return OK(new ManageViewModel(gigs.Get(url.RecordId.EnsureStartsWith("gig/")), url.RecordId.EnsureStartsWith("gig/"))).AsHtml();
        }
        public ResourceResult Get(ListUrl url)
        {
            return OK(new ListViewModel
            {
                Gigs = gigs.Get(new PagingParams(url)),
                Params = new PagingParams(url)
            }).AsHtml();
        }
        public ResourceResult Get(RepeatingGigsUrl url)
        {
            return OK(new RepeatingGigsViewModel
                          {
                              Gigs = repeatingGigs.GetRepeating(new PagingParams(url)),
                              Params = new PagingParams(url)
                          }).AsHtml();
        }

        public ResourceResult Get(RepeatingGigUrl url)
        {
            return
                OK(new RepeatingGigViewModel(repeatingGigs.GetRepeating(url.Id.EnsureStartsWith("repeatinggig/")))).AsHtml();
        }
        public ResourceResult Get(AddRepeatingGigUrl url)
        {
            return OK(new RepeatingGigViewModel()).AsHtml();
        }

        public ResourceResult Delete(EditUrl url)
        {
            gigStorer.Delete(url.RecordId.EnsureStartsWith("gig/"));
            return OK(true).AsHtml();
        }
        public ResourceResult Delete(RepeatingGigUrl url)
        {
            repeatingGigs.Delete(url.Id.EnsureStartsWith("repeatinggig/"));
            return OK(true).AsHtml();
        }

        public ResourceResult Put(CheckDateUrl url, [Json]DateCheckModel input)
        {
            return OK(new DateCheckViewModel
                          {
                              Diary = gigs.GetEntry(input.checkDate).AddOrder(x => x.Venue)
                          }).AsJson();
        }
        public ResourceResult Post(EditUrl url, ManageViewModel input)
        {
            var gig = gigStorer.Save(input.AsGig());
            input.Id = gig.Id;
            return OK(input).AsHtml();
        }
        public ResourceResult Post(RepeatingGigUrl url, RepeatingGigViewModel input)
        {
            
            repeatingGigs.SaveRepeating(input.AsRepeatingGig());
            return OK(input).AsHtml();
        }

        public ResourceResult Post(AddUrl url, ManageViewModel input)
        {
            gigStorer.Save(input.AsGig());
            return SeeOther(new ListUrl()).AsHtml();
        }

        public ResourceResult Post(AddRepeatingGigUrl url, RepeatingGigViewModel input)
        {
            var repeatingGig = repeatingGigs.SaveRepeating(input.AsRepeatingGig());
            input.Id = repeatingGig.Id;
            return SeeOther(new HomeUrl()).AsHtml();
        }
    }
}