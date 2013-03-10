using System.Collections.Generic;
using System.Linq;
using Raven.Client;
using Raven.Imports.Newtonsoft.Json;
using rhmg.StudioDiary.Extensions;

namespace rhmg.StudioDiary
{
    public class Product : Entity
    {
        public override string ToString()
        {
            if (RoomNames.Any())
                return string.Format("{0} {1}",
                    Type.ToSpacedString(),
                    RoomNames.ToStringSeparatedWithAnd());
            return string.Empty;
        }

        public Product()
        {
            RoomIds = new List<string>();
            RoomNames = new List<string>();
        }

        public enum ProductType
        {
            /// <summary>
            /// Will allow you to pick from any of the selected rooms in the product
            /// </summary>
            CanPickFrom = 1,

            /// <summary>
            /// Will book out all rooms listed in this product
            /// </summary>
            BooksOut = 2
        }
        public enum FormType
        {
            /// <summary>
            /// The standard booking form, used normally for rehearsals or recordings
            /// </summary>
            Standard = 1,
            /// <summary>
            /// The extended booking form, used for experiences and parties
            /// </summary>
            Extended = 2,
            /// <summary>
            /// The abbreviated booking form, used for studio closures and other non-standard events
            /// </summary>
            Abbreviated = 3
        }

        public string Name { get; set; }
        public ProductType Type { get; set; }
        public FormType SelectedForm { get; set; }
        public int DisplayOrder { get; set; }
        public string BookingHint { get; set; }
        public List<string> RoomIds { get; set; }
        [JsonIgnore]
        public List<string> RoomNames { get; private set; }

        public Product Save(IDocumentSession session)
        {
            session.Store(this);
            session.SaveChanges();
            return this;
        }
        public static List<Product> All(IDocumentSession session)
        {
            var rooms = Room.All(session);
            var products = session.Query<Product>().OrderBy(x => x.DisplayOrder).ToList();
            products.ForEach(x => x.SetRoomNames(rooms));
            return products;
        }
        public void SetRoomNames(List<Room> rooms)
        {
            RoomNames = rooms.Where(y => RoomIds.Contains(y.Id)).Select(x => x.Name).ToList();
        }

        public List<Room> RoomsToPickFrom(IDocumentSession session)
        {
            if (Type == ProductType.CanPickFrom)
                return session.Load<Room>(RoomIds).ToList();
            return new List<Room>();
        }
        public List<Room> RoomsToBookOut(string roomId, IDocumentSession session)
        {
            var result = new List<Room>();
            if (Type == ProductType.CanPickFrom)
                result.Add(session.Load<Room>(roomId));
            if (Type == ProductType.BooksOut)
                result.AddRange(session.Load<Room>(RoomIds));
            return result;
        }
    }
}


