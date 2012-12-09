using System;

namespace rhmg.StudioDiary
{
    public class Payment
    {
        public DateTime DateMade { get; set; }
        public double Amount { get; set; }
        public PaymentMethod Method { get; set; }
        public PaymentType PaymentType { get; set; }
    }
}