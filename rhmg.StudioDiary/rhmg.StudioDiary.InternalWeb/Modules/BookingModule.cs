using System;
using System.Linq;
using Nancy;
using Nancy.ModelBinding;
using Raven.Client;
using rhmg.StudioDiary.Extensions;
using rhmg.StudioDiary.InternalWeb.ViewModels;
using rhmg.StudioDiary.Raven;

namespace rhmg.StudioDiary.InternalWeb.Modules
{

    public class BookingModule : NancyModule
    {
        public BookingModule(IRavenSessionProvider store)
        {
            using (var session = store.GetSession())
            {
                Get[@"/booking"] = parameters =>
                {
                    if (ValidBookingQueryString())
                        return new NotFoundResponse();
                    var date = new DateTime(Request.Query.year, Request.Query.month,
                                            Request.Query.day);
                    string productName = Request.Query.product;
                    var product = session.Query<Product>()
                                               .FirstOrDefault(x => x.Name == productName.ToSpacedString());
                    if (product == null)
                        return new NotFoundResponse();
                    var rooms = product.RoomsToPickFrom(session);
                    if (product.SelectedForm == Product.FormType.Standard)
                        return View[new StandardFormBookingModel
                        {
                            ProductFriendlyName = product.Name,
                            BookingHint = product.BookingHint,
                            Date = date,
                            CurrentBookings = DiaryManager.DayCheck(date, session),
                            Rooms = rooms,
                            AllRooms = Room.All(session),
                            AvailableAdditionalEquipment = AdditionalEquipment.All(session)
                        }];
                    if (product.SelectedForm == Product.FormType.Extended)
                        return View[new ExtendedFormBookingModel
                        {
                            ProductFriendlyName = product.Name,
                            BookingHint = product.BookingHint,
                            Date = date,
                            CurrentBookings = DiaryManager.DayCheck(date, session),
                            Rooms = rooms,
                            AllRooms = Room.All(session),
                            AvailableAdditionalEquipment = AdditionalEquipment.All(session)
                        }];
                    if (product.SelectedForm == Product.FormType.Abbreviated)
                        return View[new AbbreviatedFormBookingModel
                        {
                            ProductFriendlyName = product.Name,
                            BookingHint = product.BookingHint,
                            Date = date,
                            CurrentBookings = DiaryManager.DayCheck(date, session),
                            Rooms = rooms,
                            AllRooms = Room.All(session)
                        }];
                    return new NotFoundResponse();

                };
                Get[@"/booking/{bookingId}"] = parameters =>
                {
                    var booking =
                        Booking.Get("booking/" + parameters.bookingId,
                                    session) as Booking;
                    if (booking.Product.SelectedForm == Product.FormType.Standard)
                        return View[new StandardFormBookingModel(booking)
                        {
                            CurrentBookings = DiaryManager.DayCheck(booking.Date, session),
                            Rooms = booking.Product.RoomsToPickFrom(session),
                            AllRooms = Room.All(session),
                            AvailableAdditionalEquipment = AdditionalEquipment.All(session)
                        }];
                    return new NotFoundResponse();
                };

                Post[@"/booking"] = parameters => SaveBooking(session);
                Post[@"/booking/{bookingId}"] = parameters => SaveBooking(session);
            }
        }

        dynamic SaveBooking(IDocumentSession session)
        {
            var baseForm = this.Bind<BaseFormBookingModel>();
            var product = session.Query<Product>()
                .FirstOrDefault(x => x.Name == baseForm.ProductFriendlyName);
            if (product == null)
                return new NotFoundResponse();
            if (product.SelectedForm == Product.FormType.Standard)
            {
                var booking = this.Bind<StandardFormBookingModel>();
                booking.CreateBooking(product, session);
            }
            if (product.SelectedForm == Product.FormType.Extended)
            {
                var booking = this.Bind<ExtendedFormBookingModel>();
                booking.CreateBooking(product, session);
            }
            if (product.SelectedForm == Product.FormType.Abbreviated)
            {
                var booking = this.Bind<AbbreviatedFormBookingModel>();
                booking.CreateBooking(product, session);
            }
            return Response.AsRedirect("/");
        }

        dynamic ValidBookingQueryString()
        {
            return !Request.Query.year.HasValue ||
                   !Request.Query.month.HasValue ||
                   !Request.Query.day.HasValue ||
                   !Request.Query.product.HasValue;
        }
    }
}


