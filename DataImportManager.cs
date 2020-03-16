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
        DateTime StartingDate, EndingDate;
        Resource<Response> Dataset;
        List<Response> Responses;
        bool searchFinished;

        public DataImportManager(DateTime startingDate, DateTime endingDate)
        {
            this.StartingDate = startingDate;
            this.EndingDate = endingDate;
        }

        public List<Response> GetData_SFRepo()
        {
            try
            {
                this.Responses = new List<Response>();
                this.searchFinished = false;
                var client = new SodaClient("https://data.sfgov.org", "U9X7wJc32iQgkkq4uOZN7poE7");
                
                //var rows = client.Query<Response>(query, "nuek-vuh3");
                this.Dataset = client.GetResource<Response>("nuek-vuh3");

                //var results = QueryDataset(dataset);
                // Resource objects read their own data
            

                //var rows = dataset.GetRows(limit: 5000).Where(isRespWithinDates);
                //List<Response> filteredRows = filterDates(rows);
                GetAllRows();

                Console.WriteLine("Got {0} results. Dumping first 10 results:", this.Responses.Count());
                //Console.WriteLine("Got results");
                if (this.Responses.Count >= 10)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        Console.WriteLine(this.Responses[i].call_number);
                    }
                }
                Console.WriteLine("");
                return this.Responses;
            }
            catch(Exception e)
            {
                Console.WriteLine("Error Encountered Getting Data");
                Console.WriteLine(e.Message);
                Console.WriteLine("");
                Console.WriteLine("");
                return null;
            }
        }

        public void GetAllRows()
        {
            ScanRowsForMatch(0, 6000000, 1000000);
            Console.WriteLine("");
            /*while (rowCount < rowsToScan)
            {
                var lastRow = this.Dataset.GetRows(1, rowCount + (scanWidth - 1));
                if (lastRow.Count() == 1) // if not last row
                {
                    DateTime firstDate, lastDate;                   
                    DateTime.TryParse(lastRow.First().call_date, out lastDate);
                    if (DateTime.Compare(this.EndingDate, lastDate) < 0)
                    {
                        Console.WriteLine("Checked rows {0} - {1}. {2} matches so far", rowCount, rowCount + (scanWidth - 1), responses.Count);
                        rowCount += scanWidth;
                        continue;
                    }

                    var firstRow = this.Dataset.GetRows(1, rowCount);
                    DateTime.TryParse(firstRow.First().call_date, out firstDate);
                    if (DateTime.Compare(firstDate, this.StartingDate) < 0)
                    {
                        Console.WriteLine("Finished Searching...");
                        return responses;
                    }
                }

                var rows = this.Dataset.GetRows(scanWidth, rowCount).Where(isRespWithinDates);
                responses.AddRange(rows);
                Console.WriteLine("Checked rows {0} - {1}. {2} matches so far", rowCount, rowCount + (scanWidth - 1), responses.Count);
                rowCount += scanWidth;
            }
            return responses;*/
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
        }
    }
}
