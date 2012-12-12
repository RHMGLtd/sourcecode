using System;
using blackpoolgigs.common.Interfaces;
using rhmg.AdministrateBlackpoolGigs.web.Models.ViewModels;
using rhmg.AdministrateBlackpoolGigs.web.Urls;
using Snooze;

namespace rhmg.AdministrateBlackpoolGigs.web.Controllers
{
    public class HomeController : ResourceController
    {
        protected readonly IJustDoCounts counts;

        public HomeController(IJustDoCounts counts)
        {
            this.counts = counts;
        }

        public ResourceResult Get(HomeUrl url)
        {
            return OK(new HomeViewModel
                          {
                              Gigs = counts.Gigs,
                              RepeatingGigs = counts.RepeatingGigs,
                              StolenGear = counts.StolenGear
                          }).AsHtml();
        }
    }
}