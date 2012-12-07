using System.Net;
using System.Text;
using blackpoolgigs.web.Models;
using blackpoolgigs.web.Services.Interfaces;
using coolbunny.common.Extensions;
using Newtonsoft.Json;

namespace blackpoolgigs.web.Services
{
    public class SayThanks : ISayThanks
    {
        public ThanksImageInfo Gimme()
        {
            var request = (HttpWebRequest)WebRequest.Create("http://www.rhmg-files.co.uk/wastedworld/random");
            request.Accept = "application/json";

            var response = request.GetResponse();
            return JsonConvert.DeserializeObject<ThanksImageInfo>(Encoding.UTF8.GetString(response.GetResponseStream().ReadFully()));
        }
    }
}