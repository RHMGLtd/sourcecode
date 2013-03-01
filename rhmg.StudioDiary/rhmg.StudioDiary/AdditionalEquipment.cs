using System.Collections.Generic;
using System.Linq;
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

        public static List<AdditionalEquipment> All(IDocumentSession session)
        {
            return session.Query<AdditionalEquipment>().ToList();
        }
    }
}