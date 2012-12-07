using blackpoolgigs.common.Interfaces;
using blackpoolgigs.common.Models;
using blackpoolgigs.web.Models.ViewModels;
using blackpoolgigs.web.Services.Interfaces;
using blackpoolgigs.web.Urls;
using coolbunny.common.Extensions;
using coolbunny.common.Helpers;
using Snooze;

namespace blackpoolgigs.web.Controllers
{
    public class BandController : ResourceController
    {
        protected readonly IWillProvideBands bands;
        protected readonly ISummariseBands bandSummary;
        protected readonly IGiveYouGigs gigs;
        protected readonly IEmail email;
        protected readonly ISayThanks thanks;

        public BandController(IWillProvideBands bands, ISummariseBands bandSummary, IGiveYouGigs gigs, IEmail email, ISayThanks thanks)
        {
            this.bands = bands;
            this.thanks = thanks;
            this.email = email;
            this.gigs = gigs;
            this.bandSummary = bandSummary;
        }

        public ResourceResult Get(BandsUrl url)
        {
            return OK(new BandsViewModel
                          {
                              Bands = bands.GetBands(new PagingParams(url), url.AlphaPick),
                              Params = new PagingParams(url),
                              AlphaPick = url.AlphaPick.ToUpper(),
                              TotalNumberOfBands = bandSummary.NumberOfBands()
                          }).AsHtml();
        }

        public ResourceResult Get(BandUrl url)
        {
            return OK(new BandViewModel
                          {
                              Metadata = bands.GetMetadata(new BandName { Value = url.BandName.Tidy() }),
                              Diary = bands.GetDiary(new BandName { Value = url.BandName.Tidy() }),
                              AlphaPick = url.BandName.Substring(0, 1)
                          }).AsHtml();
        }

        public ResourceResult Get(ClaimBandUrl url)
        {
            return OK(new ClaimBandViewModel { BandName = url.BandName }).AsHtml();
        }
        public ResourceResult Get(ClaimThanksUrl url)
        {
            var result = thanks.Gimme();
            return OK(new ClaimThanksViewModel
            {
                BandName = url.BandName,
                ImageAlt = result.Alt,
                ImagePath = result.Path,
                ImageTitle = result.Title
            }).AsHtml();
        }

        public ResourceResult Post(ClaimBandUrl url, ClaimBandViewModel input)
        {
            email.Send(input, Request.ServerVariables["remote_addr"]);
            return OK(new ClaimThanksUrl
                          {
                              BandName = url.BandName
                          });
        }
    }
}