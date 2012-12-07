using System.Collections.Generic;
using rhmgOnline.Models;
using rhmgOnline.Models.ViewModels;
using rhmgOnline.Services.Interfaces;
using rhmgOnline.Urls;
using Snooze;

namespace rhmgOnline.Controllers
{
    public class HomeController : ResourceController
    {
        protected readonly IDoBranding brands;

        public HomeController(IDoBranding brands)
        {
            this.brands = brands;
        }

        public ResourceResult Get(HomeUrl url)
        {
            return OK(new HomeViewModel
                          {
                              Brandings = brands.GetThem()
                          });
        }
    }
}