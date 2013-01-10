using System;
using System.Collections.Generic;
using System.Linq;

namespace rhmg.StudioDiary
{
    public class Booking : Entity
    {
        public bool HasOutstandingOwings { get; set; }
        public bool IsCancelled { get; set; }
        public double CurrentlyOwed { get; set; }

        public List<Contact> Contacts { get; set; }
        public DateTime Date { get; set; }
        public TimePart StartTime { get; set; }
        public TimeSpan Length { get; set; }
        public Room Room { get; set; }
        public Rate Rate { get; set; }
        public List<Note> Notes { get; set; }

        public Cancellation Cancellation { get; set; }
        public List<Payment> Payments { get; set; }

        public List<AdditionalEquipment> AdditionalEquipment { get; set; }

        public bool CheckedIn { get; set; }

        public static Booking Create(List<Contact> contacts, DateTime date, TimePart startTime, TimeSpan length, Room room, Rate rate)
        {
            return new Booking
                       {
                           Contacts = contacts,
                           Date = date,
                           StartTime = startTime,
                           Length = length,
                           Room = room,
                           Rate = rate
                       };
        }

        public static Booking Get(string id, IRepository<Booking> repo)
        {
            return repo.Get(id);
        }

        public Booking()
        {
            Payments = new List<Payment>();
            AdditionalEquipment = new List<AdditionalEquipment>();
            Notes = new List<Note>();
        }

        public Booking Save(IRepository<Booking> repo)
        {
            // set flags for searching
            HasOutstandingOwings = Outstanding() > 0;
            IsCancelled = Cancellation != null;
            CurrentlyOwed = Outstanding();

            return repo.Put(this);
        }

        public double Value()
        {
            return Rate.For(Length) + AdditionalEquipment.Sum(x => x.UnitCost);
        }

        public double Outstanding()
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
    }
}