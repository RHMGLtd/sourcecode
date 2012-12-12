using blackpoolgigs.common.Interfaces;
using rhmg.AdministrateBlackpoolGigs.web.Models.ViewModels;
using rhmg.AdministrateBlackpoolGigs.web.Urls;
using Snooze;

namespace rhmg.AdministrateBlackpoolGigs.web.Controllers
{
    public class ExportController : ResourceController
    {
        protected readonly IExport export;

        public ExportController(IExport export)
        {
            this.export = export;
        }

        public ResourceResult Get(ExportUrl url)
        {
            export.Export();
            return OK(new ExportViewModel()).AsHtml();
        }
    }
}