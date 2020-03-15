using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;
using SODA;
using Newtonsoft.Json;
using ExportFireData.BusinessObject;

namespace ExportFireData.BusinessLogic
{
    public class DataImportManager
    {
        public static void GetData_SFRepo()
        {
            try
            {
                var client = new SodaClient("https://data.sfgov.org", "U9X7wJc32iQgkkq4uOZN7poE7");

                // Get a reference to the resource itself
                // The result (a Resouce object) is a generic type
                // The type parameter represents the underlying rows of the resource
                // and can be any JSON-serializable class
                var dataset = client.GetResource<Response>("nuek-vuh3");

                // Resource objects read their own data
                var rows = dataset.GetRows(10);

                Console.WriteLine("Got {0} results. Dumping first results:", rows.Count());

                foreach (Response response in rows)
                {
                    Console.WriteLine(response.call_number);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
