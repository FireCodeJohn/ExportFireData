using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using ExportFireData.BusinessObject;

namespace ExportFireData.BusinessLogic
{
    public class DataImportManager
    {
        /*DateTime StartingDate, EndingDate;
        Resource<Response> Dataset;
        List<Response> Responses;
        bool searchFinished;

        public DataImportManager(DateTime startingDate, DateTime endingDate)
        {
            this.StartingDate = startingDate;
            this.EndingDate = endingDate;
        }*/

        public static List<Response> GetData_SFRepo_Https(DateTime startTime, DateTime endTime, int offset)
        {
            int limit = 50000;
            string startStamp = string.Format("{0}-{1}-{2}T00:00:00.000", startTime.Year, startTime.Month, startTime.Day);
            string endStamp = string.Format("{0}-{1}-{2}T00:00:00.000", endTime.Year, endTime.Month, endTime.Day);

            string url = string.Format("https://data.sfgov.org/resource/nuek-vuh3.json?$where=call_date >= \"{0}\" AND call_date <= \"{1}\"&$limit={2}&$offset={3}", startStamp, endStamp, limit, offset);
            Console.WriteLine("HTTPS Get Request: " + url);
            Console.WriteLine("");

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers["X-App-Token"] = "U9X7wJc32iQgkkq4uOZN7poE7";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream resStream = response.GetResponseStream();
            string responseBody;

            using (StreamReader streamReader = new StreamReader(resStream))
            {
                responseBody = streamReader.ReadToEnd();
            }

            List<Response> responseList = JsonConvert.DeserializeObject<List<Response>>(responseBody);

            if (responseList.Count == limit)
            {
                responseList.AddRange(GetData_SFRepo_Https(startTime, endTime, offset + limit));
            }

            return responseList;
        }

        /*public List<Response> GetData_SFRepo()
        {
            //try
            //{
                this.Responses = new List<Response>();
                this.searchFinished = false;
                var client = new SodaClient("https://data.sfgov.org", "U9X7wJc32iQgkkq4uOZN7poE7");

            //this.Dataset = client.GetResource<Response>("nuek-vuh3");
            var resource = client.GetResource<Dictionary<string, object>>("nuek-vuh3");

            var soql = new SoqlQuery().Select("call_number").Where("call_number > 200752870");
            
            var results = resource.Query(soql);

            //GetAllRows();
            //var rows = resource.GetRows(1000);

                Console.WriteLine("Got {0} results. Dumping first 10 results:", results.Count());
            /*List<Dictionary<string, object>> list = results.ToList();
                if (list.Count() >= 10)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        Console.WriteLine(list[i]["call_number"]);
                    }
                }
                Console.WriteLine("");**
                return this.Responses;
            //}
            //catch(Exception e)
            //{
             //   Console.WriteLine("Error Encountered Getting Data");
              //  Console.WriteLine(e.Message);
               // Console.WriteLine("");
                //Console.WriteLine("");
                //return null;
           // }
        }

        public void GetAllRows()
        {
            ScanRowsForMatch(0, 6000000, 1000000);
            Console.WriteLine("");
        }

        public void ScanRowsForMatch(int startRow, int endRow, int scanWidth)
        {
            Console.WriteLine("Checking for matches in rows {0} - {1}...", startRow, endRow);
            int rowCount = startRow;

            while (rowCount < endRow && !searchFinished)
            {
                var lastRow = this.Dataset.GetRows(1, rowCount + (scanWidth-1));
                if (lastRow.Count() == 1) // if not last row
                {
                    DateTime lastDate;
                    DateTime.TryParse(lastRow.First().call_date, out lastDate);
                    if (DateTime.Compare(this.EndingDate, lastDate) < 0)
                    {
                        Console.WriteLine("No matches in rows {0} - {1}", rowCount, rowCount + (scanWidth - 1));
                        rowCount += scanWidth;
                        continue;
                    }
                }
                var firstRow = this.Dataset.GetRows(1, rowCount);
                DateTime firstDate;
                DateTime.TryParse(firstRow.First().call_date, out firstDate);
                if (DateTime.Compare(firstDate, this.StartingDate) < 0)
                {
                    this.searchFinished = true;
                    break;
                }

                if (scanWidth > 10000)
                {
                    Console.WriteLine("Found matches between rows {0} and {1}", rowCount, rowCount + scanWidth);
                    ScanRowsForMatch(rowCount, rowCount + scanWidth, scanWidth / 10);
                    //Console.WriteLine("Finished Searching rows {0} - {1}. {2} matches so far", rowCount, rowCount + scanWidth, this.Responses.Count);
                    rowCount += scanWidth;
                }
                else
                {
                    var rows = this.Dataset.GetRows(scanWidth, rowCount).Where(isRespWithinDates);
                    this.Responses.AddRange(rows);
                    Console.WriteLine("Checked rows {0} - {1}. {2} matches", rowCount, rowCount + (scanWidth - 1), rows.Count());          
                    rowCount += scanWidth;
                }
            }

            Console.WriteLine("Finished Searching rows {0} - {1}. {2} matches so far", startRow, endRow, this.Responses.Count);
        }

        public List<Response> filterDates(IEnumerable<Response> rows)
        {
            return rows.Where(isRespWithinDates).ToList();
        }

        public bool isRespWithinDates(Response resp)
        {
            DateTime responseDt;
            if (DateTime.TryParse(resp.call_date, out responseDt))
            {
                if (DateTime.Compare(responseDt, this.StartingDate) < 0 || DateTime.Compare(responseDt, this.EndingDate) > 0)
                    return false;
                else
                    return true;
            }
            else
            {
                Console.WriteLine("call_date not parsed to DateTime");
                return false;
            }
        }

        public IEnumerable<Response> QueryDataset(Resource<Response> dataset)
        {
            var soql = new SoqlQuery().Select("*")
                          .Where("call_date=2000-04-12T00:00:00.000");

            var results = dataset.Query(soql);
            return results;
        }*/
    }
}
