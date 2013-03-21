using System;
using System.Collections.Generic;
using System.Linq;
using Nancy.Extensions;
using Raven.Client;
using Raven.Imports.Newtonsoft.Json;

namespace rhmg.StudioDiary
{
    public class NullBooking : Booking
    {
        public override bool IsNull()
        {
            return true;
        }
        public override string SummariseBooking()
        {
            return string.Empty;
        }
    }
    public class Booking : Entity
    {
        public virtual bool IsNull()
        {
            return false;
        }
        public bool HasOutstandingOwings { get; set; }
        public bool IsCancelled { get; set; }
        public double CurrentlyOwed { get; set; }
        public string BookingSummary { get; set; }

        public string MainContactId { get; set; }
        [JsonIgnore]
        public Contact MainContact { get; set; }
        public DateTime Date { get; set; }
        public TimePart StartTime { get; set; }
        public TimeSpan Length { get; set; }
        public List<Room> Rooms { get; set; }
        public Rate Rate { get; set; }
        public double OneOffCharge { get; set; }
        public Product Product { get; set; }
        public List<Note> Notes { get; set; }

        public bool MakeUpSession { get; set; }
        public bool Biscuits { get; set; }
        public string SongChoice { get; set; }
        public string NumberInParty { get; set; }
        public bool PizzaOnTheDay { get; set; }

        public Cancellation Cancellation { get; set; }
        public List<Payment> Payments { get; set; }
        public List<Payment> Refunds { get; set; }
        public List<AdditionalEquipment> AdditionalEquipment { get; set; }

        public bool CheckedIn { get; set; }
        public bool IsNoShow { get; set; }

        public static NullBooking GetNull() { return new NullBooking(); }
        public static Booking Create(string contactId, DateTime date, TimePart startTime, TimeSpan length, Room room, Rate rate, Product product)
        {
            return new Booking
                       {
                           MainContactId = contactId,
                           Date = date,
                           StartTime = startTime,
                           Length = length,
                           Rooms = new List<Room> { room },
                           Rate = rate,
                           Product = product
                       };
        }
        public static Booking Get(string id, IDocumentSession session)
        {
            var booking = session.Include<Booking>(x => x.MainContactId).Load(id);
            booking.MainContact = session.Load<Contact>(booking.MainContactId);
            return booking;
        }
        public Booking()
        {
            Payments = new List<Payment>();
            Refunds = new List<Payment>();
            Rooms = new List<Room>();
            AdditionalEquipment = new List<AdditionalEquipment>();
            Notes = new List<Note>();
        }
        public Booking Save(IDocumentSession session)
        {
            // set flags for searching
            HasOutstandingOwings = Outstanding() > 0;
            IsCancelled = Cancellation != null;
            CurrentlyOwed = Outstanding();

            session.Store(this);
            session.SaveChanges();
            return this;
        }
        public virtual string SummariseBooking()
        {
            if (MainContact == null)
                return Product.Name;
            return string.Concat(MainContact.Name,
                                 ". Phone number: ",
                                 MainContact.PhoneNumber,
                                 ". value: ",
                                 string.Format("{0:£0.00}",
                                               Value()));
        }

        public double Value()
        {
            if (OneOffCharge > 0)
                return OneOffCharge + AdditionalEquipment.Sum(x => x.UnitCost);
            return Rate.For(Length) + AdditionalEquipment.Sum(x => x.UnitCost);
        }

        public double Outstanding()
        {
            return CalculateValueBeforeRefunds() + Refunds.Sum(x => x.Amount);
        }

        double CalculateValueBeforeRefunds()
        {
            if (Cancellation != null)
            {
                if (Cancellation.Type == CancellationType.NoCost)
                    return 0.00;
                if (Cancellation.Type == CancellationType.HalfCost)
                    return Value() / 2 - Payments.Sum(x => x.Amount);
            }
            return Value() - Payments.Sum(x => x.Amount);
        }
        public void ApplyPayment(Payment payment)
        {
            Payments.Add(payment);
        }
        public void ApplyRefund(Payment refund)
        {
            Refunds.Add(refund);
        }
        public void Cancel(CancellationType cancellationType, string reason, DateTime madeOn)
        {
            if (Cancellation == null)
                Cancellation = new Cancellation
                                   {
                                       DateMade = madeOn,
                                       Reason = reason,
                                       Type = cancellationType
                                   };
        }
        public void AddAdditionalEquipment(AdditionalEquipment additionalEquipment)
        {
            AdditionalEquipment.Add(additionalEquipment);
        }
        public void AddNote(Note note)
        {
            Notes.Add(note);
        }
        public bool IsInWeekdayPeakTime()
        {
            if (Date.DayOfWeek == DayOfWeek.Saturday || Date.DayOfWeek == DayOfWeek.Sunday)
                return false;
            if (StartTime.Hour == 18 &&
                Length > new TimeSpan(0, 1, 0, 0))
                return true;
            if (StartTime.Hour >= 19)
                return true;
            return false;
        }
        public void CheckIn()
        {
            CheckedIn = true;
            IsNoShow = false;
        }
        public void NoShow()
        {
            IsNoShow = true;
            CheckedIn = false;
        }
        /*public void SaveAttachment(IFile file, IFileSystem fs, string rootDirectory)
        {
            if (string.IsNullOrEmpty(Id))
                throw new IndexOutOfRangeException("You cannot add attachments when the booking is not saved");
            var folder = fs.GetDirectory(rootDirectory.EnsureEndsWith("/") + Id) ??
                         fs.CreateDirectory(rootDirectory.EnsureEndsWith("/") + Id);

            var bob = fs.GetFile()
            folder.CopyTo(file);
        }*/
        public bool IsValidFor(int hour)
        {
            var endTime = StartTime.Hour + Length.Hours;
            return hour >= StartTime.Hour && hour < endTime;
        }

        public List<string> FlattenAdditionalEquipment()
        {
            return AdditionalEquipment.DistinctBy(x => x.Id).Select(eq => string.Format("{0}_{1}", 
                                                                            eq.Id.Replace("additionalequipment/", "eq"), 
                                                                            AdditionalEquipment.Count(x => x.Id == eq.Id))).ToList();
        }
    }
}