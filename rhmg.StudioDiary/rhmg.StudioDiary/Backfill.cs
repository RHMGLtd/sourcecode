using System;

namespace rhmg.StudioDiary
{
    public class BackFill : Entity
    {
        public Contact Contact { get; set; }

        public DateTime Date { get; set; }

        public static BackFill Create(Contact contact, DateTime date)
        {
            return new BackFill
                       {
                           Contact = contact,
                           Date = date
                       };
        }

        public BackFill Save(IRepository<BackFill> repo)
        {
            return repo.Put(this);
        }

        public static BackFill Get(string id, IRepository<BackFill> repo)
        {
            return repo.Get(id);
        }
    }
}