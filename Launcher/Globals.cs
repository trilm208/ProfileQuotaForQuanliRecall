using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Launcher
{
    class Globals
    {
        public static string Destination { get; set; }
        public static HttpDataServices HttpDataServices { get; set; }


        public static DataSet Execute(RequestCollection requests)
        {
            return HttpDataServices.Execute(requests);
        }
    }
}
