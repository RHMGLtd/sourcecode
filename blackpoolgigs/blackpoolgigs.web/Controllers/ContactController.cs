using blackpoolgigs.web.Models.ViewModels;
using blackpoolgigs.web.Services.Interfaces;
using blackpoolgigs.web.Urls;
using Snooze;

namespace blackpoolgigs.web.Controllers
{
    public class ContactController : ResourceController
    {
        protected readonly IEmail email;
        protected readonly ISayThanks thanks;

        public ContactController(IEmail email, ISayThanks thanks)
        {
            this.email = email;
            this.thanks = thanks;
        }

        public ResourceResult Get(ContactUrl url)
        {
            return OK(new ContactViewModel()).AsHtml();
        }
        public ResourceResult Post(ContactUrl url, ContactViewModel model)
        {
            if (model.InValid())
                return BadRequest(model).AsHtml();
            email.Send(model, Request.ServerVariables["remote_addr"]);
            return Get(new ThanksUrl
                                {
                                    contact = model
                                });
        }
        public ResourceResult Get(ThanksUrl url)
        {
            var result = thanks.Gimme();
            return OK(new ThanksViewModel
                          {
                              ImageAlt = result.Alt,
                              ImagePath = result.Path,
                              ImageTitle = result.Title
                          }).AsHtml();
        }
    }
}