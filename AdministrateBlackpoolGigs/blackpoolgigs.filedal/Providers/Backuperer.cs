using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Hosting;
using blackpoolgigs.common.Interfaces;
using blackpoolgigs.common.Models;
using coolbunny.common.Extensions;
using coolbunny.common.Interfaces;
using Newtonsoft.Json;
using OpenFileSystem.IO;

namespace blackpoolgigs.filedal.Providers
{
    public class Backuperer : ISaveBackups
    {
        protected readonly IFileSystem files;
        protected readonly IEnvironmentConfiguration config;

        public Backuperer(IFileSystem files, IEnvironmentConfiguration config)
        {
            this.files = files;
            this.config = config;
        }

        public void Save(IEnumerable<Diary> diaries,
            CountCollection counts,
            MonthlyGigCounts monthlyCounts,
            IEnumerable<VenueDiary> venueDiaries,
            IEnumerable<BandDiary> bandDiaries,
            IEnumerable<BandMetadata> bandMetadata,
            IEnumerable<VenueMetadata> venueMetadata,
            IEnumerable<StolenGear> stolenGear,
            IEnumerable<Gig> recentlyUpdated)
        {
            ClearExportFolder();
            WriteDiaries(diaries);
            WriteCounts(counts);
            WriteMonthlyCounts(monthlyCounts);
            WriteVenueDiaries(venueDiaries);
            WriteBandDiaries(bandDiaries);
            WriteBandMetadata(bandMetadata);
            WriteVenueMetadata(venueMetadata);
            WriteRecentlyUpdated(recentlyUpdated);
            ExportImages(venueMetadata);
            WriteStolenGear(stolenGear);
            CopyStolenGearImages();
        }
        
        void ClearExportFolder()
        {
            var path = HostingEnvironment.MapPath("~/Content/Export");
            if (!files.GetDirectory(path).Exists)
                files.CreateDirectory(path);
            new DirectoryInfo(path).Empty();

            path = HostingEnvironment.MapPath("~/Content/Export/Venues/");
            if (!files.GetDirectory(path).Exists)
                files.CreateDirectory(path);
            path = HostingEnvironment.MapPath("~/Content/Export/Venues/Images/");
            if (!files.GetDirectory(path).Exists)
                files.CreateDirectory(path);
            path = HostingEnvironment.MapPath("~/Content/Export/Venues/Metadata/");
            if (!files.GetDirectory(path).Exists)
                files.CreateDirectory(path);
            path = HostingEnvironment.MapPath("~/Content/Export/Bands/");
            if (!files.GetDirectory(path).Exists)
                files.CreateDirectory(path);
            path = HostingEnvironment.MapPath("~/Content/Export/Bands/Metadata/");
            if (!files.GetDirectory(path).Exists)
                files.CreateDirectory(path);
            path = HostingEnvironment.MapPath("~/Content/Export/StolenGear/");
            if (!files.GetDirectory(path).Exists)
                files.CreateDirectory(path);
        }

        void WriteMonthlyCounts(MonthlyGigCounts monthlyCounts)
        {
            var jsonMonthlyCounts = JsonConvert.SerializeObject(monthlyCounts, Formatting.Indented);
            File.WriteAllText(HostingEnvironment.MapPath("~/Content/Export/") + "monthlyCounts.json", jsonMonthlyCounts);
        }

        void WriteCounts(CountCollection counts)
        {
            var jsonCounts = JsonConvert.SerializeObject(counts, Formatting.Indented);
            File.WriteAllText(HostingEnvironment.MapPath("~/Content/Export/") + "counts.json", jsonCounts);
        }

        void WriteRecentlyUpdated(IEnumerable<Gig> recentlyUpdated)
        {
            var jsonCounts = JsonConvert.SerializeObject(recentlyUpdated, Formatting.Indented);
            File.WriteAllText(HostingEnvironment.MapPath("~/Content/Export/") + "recentlyUpdated.json", jsonCounts);
        }

        void WriteDiaries(IEnumerable<Diary> diaries)
        {
            foreach (var diary in diaries)
            {
                var json = JsonConvert.SerializeObject(diary, Formatting.Indented);
                File.WriteAllText(HostingEnvironment.MapPath("~/Content/Export/") + diary.FirstDayOfMonth.ToString("MMM yyyy") + ".json", json);
            }
        }

        void WriteVenueDiaries(IEnumerable<VenueDiary> venueDiaries)
        {
            var path = HostingEnvironment.MapPath("~/Content/Export/Venues/");
            foreach (var diary in venueDiaries)
            {
                var json = JsonConvert.SerializeObject(diary, Formatting.Indented);
                File.WriteAllText(path + diary.Venue.Tidy() + ".json", json);
            }
        }

        void WriteBandDiaries(IEnumerable<BandDiary> bandDiaries)
        {
            var path = HostingEnvironment.MapPath("~/Content/Export/Bands/");
            foreach (var diary in bandDiaries)
            {
                var json = JsonConvert.SerializeObject(diary, Formatting.Indented);
                File.WriteAllText(path + diary.BandName.Value.Tidy() + ".json", json);
            }
        }

        void WriteBandMetadata(IEnumerable<BandMetadata> bandMetadata)
        {
            var path = HostingEnvironment.MapPath("~/Content/Export/Bands/Metadata/");
            foreach (var metadata in bandMetadata)
            {
                var json = JsonConvert.SerializeObject(metadata, Formatting.Indented);
                File.WriteAllText(path + metadata.BandName.Tidy() + ".json", json);
            }
        }

        void ExportImages(IEnumerable<VenueMetadata> metadata)
        {
            var path = HostingEnvironment.MapPath("~/Content/Export/Venues/Images/");
            foreach (var venueMetadata in
                metadata.Where(venueMetadata => !string.IsNullOrEmpty(venueMetadata.MainImageName)))
            {
                try
                {
                    var www = config.WebPath + "/venues/" + HttpUtility.HtmlEncode(venueMetadata.VenueName) + "/mainimage";
                    var request = (HttpWebRequest)WebRequest.Create(www);
                    request.Accept = "image/jpeg";

                    var response = request.GetResponse();
                    var image = Image.FromStream(response.GetResponseStream());
                    image.Resize(180).Save(path + venueMetadata.MainImageName);
                }
                catch (Exception x)
                {
                    throw new InvalidOperationException(string.Format("venue attempting to export image for = {0}", venueMetadata.VenueName), x);
                }
            }
        }

        void WriteVenueMetadata(IEnumerable<VenueMetadata> metadata)
        {
            var path = HostingEnvironment.MapPath("~/Content/Export/Venues/Metadata/");
            foreach (var thing in metadata)
            {
                var json = JsonConvert.SerializeObject(thing, Formatting.Indented);
                File.WriteAllText(path + thing.VenueName.Tidy() + ".json", json);
            }
        }

        void WriteStolenGear(IEnumerable<StolenGear> stolenGear)
        {
            var path = HostingEnvironment.MapPath("~/Content/Export/StolenGear/");
            foreach (var gear in stolenGear)
            {
                var json = JsonConvert.SerializeObject(gear, Formatting.Indented);
                File.WriteAllText(path + gear.Headline.Tidy() + ".json", json);
            }
        }

        void CopyStolenGearImages()
        {
            var target = HostingEnvironment.MapPath("~/Content/Export/StolenGear/");
            var source = HostingEnvironment.MapPath("~/Content/StolenImages/");
            files.GetDirectory(source).CopyTo(files.GetDirectory(target));
        }
    }
}