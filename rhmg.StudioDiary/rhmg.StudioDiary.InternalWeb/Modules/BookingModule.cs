using System;
using System.Linq;
using Nancy;
using Nancy.ModelBinding;
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
                Get[@"/{product}/(?<day>[\d]{1,2})/(?<month>[\d]{1,2})/(?<year>[\d]{1,4})/newbooking/"] = parameters =>
                                                              {
                                                                  var date = new DateTime(parameters.year, parameters.month, parameters.day);
                                                                  string productName = parameters.product;
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
                                                                          AvailableAdditionalEquipment = AdditionalEquipment.All(session)
                                                                      }];
                                                                  if (product.SelectedForm == Product.FormType.Abbreviated)
                                                                      return View[new AbbreviatedFormBookingModel
                                                                      {
                                                                          ProductFriendlyName = product.Name,
                                                                          BookingHint = product.BookingHint,
                                                                          Date = date,
                                                                          CurrentBookings = DiaryManager.DayCheck(date, session),
                                                                          Rooms = rooms
                                                                      }];
                                                                  return new NotFoundResponse();

                                                              };

                Post[@"/{product}/(?<day>[\d]{1,2})/(?<month>[\d]{1,2})/(?<year>[\d]{1,4})/newbooking/"] = parameters =>
                                                                {
                                                                    string spacedString = parameters.product;
                                                                    var product = session.Query<Product>()
                                                                        .FirstOrDefault(x => x.Name == spacedString.ToSpacedString());
                                                                    if (product == null)
                                                                        return new NotFoundResponse();
                                                                    if (product.SelectedForm == Product.FormType.Standard)
                                                                    {
                                                                        var booking = this.Bind<StandardFormBookingModel>();
                                                                        booking.CreateBooking(new DateTime(parameters.year, parameters.month, parameters.day), product, session);
                                                                    }
                                                                    if (product.SelectedForm == Product.FormType.Extended)
                                                                    {
                                                                        var booking = this.Bind<ExtendedFormBookingModel>();
                                                                        booking.CreateBooking(new DateTime(parameters.year, parameters.month, parameters.day), product, session);
                                                                    }
                                                                    if (product.SelectedForm == Product.FormType.Abbreviated)
                                                                    {
                                                                        var booking = this.Bind<AbbreviatedFormBookingModel>();
                                                                        booking.CreateBooking(new DateTime(parameters.year, parameters.month, parameters.day), product, session);
                                                                    }
                                                                    return Response.AsRedirect("/");
                                                                };
            }
        }
    }
}