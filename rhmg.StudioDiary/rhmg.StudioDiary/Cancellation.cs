using System;

namespace rhmg.StudioDiary
{
    public class Cancellation
    {
        public DateTime DateMade { get; set; }
        public string Reason { get; set; }
        public CancellationType Type { get; set; }
    }
}