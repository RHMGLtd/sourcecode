using System.Linq;
using blackpoolgigs.common.Contracts;
using blackpoolgigs.common.Interfaces;
using blackpoolgigs.common.Models;
using coolbunny.common.Extensions;
using coolbunny.common.Helpers;
using rhmg.AdministrateBlackpoolGigs.web.Models.ViewModels;
using rhmg.AdministrateBlackpoolGigs.web.Urls;
using Snooze;

namespace rhmg.AdministrateBlackpoolGigs.web.Controllers
{
    public class VenuesController : ResourceController
    {
        protected readonly IGiveYouVenues venues;
        protected readonly IStoreMetadata metadata;
        protected readonly IDealWithRavenAttachments attachments;

        public VenuesController(IGiveYouVenues venues, IStoreMetadata metadata, IDealWithRavenAttachments attachments)
        {
            this.venues = venues;
            this.attachments = attachments;
            this.metadata = metadata;
        }

        public ResourceResult Get(VenuesUrl url)
        {
            return OK(new VenuesViewModel
            {
                Params = new PagingParams(url, ""),
                Venues = venues.DiaryPlusMetadata(new PagingParams(url))
            }).AsHtml();
        }
        public ResourceResult Get(VenueMainImageUrl url)
        {
            var venue = venues.GetMetadata(new VenueName { Value = url.VenueName });
            if (venue.IsEmpty())
                return NotFound();
            var attachment = attachments.MainImage(venue.Id);
            if (attachment == null)
                return NotFound();
            var fileName = attachment.Metadata.Value<string>("Filename");

            return OK(attachment.Data)
                .AsFile(fileName.Split('.').Last(), fileName)
                .WithHeader("etag", attachment.Etag.ToString());
        }
        public ResourceResult Get(EditVenueUrl url)
        {
            var metadata = venues.GetMetadata(new VenueName { Value = url.VenueName });
            return OK(new VenueViewModel
                          {
                              Name = new VenueName { Value = url.VenueName },
                              BarAndClubReviewsUrl = metadata.BarAndClubReviewsUrl,
                              AddressLines = metadata.ContactDetail.AddressLines,
                              ContactName = metadata.ContactDetail.ContactName,
                              EmailAddress = metadata.ContactDetail.EmailAddress,
                              Website = metadata.ContactDetail.Website,
                              Id = metadata.Id,
                              Lat = metadata.MapCoords.Lat,
                              Long = metadata.MapCoords.Long,
                              MainImageName = metadata.MainImageName,
                              MainPhoneNumber = metadata.ContactDetail.MainPhoneNumber,
                              PostCode = metadata.ContactDetail.PostCode,
                              SecondaryPhoneNumbers = metadata.ContactDetail.SecondaryPhoneNumbers,
                              Gigs = venues.Get(new VenueName { Value = url.VenueName }).Gigs
                          }).AsHtml();
        }

        public ResourceResult Delete(MetadataUrl url)
        {
            metadata.Delete(url.Id.EnsureStartsWith("metadata/"));
            return OK(true).AsJson();
        }
        public ResourceResult Post(EditVenueUrl url, VenueViewModel input)
        {
            var result = new VenueMetadata
                               {
                                   Id = input.Id,
                                   VenueName = url.VenueName,
                                   MainImageName = input.MainImage == null ? input.MainImageName : url.VenueName + "." + input.MainImage.FileName.Split('.').Last(),
                                   BarAndClubReviewsUrl = input.BarAndClubReviewsUrl.EnsureHttp(),
                                   ContactDetail = new ContactDetail
                                                       {
                                                           EmailAddress = input.EmailAddress,
                                                           MainPhoneNumber = input.MainPhoneNumber,
                                                           PostCode = input.PostCode,
                                                           Website = input.Website.EnsureHttp(),
                                                           AddressLines = input.AddressLines ?? new string[] { },
                                                           ContactName = input.ContactName,
                                                           SecondaryPhoneNumbers = input.SecondaryPhoneNumbers ?? new string[] { }
                                                       },
                                   MapCoords = new GoogleMapCoords
                                                   {
                                                       Lat = input.Lat,
                                                       Long = input.Long
                                                   }
                               };
            metadata.Store(result, input.MainImage);
            return SeeOther(new VenuesUrl());
        }
    }
}