using System.Collections.Generic;
using System.Linq;
using Raven.Client;
using Raven.Client.Linq;

namespace rhmg.StudioDiary
{
    public class Room : Entity
    {
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
        public Rate[] Rates { get; set; }
        
        public void Save(IDocumentSession session)
        {
            session.Store(this);
            session.SaveChanges();
        }

        public static List<Room> All(IDocumentSession session)
        {
            return session.Query<Room>().OrderBy(x => x.DisplayOrder).ToList();
        }
    }
}