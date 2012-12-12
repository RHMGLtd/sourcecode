using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Hosting;
using blackpoolgigs.common.Interfaces;
using blackpoolgigs.common.Models;
using coolbunny.common.Extensions;
using Newtonsoft.Json;
using OpenFileSystem.IO;

namespace blackpoolgigs.filedal.Providers
{
    public class FileSystemCache : IAccessTheFileSystem
    {
        protected readonly IFileSystem files;

        ConcurrentDictionary<string, Diary> DiaryCache;
        CountCollection CountsCache;
        MonthlyGigCounts MonthlyCountsCache;
        DateTime cacheRefresh;

        public FileSystemCache(IFileSystem files)
        {
            DiaryCache = new ConcurrentDictionary<string, Diary>();
            cacheRefresh = DateTime.Now.AddMinutes(1);
            this.files = files;
        }
        private void CacheExpiry()
        {
            if (DateTime.Now < cacheRefresh)
                return;
            DiaryCache.Clear();
            CountsCache = null;
            MonthlyCountsCache = null;
            cacheRefresh = DateTime.Now.AddMinutes(1);
        }
        public T ReadOut<T>(string fileName, params T[] errorReturn) where T : new()
        {
            Stream read;
            try
            {
                read =
                    files.GetFile(HostingEnvironment.MapPath("~/Content/Export/") + fileName).OpenRead();
            }
            catch (Exception)
            {
                return errorReturn.Any() ? errorReturn[0] : new T();
            }
            var result = JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(read.ReadFully()));
            read.Close();
            return result;
        }
        public IEnumerable<T> ReadOutList<T>(string fileName)
            where T : new()
        {
            Stream read;
            try
            {
                read =
                    files.GetFile(HostingEnvironment.MapPath("~/Content/Export/") + fileName).OpenRead();
            }
            catch (Exception)
            {
                return new List<T>();
            }
            var result = JsonConvert.DeserializeObject<IEnumerable<T>>(Encoding.UTF8.GetString(read.ReadFully()));
            read.Close();
            return result;
        }
    }
}