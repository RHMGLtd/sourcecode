using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using rhmg.StudioDiary.Tests.Contexts;
using rhmg.StudioDiary.Tests.Contexts.test_entities;

namespace rhmg.StudioDiary.Tests
{
    public class when_loading_all_products : with_raven_integration<Product, Product>
    {
        static List<Product> products;

        Establish context = () =>
                     {
                         TestRooms.room2.Save(session);
                         TestRooms.room3.Save(session);
                         TestRooms.room4.Save(session);
                         TestRooms.liveRoom.Save(session);
                         new Product
                                           {
                                               Name = "test",
                                               Type = Product.ProductType.CanPickFrom,
                                               RoomIds = new List<string>
                                                                 {
                                                                     TestRooms.room2.Id,
                                                                     TestRooms.room3.Id
                                                                 }
                                           }.Save(session);
                     };

        Because of = () => products = Product.All(session);
        It has_one_product = () => products.Count.ShouldEqual(1);
        It has_loaded_names = () => products.First().RoomNames.Count.ShouldEqual(2);
        It has_the_correct_to_string = () => products.First().ToString().ShouldEqual("Can Pick From Room 2 and Room 3");
    }
}