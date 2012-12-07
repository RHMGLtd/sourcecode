using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using blackpoolgigs.common.Contracts;
using coolbunny.common.Extensions;
using Newtonsoft.Json;
using OpenFileSystem.IO;

namespace blackpoolgigs.common.Models
{
    public class SmartMatch<T> where T : new()
    {
        public SmartMatch()
        {
            Results = new List<T>();
        }
        public IList<T> Results { get; set; }
        public bool ExactMatchFound { get { return Results.Count() == 1; } }

        public SmartMatch<T> Do(IDirectory dir, VenueName venueName)
        {
            var options = new SmartMatch<T>();
            // exact match
            var search = dir.Files(venueName.Value + ".json");
            if (search.Count() == 1)
            {
                Results.Add(ReadAndClose<T>(search.First()));
                return this;
            }
            foreach (var file in search)
            {
                options.Results.Add(ReadAndClose<T>(file));
            }
            // remove the
            if (venueName.Value.ToLower().StartsWith("the "))
            {
                search = dir.Files(venueName.Value.ToLower().Replace("the ", "") + ".json");
                if (search.Count() == 1)
                {
                    Results.Add(ReadAndClose<T>(search.First()));
                    return this;
                }
                foreach (var file in search)
                {
                    options.Results.Add(ReadAndClose<T>(file));
                }
            }
            // add the
            search = dir.Files("the " + venueName.Value + ".json");
            if (search.Count() == 1)
            {
                Results.Add(ReadAndClose<T>(search.First()));
                return this;
            }
            foreach (var file in search)
            {
                options.Results.Add(ReadAndClose<T>(file));
            }
            // no spaces
            search = dir.Files().Where(x => x.NameWithoutExtension.Replace(" ", "").ToLower() == venueName.Value.ToLower());
            if (search.Count() == 1)
            {
                Results.Add(ReadAndClose<T>(search.First()));
                return this;
            }
            foreach (var file in search)
            {
                options.Results.Add(ReadAndClose<T>(file));
            }
            // starts with
            search = dir.Files().Where(x => x.NameWithoutExtension.Replace(" ", "").ToLower().StartsWith(venueName.Value.Replace(" ", "").ToLower()));
            if (search.Count() == 1)
            {
                Results.Add(ReadAndClose<T>(search.First()));
                return this;
            }
            foreach (var file in search)
            {
                options.Results.Add(ReadAndClose<T>(file));
            }
            Results = options.Results;
            return this;
        }

        T ReadAndClose<T>(IFile file) where T : new()
        {
            Stream read;
            try
            {
                read = file.OpenRead();
            }
            catch (Exception)
            {
                return new T();
            }
            var result = JsonConvert.DeserializeObject<T>(
                Encoding.UTF8.GetString(read.ReadFully()));
            read.Close();
            return result;
        }
    }
}