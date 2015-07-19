using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Twilio;

namespace Web.Controllers
{
    public class TwilioController : ApiController
    {
        public string Get([FromUri] string phoneNumber, string flatteristId, string flattery)
        {
            const string accountSid = "AC4a8be98fbae816067e0c0517858f56fd";
            const string authToken = "03e552278c505744bb862fa53e24b073";
            const string fromNumber = "+12245883104";

            var client = new TwilioRestClient(accountSid, authToken);

            if (string.IsNullOrEmpty(flattery))
            {


                var call = client.InitiateOutboundCall(
                    fromNumber, // The number of the phone initiating the call
                    string.Format("+{0}", phoneNumber), // The number of the phone receiving call
                    string.Format("http://www.flatterist.com/{0}.mp3", flatteristId)
                    // The URL Twilio will request when the call is answered
                    );

                if (call.RestException == null)
                {
                    return string.Format("Started call: {0}", call.Sid);
                }
                return string.Format("Error: {0}", call.RestException.Message);
            }

            client.SendMessage(fromNumber, phoneNumber, flattery);

            return string.Format("Sent message to {0}", phoneNumber);
        }
    }
}
