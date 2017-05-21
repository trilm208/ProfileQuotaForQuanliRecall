using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shell
{
    public class PersonSimilarResults
    {
        public Dictionary<String,double> Data = new Dictionary<string,double>();
        internal void Add(Guid persistedFaceId, double conf)
        {
            string id = persistedFaceId.ToString();
            if (Data.ContainsKey(id))
            {
                Data[id] = conf;
            }
            else
            {
                Data.Add(id, conf);
            }
        }
    }
}
