using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Launcher
{
    public class DataQuery : Request
    {
        public static RequestCollection Create(string category, string command, params object[] args)
        {
            var query = new Request();

            query["Attributes"].Add(new NameValuePair("Category", category));
            query["Attributes"].Add(new NameValuePair("Command", command));

            foreach (var item in args)
            {
                query["Parameters"].Add(item);
            }

            return new RequestCollection(query);
        }


        public static RequestCollection Cached(string category, string command, params object[] args)
        {
            var query = new Request();

            query["Attributes"].Add(new NameValuePair("Category", category));
            query["Attributes"].Add(new NameValuePair("Command", command));

            foreach (var item in args)
            {
                query["Parameters"].Add(item);
            }

            return new RequestCollection(query);
        }
    }
}
