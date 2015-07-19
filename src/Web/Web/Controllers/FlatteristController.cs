using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Web.Controllers
{
    public class Compliment
    {
        public string HashKey { get; set; }
        public string Description { get; set; }
    }

    public class FlatteristController : ApiController
    {
        static readonly Random Random = new Random();
        const string ComplimentsUri = @"http://www.flatterist.com/compliments.json";

        public Compliment Get()
        {
            var json = new WebClient().DownloadString(ComplimentsUri);

            dynamic stuff = JsonConvert.DeserializeObject(json);
            var compliments = new List<Compliment>();

            foreach (var blah in stuff.compliments)
            {
                compliments.Add(new Compliment { HashKey = blah[0], Description = blah[1] });
            }

            var index = Random.Next(compliments.Count);

            return compliments[index];
        }
    }
}
