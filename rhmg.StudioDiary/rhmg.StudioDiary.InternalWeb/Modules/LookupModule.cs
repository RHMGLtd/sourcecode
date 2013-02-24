using Nancy;
using Nancy.ModelBinding;
using rhmg.StudioDiary.InternalWeb.ViewModels;
using rhmg.StudioDiary.Raven;

namespace rhmg.StudioDiary.InternalWeb.Modules
{
    public class LookupModule : NancyModule
    {
        public class ContactLookupForm
        {
            public string PhoneNumber { get; set; }
        }

        public LookupModule(IRavenSessionProvider store)
        {
            using (var session = store.GetSession())
            {
                Get["/lookup/rates/for/rooms/{roomId}"] = parameters =>
                                                        {
                                                            var room = session.Load<Room>("rooms/" + parameters.roomId.ToString());
                                                            if (room == null)
                                                                return new NotFoundResponse();
                                                            return Response.AsJson(((Room)room).Rates);
                                                        };
                Post["/lookup/contact/from/phoneNumber"] = parameters =>
                {
                    var model = this.Bind<ContactLookupForm>();
                    return Response.AsJson(new ContactModel
                                               {
                                                   Id = "contact/1",
                                                   EmailAddress = "an.email@somewhere.com",
                                                   MainContactName = "someone",
                                                   Name = "a band",
                                                   PhoneNumber = model.PhoneNumber,
                                                   CurrentOwings = string.Format("{0:£0.00}", 15.00)
                                               });
                };
            }
        }
    }
}