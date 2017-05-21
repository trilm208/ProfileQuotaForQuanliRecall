using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;

namespace Launcher
{
    class HttpDataServices
    {
        public string HttpURL { get; private set; }
        public CookieContainer HttpCookies { get; private set; }
        public string HttpUserAgent { get; private set; }


        public HttpDataServices(string webServiceURL)
        {
            this.HttpURL = webServiceURL;
            this.HttpCookies = new CookieContainer();
            this.HttpUserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; Trident/6.0)";//note this
        }


        public DataSet Execute(RequestCollection requests)
        {
            //var xml = requests.ToXml();
            //var text = HttpPost(xml);
            //var bytes = Convert.FromBase64String(text);
            //var ds = Serializer.Decompress<DataSet>(bytes);
            //var ds = Serializer.FromBinary<DataSet>(bytes);

            var ds = DataAccess.DataProvider.Excute(requests);
            return ds;
        }


        private string HttpPost(string data)
        {
            var request = (HttpWebRequest)HttpWebRequest.Create(HttpURL);
            request.CookieContainer = HttpCookies;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.UserAgent = HttpUserAgent;
            request.AllowAutoRedirect = true;
            //request.Headers.Add("Accept-Encoding", "gzip,deflate");

            using (var writer = new StreamWriter(request.GetRequestStream()))
            {
                if (string.IsNullOrEmpty(data) == false)
                {
                    writer.Write(data);
                }
            }

            var response = (HttpWebResponse)request.GetResponse();

            using (var stream = GetStreamForResponse(response))
            {
                using (var reader = new StreamReader(stream))
                {
                    var result = reader.ReadToEnd();
                    return result;
                }
            }
        }


        static Stream GetStreamForResponse(HttpWebResponse webResponse)
        {
            Stream stream;
            switch (webResponse.ContentEncoding.ToUpperInvariant())
            {
                case "GZIP":
                    stream = new GZipStream(webResponse.GetResponseStream(), CompressionMode.Decompress);
                    break;

                case "DEFLATE":
                    stream = new DeflateStream(webResponse.GetResponseStream(), CompressionMode.Decompress);
                    break;

                default:
                    stream = webResponse.GetResponseStream();
                    break;
            }
            return stream;
        }
    }
}
