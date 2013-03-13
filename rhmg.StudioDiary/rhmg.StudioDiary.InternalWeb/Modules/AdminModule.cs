using System;
using System.Linq;
using Nancy;
using Nancy.ModelBinding;
using Raven.Client;
using rhmg.StudioDiary.InternalWeb.ViewModels;
using rhmg.StudioDiary.Raven;

namespace rhmg.StudioDiary.InternalWeb.Modules
{
    public class AdminModule : NancyModule
    {
        public AdminModule(IRavenSessionProvider store)
        {
            using (var session = store.GetSession())
            {
                Get["/Admin"] = parameters => View[new AdminIndexModel
                {
                    Rooms = Room.All(session),
                    Rates = Rate.All(session),
                    AdditionalEquipment = AdditionalEquipment.All(session),
                    Products = Product.All(session)
                }];
                Rooms(session);
                Rates(session);
                AdditionalEquipments(session);
                Products(session);
            }
        }
        void AdditionalEquipments(IDocumentSession session)
        {
            Get["/Admin/AdditionalEquipment/Add"] = parameters => View[new AddEquipmentModel
                                                             {
                                                                 CurrentEquipment = session.Query<AdditionalEquipment>().ToList()
                                                             }];
            Get["/Admin/AdditionalEquipment/{id}"] = parameters =>
            {
                var eq = session.Load<AdditionalEquipment>("AdditionalEquipment/" + parameters.id.ToString()) as AdditionalEquipment;
                return View[new EditEquipmentModel
                {
                    EquipmentId = eq.Id,
                    Description = eq.Description,
                    UnitCost = eq.UnitCost,
                    CurrentEquipment = AdditionalEquipment.All(session)
                }];
            };
            Post["/Admin/AdditionalEquipment/Add"] = parameters =>
            {
                var model = this.Bind<AddEquipmentModel>();
                var eq = new AdditionalEquipment
                             {
                                 Description = model.Description,
                                 UnitCost = model.UnitCost
                             };
                eq.Save(session);
                return Response.AsRedirect("/Admin/");
            };
            Post["/Admin/AdditionalEquipment/{id}"] = parameters =>
            {
                var model = this.Bind<EditEquipmentModel>();
                var eq = session.Load<AdditionalEquipment>(model.EquipmentId);
                eq.Description = model.Description;
                eq.UnitCost = model.UnitCost;
                eq.Save(session);
                return Response.AsRedirect("/Admin/");
            };
        }

        void Rooms(IDocumentSession session)
        {
            Get["/Admin/Rooms/Add"] = parameters => View[new AddRoomModel
                                                             {
                                                                 CurrentRooms = Room.All(session),
                                                                 AvailableRates = Rate.All(session)
                                                             }];
            Get["/Admin/Rooms/{id}"] = parameters =>
            {
                var room = session.Load<Room>("rooms/" + parameters.id.ToString()) as Room;
                return View[new EditRoomModel
                {
                    RoomId = room.Id,
                    Name = room.Name,
                    DisplayOrder = room.DisplayOrder,
                    RateIds = room.Rates.Select(x => x.Id).ToList(),
                    CurrentRooms = Room.All(session),
                    AvailableRates = Rate.All(session)
                }];
            };
            Post["/Admin/Rooms/Add"] = parameters =>
            {
                var model = this.Bind<AddRoomModel>();
                var rates = session.Load<Rate>(model.RateIds);
                var room = new Room
                {
                    Name = model.Name,
                    Rates = rates,
                    DisplayOrder = model.DisplayOrder
                };
                room.Save(session);
                return Response.AsRedirect("/Admin/");
            };
            Post["/Admin/Rooms/{id}"] = parameters =>
            {
                var model = this.Bind<EditRoomModel>();
                var room = session.Load<Room>(model.RoomId);
                var rates = session.Load<Rate>(model.RateIds);
                room.Name = model.Name;
                room.Rates = rates;
                room.DisplayOrder = model.DisplayOrder;
                room.Save(session);
                return Response.AsRedirect("/Admin/");
            };
        }

        void Rates(IDocumentSession session)
        {
            Get["/Admin/Rates/Add"] = parameters => View[new AddRateModel
            {
                CurrentRates = Rate.All(session)
            }];
            Get["/Admin/Rates/{id}"] = parameters =>
                                           {
                                               var rate = session.Load<Rate>("rates/" + parameters.id.ToString());
                                               return View[new EditRateModel
                                                        {
                                                            RateId = rate.Id,
                                                            Description = rate.Description,
                                                            PoundsAmount = rate.PoundsAmount,
                                                            DaysPer = rate.Per.Days,
                                                            HoursPer = rate.Per.Hours,
                                                            MinutesPer = rate.Per.Minutes,
                                                            CurrentRates = Rate.All(session)
                                                        }];
                                           };
            Post["/Admin/Rates/Add"] = parameters =>
            {
                var model = this.Bind<AddRateModel>();
                var rate = new Rate
                {
                    Description = model.Description,
                    PoundsAmount = model.PoundsAmount,
                    Per = new TimeSpan(model.DaysPer, model.HoursPer, model.MinutesPer, 0)
                };
                rate.Save(session);
                return Response.AsRedirect("/Admin/");
            };
            Post["/Admin/Rates/{id}"] = parameters =>
            {
                var model = this.Bind<EditRateModel>();
                var rate = session.Load<Rate>(model.RateId);
                rate.Description = model.Description;
                rate.PoundsAmount = model.PoundsAmount;
                rate.Per = new TimeSpan(model.DaysPer, model.HoursPer, model.MinutesPer, 0);
                rate.Save(session);
                return Response.AsRedirect("/Admin/");
            };
        }

        void Products(IDocumentSession session)
        {
            Get["/Admin/Products/Add"] = parameters => View[new AddProductModel
            {
                CurrentProducts = Product.All(session),
                AvailableRooms = Room.All(session)
            }];
            Get["/Admin/Products/{id}"] = parameters =>
            {
                var product = session.Load<Product>("products/" + parameters.id.ToString()) as Product;
                return View[new EditProductModel
                {
                    ProductId = product.Id,
                    Name = product.Name,
                    RoomIds = product.RoomIds,
                    DisplayOrder = product.DisplayOrder,
                    Type = (int)product.Type,
                    SelectedForm = (int)product.SelectedForm,
                    BookingHint = product.BookingHint,
                    CurrentProducts = Product.All(session),
                    AvailableRooms = Room.All(session)
                }];
            };
            Post["/Admin/Products/Add"] = parameters =>
            {
                var model = this.Bind<AddProductModel>();
                var rate = new Product
                               {
                                   Name = model.Name,
                                   Type = (Product.ProductType)model.Type,
                                   SelectedForm = (Product.FormType)model.SelectedForm,
                                   RoomIds = model.RoomIds,
                                   BookingHint = model.BookingHint,
                                   DisplayOrder = model.DisplayOrder
                               };
                rate.Save(session);
                return Response.AsRedirect("/Admin/");
            };
            Post["/Admin/Products/{id}"] = parameters =>
            {
                var model = this.Bind<EditProductModel>();
                var product = session.Load<Product>(model.ProductId);
                product.Name = model.Name;
                product.Type = (Product.ProductType)model.Type;
                product.SelectedForm = (Product.FormType)model.SelectedForm;
                product.RoomIds = model.RoomIds;
                product.DisplayOrder = model.DisplayOrder;
                product.BookingHint = model.BookingHint;
                product.Save(session);
                return Response.AsRedirect("/Admin/");
            };
        }
    }
}