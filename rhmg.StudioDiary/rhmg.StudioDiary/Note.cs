using System;

namespace rhmg.StudioDiary
{
    public class Note 
    {
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }

        public Note()
        {
            DateCreated = DateTime.Now;
        }
    }
}