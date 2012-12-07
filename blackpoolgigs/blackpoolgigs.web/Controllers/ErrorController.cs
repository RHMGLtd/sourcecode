using blackpoolgigs.web.Models;
using blackpoolgigs.web.Models.ViewModels;
using blackpoolgigs.web.Services.Interfaces;
using blackpoolgigs.web.Urls;
using Snooze;

namespace blackpoolgigs.web.Controllers
{
    public class ErrorController : ResourceController
    {
        protected readonly IProvideBlobs blobs;

        public ErrorController(IProvideBlobs blobs)
        {
            this.blobs = blobs;
        }
        public ResourceResult Get(ErrorUrl url)
        {
            return NotFound(new FourOhFourViewModel(FourOhFourReason.InvalidRouteRequested, blobs.GiveMeOne())).AsHtml();
        }
    }
}