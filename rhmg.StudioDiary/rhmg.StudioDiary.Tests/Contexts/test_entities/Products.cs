using System.Collections.Generic;

namespace rhmg.StudioDiary.Tests.Contexts.test_entities
{
    public static class Products
    {
        public static Product rehearsal = new Product
        {
            Type = Product.ProductType.CanPickFrom,
            RoomIds = new List<string>
                                                               {
                                                                   TestRooms.liveRoom.Id,
                                                                   TestRooms.room2.Id,
                                                                   TestRooms.room3.Id,
                                                                   TestRooms.room4.Id
                                                               },
            Name = "Rehearsal"
        };
        public static Product recording = new Product
        {
            Type = Product.ProductType.BooksOut,
            RoomIds = new List<string>
                                                               {
                                                                   TestRooms.liveRoom.Id,
                                                                   TestRooms.controlRoom.Id
                                                               },
            Name = "Recording"
        };
        public static Product mastering = new Product
        {
            Type = Product.ProductType.BooksOut,
            RoomIds = new List<string>
                                                               {
                                                                   TestRooms.controlRoom.Id
                                                               },
            Name = "Mastering"
        };
        public static Product party = new Product
        {
            Type = Product.ProductType.BooksOut,
            RoomIds = new List<string>
                                                               {
                                                                   TestRooms.liveRoom.Id,
                                                                   TestRooms.controlRoom.Id,
                                                                   TestRooms.room2.Id,
                                                                   TestRooms.room3.Id,
                                                                   TestRooms.room4.Id
                                                               },
            Name = "Party"
        };
    }
}