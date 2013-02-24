using Raven.Client;

namespace rhmg.StudioDiary
{
    public class AdditionalEquipment : Entity
    {
        public double UnitCost { get; set; }
        public string Description { get; set; }

        public void Save(IDocumentSession session)
        {
            session.Store(this);
            session.SaveChanges();
        }
    }
}