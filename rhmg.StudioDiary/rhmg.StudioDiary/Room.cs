using Raven.Client;

namespace rhmg.StudioDiary
{
    public class Room : Entity
    {
        public string Name { get; set; }
        public Rate[] Rates { get; set; }
        
        public void Save(IDocumentSession session)
        {
            session.Store(this);
            session.SaveChanges();
        }
    }
}