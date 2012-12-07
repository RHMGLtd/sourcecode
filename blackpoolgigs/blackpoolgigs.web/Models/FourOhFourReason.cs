namespace blackpoolgigs.web.Models
{
    public enum FourOhFourReason
    {
        TooFarInTheFuture = 1,
        HistoricalNoGigs,
        DateNotValid,
        NoGigOnThisDate,
        InvalidRouteRequested,
        NoMapForThisVenue
    }
}