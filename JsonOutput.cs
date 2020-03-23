using System;
using System.Collections.Generic;
using System.IO;
using ExportFireData.BusinessObject;

namespace ExportFireData.BusinessLogic
{
    public class JsonOutput
    {
        public static void WriteJsonFiles(DirectoryInfo dir, List<Response> responses, ResponseIncident respInc)
        {
            if (respInc == ResponseIncident.Response)
                WriteResponses(dir, responses, respInc);
            else if (respInc == ResponseIncident.Incident)
                WriteIncidents(dir, responses, respInc);
        }

        public static void WriteResponses(DirectoryInfo dir, List<Response> responses, ResponseIncident respInc)
        {
            int count = 0;
            foreach (Response response in responses)
            {
                //string path = dir.FullName + "\\" + "call_" + response.call_number + ".json";
                string path = Path.Combine(dir.FullName, "call_" + response.call_number + ".json");
                string contents = CreateContents(response, respInc);

                int pathNum = 2;
                while (File.Exists(path))
                {
                    //path = dir.FullName + "\\" + "call_" + response.call_number + "_" + pathNum + ".json";
                    path = Path.Combine(dir.FullName, "call_" + response.call_number + "_" + pathNum + ".json");
                    pathNum++;
                }

                File.WriteAllText(path, contents);
                count++;

                if (count % 10000 == 0)
                {
                    Console.WriteLine("Wrote {0} JSON files so far...", count);
                    Console.WriteLine("");
                }
            }
            Console.WriteLine("Wrote {0} JSON files for {1} responses in the data table", count, count);
            Console.WriteLine("");
        }

        public static void WriteIncidents(DirectoryInfo dir, List<Response> responses, ResponseIncident respInc)
        {
            int count = 0;
            List<string> allFilePaths = new List<string>();
            foreach (Response response in responses)
            {
                //string path = dir.FullName + "\\" + "incident_" + response.incident_number + ".json";
                string path = Path.Combine(dir.FullName, "incident_" + response.incident_number + ".json");
                string rowContents = CreateContents(response, respInc); // Creates content for just the row in the data.

                if (File.Exists(path)) // if the file exists, add the rowContents to the file
                {
                    using (StreamWriter sw = File.AppendText(path))
                    {
                        sw.WriteLine(",");
                        sw.Write(rowContents);
                    }
                }
                else
                {
                    rowContents = "[" + Environment.NewLine + rowContents;
                    File.WriteAllText(path, rowContents);
                    allFilePaths.Add(path);
                    count++;
                }

                if (count % 10000 == 0)
                {
                    Console.WriteLine("Writing {0} JSON files so far...", count);
                    Console.WriteLine("");
                }
            }

            Console.WriteLine("Almost done... finishing up files");
            Console.WriteLine("");
            foreach (string path in allFilePaths)
            {
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine("");
                    sw.Write("]");
                }
            }

            Console.WriteLine("Wrote {0} incident JSON files for {1} responses in the data table", allFilePaths.Count, responses.Count);
            Console.WriteLine("");
        }

