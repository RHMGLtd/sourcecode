using System;

namespace rhmg.StudioDiary
{
    public class Entity
    {
        public string Id { get; set; }
        public DateTime DateCreated { get; set; }

        public Entity()
        {
            DateCreated = DateTime.Now;
        }
    }
}