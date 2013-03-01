using System;
using System.Collections.Generic;
using System.Linq;
using Raven.Client;
using Raven.Imports.Newtonsoft.Json;
using rhmg.StudioDiary.Raven.Indexes;

namespace rhmg.StudioDiary
{
    public class Contact : Entity
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string MainContactName { get; set; }
        [JsonIgnore]
        public List<Booking> Bookings { get; set; }

        
        public Contact Save(IDocumentSession session)
        {
            if (session.Query<Contact>().Any(x => x.PhoneNumber == PhoneNumber))
                return this;
            session.Store(this);
            session.SaveChanges();
            return this;
        }
        static void AddBookingsToContactLazily(IDocumentSession session, Contact result)
        {
            var queryable = session.Query<Bookings_ByContact.Result>("bookings/bycontact").FirstOrDefault(x => x.ContactId == result.Id);
            if (queryable != null)
                result.Bookings = session.Load<Booking>(queryable.BookingIds).ToList();
        }

        public static Contact Get(string id, IDocumentSession session)
        {
            var result = session.Load<Contact>(id);
            AddBookingsToContactLazily(session, result);
            return result;
        }
        public static Contact GetByPhone(string phoneNumber, IDocumentSession session)
        {
            var result = session.Advanced.LuceneQuery<Contact>()
                .WhereEquals(x => x.PhoneNumber, phoneNumber).WaitForNonStaleResults().FirstOrDefault();
            if (result != null)
                AddBookingsToContactLazily(session, result);
            return result;
        }

        public double CurrentlyOverdue(IDocumentSession session)
        {
            var results = session.Query<CustomerArrears.Result>("customerarrears").Where(x => x.ContactId == Id);
            return results.Any() ? results.First().AmountOwed : 0.00;
        }

        public CustomerArrears.Result GetArrears(IDocumentSession session)
        {
            return session.Query<CustomerArrears.Result>("CustomerArrears")
                .Customize(x => x.Include("BookingIds,"))
                .FirstOrDefault(x => x.ContactId == Id);
        }

        public void ApplyRefund(double refundAmount, IDocumentSession session)
        {
            if (refundAmount >= 0)
                refundAmount = refundAmount*-1;
            var arrears = GetArrears(session);
            if (arrears != null &&
                arrears.BookingIds.Any())
            {
                var booking = session.Load<Booking>(arrears.BookingIds.First());
                booking.ApplyRefund(new Payment
                                         {
                                             Amount = refundAmount,
                                             DateMade = DateTime.Now,
                                             Method = PaymentMethod.InKind,
                                             PaymentType = PaymentType.Refund
                                         });
                booking.Save(session);
            }
        }
    }
}