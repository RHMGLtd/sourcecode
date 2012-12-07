namespace blackpoolgigs.web.Models.ViewModels
{
    public abstract class NotFoundViewModel
    {
        public FourOhFourReason Reason { get; set; }
        public Blob Blob { get; internal set; }
        public string VenueName { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }

        public string ReasonString()
        {
            if (Reason == FourOhFourReason.TooFarInTheFuture)
                return "this date is too far in the future.";
            if (Reason == FourOhFourReason.DateNotValid)
                return "the date you input is not valid.";
            if (Reason == FourOhFourReason.NoGigOnThisDate)
                return "we have gigs on this date at this venue";
            if (Reason == FourOhFourReason.InvalidRouteRequested)
                return "your request did not match anything on our site";
            if (Reason == FourOhFourReason.NoMapForThisVenue)
                return "unfortunately we do not have the coordinates for this venue";
            return "we have no gigs in our archive for this date.";
        }
    }
}