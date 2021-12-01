using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using NeoDisplay;
using Newtonsoft.Json;

namespace ConsoleForTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            string data;
            using (WebClient WebC = new WebClient())
            {
                data = WebC.DownloadString(@"https://api.nasa.gov/neo/rest/v1/neo/browse?api_key=IcRSAo8y9yxcuzf6bRSpQ1kPrpBGwqCpBPGaHlt2");
            }
            List<Near_Earth_Objects> NeoCollection = new List<Near_Earth_Objects>(JsonConvert.DeserializeObject<RootNeoObject>(data).near_earth_objects);
            for (int i = 0; i < NeoCollection.Count; i++)
            {
                Console.WriteLine(NeoCollection[i].name);
            }
            Console.ReadLine();
        }
    }
}
