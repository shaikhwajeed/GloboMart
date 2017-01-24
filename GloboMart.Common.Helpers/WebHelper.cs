using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GloboMart.Common.Helpers
{
    public static class WebHelper
    {
        public static string PostRequest(string url, string data)
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("content-type", "application/json");
                // Caller is expected to handle web exceptions.
                var result = client.UploadString(url, data);
                return result;
            }
        }

        public static string GetRequest(string url)
        {
            using (WebClient client = new WebClient())
            {
                // Caller is expected to handle web exceptions.
                var result = client.DownloadString(url);
                return result;
            }
        }
        
        /// <summary>
        /// Calls delete API. 
        /// </summary>
        /// <param name="url">The end point url for delete. Id is supposed to be passed along as query string.</param>
        /// <returns></returns>
        public static bool Delete(string url)
        {
            using (WebClient client = new WebClient())
            {
                client.UploadValues(url, "DELETE", new NameValueCollection());
                return true;
            }
        }
        
    }
}