        public static string CreateContents(Response res, ResponseIncident respInc)
        {
            string indentBrackets, indentProps, indentLocProps;
            if (respInc == ResponseIncident.Response)
            {
                indentBrackets = ""; // 0 spaces
                indentProps = "    "; // 4 spaces
                indentLocProps = "        "; // 8 spaces
            }
            else
            {
                indentBrackets = "    "; // 4 spaces
                indentProps = "        "; // 8 spaces
                indentLocProps = "            "; // 12 spaces
            }

            string contents = indentBrackets + "{" + Environment.NewLine;

            contents += indentProps + "\"call_number\":\"" + res.call_number + "\"," + Environment.NewLine;
            contents += indentProps + "\"unit_id\":\"" + res.unit_id + "\"," + Environment.NewLine;
            contents += indentProps + "\"incident_number\":\"" + res.incident_number + "\"," + Environment.NewLine;
            contents += indentProps + "\"call_type\":\"" + res.call_type + "\"," + Environment.NewLine;
            contents += indentProps + "\"call_date\":\"" + res.call_date + "\"," + Environment.NewLine; // 5
            contents += indentProps + "\"watch_date\":\"" + res.watch_date + "\"," + Environment.NewLine;
            contents += indentProps + "\"received_dttm\":\"" + res.received_dttm + "\"," + Environment.NewLine;
            contents += indentProps + "\"entry_dttm\":\"" + res.entry_dttm + "\"," + Environment.NewLine;
            contents += indentProps + "\"dispatch_dttm\":\"" + res.dispatch_dttm + "\"," + Environment.NewLine;
            contents += indentProps + "\"response_dttm\":\"" + res.response_dttm + "\"," + Environment.NewLine; // 10
            contents += indentProps + "\"on_scene_dttm\":\"" + res.on_scene_dttm + "\"," + Environment.NewLine;
            contents += indentProps + "\"transport_dttm\":\"" + res.transport_dttm + "\"," + Environment.NewLine;
            contents += indentProps + "\"hospital_dttm\":\"" + res.hospital_dttm + "\"," + Environment.NewLine;
            contents += indentProps + "\"call_final_disposition\":\"" + res.call_final_disposition + "\"," + Environment.NewLine;
            contents += indentProps + "\"available_dttm\":\"" + res.available_dttm + "\"," + Environment.NewLine; // 15
            contents += indentProps + "\"address\":\"" + res.address + "\"," + Environment.NewLine;
            contents += indentProps + "\"city\":\"" + res.city + "\"," + Environment.NewLine;
            contents += indentProps + "\"zipcode_of_incident\":\"" + res.zipcode_of_incident + "\"," + Environment.NewLine;
            contents += indentProps + "\"battalion\":\"" + res.battalion + "\"," + Environment.NewLine;
            contents += indentProps + "\"station_area\":\"" + res.station_area + "\"," + Environment.NewLine; // 20
            contents += indentProps + "\"box\":\"" + res.box + "\"," + Environment.NewLine;
            contents += indentProps + "\"original_priority\":\"" + res.original_priority + "\"," + Environment.NewLine;
            contents += indentProps + "\"priority\":\"" + res.priority + "\"," + Environment.NewLine;
            contents += indentProps + "\"final_priority\":\"" + res.final_priority + "\"," + Environment.NewLine;
            contents += indentProps + "\"als_unit\":\"" + res.als_unit + "\"," + Environment.NewLine; // 25
            contents += indentProps + "\"call_type_group\":\"" + res.call_type_group + "\"," + Environment.NewLine;
            contents += indentProps + "\"number_of_alarms\":\"" + res.number_of_alarms + "\"," + Environment.NewLine;
            contents += indentProps + "\"unit_type\":\"" + res.unit_type + "\"," + Environment.NewLine;
            contents += indentProps + "\"unit_sequence_in_call_dispatch\":\"" + res.unit_sequence_in_call_dispatch + "\"," + Environment.NewLine;
            contents += indentProps + "\"fire_prevention_district\":\"" + res.fire_prevention_district + "\"," + Environment.NewLine; // 30
            contents += indentProps + "\"supervisor_district\":\"" + res.supervisor_district + "\"," + Environment.NewLine;
            contents += indentProps + "\"neighborhoods_analysis_boundaries\":\"" + res.neighborhoods_analysis_boundaries + "\"," + Environment.NewLine;
            contents += CreateLocationString(res.location, indentProps, indentLocProps);
            contents += indentProps + "\"rowid\":\"" + res.rowid + "\"" + Environment.NewLine; // 34

            contents += indentBrackets + "}";            
            return contents;
        }

        public static string CreateLocationString(Location loc, string indentProps, string indentLocProps)
        {
            if (loc.human_address == null)
                loc.human_address = "{\"address\": \"\", \"city\": \"\", \"state\": \"\", \"zip\": \"\"}";

            string human_address_escaped = loc.human_address.Replace("\"", "\\\"");

            string contents = indentProps + "\"location\":{" + Environment.NewLine;
            contents += indentLocProps + "\"latitude\":\"" + loc.latitude + "\"," + Environment.NewLine;
            contents += indentLocProps + "\"longitude\":\"" + loc.longitude + "\"," + Environment.NewLine;
            contents += indentLocProps + "\"human_address\":\"" + human_address_escaped + "\"" + Environment.NewLine;
            contents += indentProps + "}," + Environment.NewLine;
            return contents;
        }
    }
}
