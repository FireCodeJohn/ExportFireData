using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ExportFireData.BusinessObject;

namespace ExportFireData.BusinessLogic
{
    public class JsonOutput
    {
        public static void WriteJsonFiles(DirectoryInfo dir, List<Response> responses)
        {
            int count = 0;
            foreach (Response response in responses)
            {
                string path = dir.FullName + "\\" + "call_" + response.call_number + ".json";
                string contents = CreateContents(response);

                int pathNum = 2;
                while (File.Exists(path))
                {
                    path = dir.FullName + "\\" + "call_" + response.call_number + "_" + pathNum + ".json";
                    pathNum++;
                }

                File.WriteAllText(path, contents);
                count++;
            }
            Console.WriteLine("Wrote JSON files for {0} Responses", count);
        }

        public static string CreateContents(Response res)
        {
            string contents = "{" + Environment.NewLine;

            contents += "\"call_number\":\"" + res.call_number + "\"," + Environment.NewLine;
            contents += "\"unit_id\":\"" + res.unit_id + "\"," + Environment.NewLine;
            contents += "\"incident_number\":\"" + res.incident_number + "\"," + Environment.NewLine;
            contents += "\"call_type\":\"" + res.call_type + "\"," + Environment.NewLine;
            contents += "\"call_date\":\"" + res.call_date + "\"," + Environment.NewLine; // 5
            contents += "\"watch_date\":\"" + res.watch_date + "\"," + Environment.NewLine;
            contents += "\"received_dttm\":\"" + res.received_dttm + "\"," + Environment.NewLine;
            contents += "\"entry_dttm\":\"" + res.entry_dttm + "\"," + Environment.NewLine;
            contents += "\"dispatch_dttm\":\"" + res.dispatch_dttm + "\"," + Environment.NewLine;
            contents += "\"response_dttm\":\"" + res.response_dttm + "\"," + Environment.NewLine; // 10
            contents += "\"on_scene_dttm\":\"" + res.on_scene_dttm + "\"," + Environment.NewLine;
            contents += "\"transport_dttm\":\"" + res.transport_dttm + "\"," + Environment.NewLine;
            contents += "\"hospital_dttm\":\"" + res.hospital_dttm + "\"," + Environment.NewLine;
            contents += "\"call_final_disposition\":\"" + res.call_final_disposition + "\"," + Environment.NewLine;
            contents += "\"available_dttm\":\"" + res.available_dttm + "\"," + Environment.NewLine; // 15
            contents += "\"address\":\"" + res.address + "\"," + Environment.NewLine;
            contents += "\"city\":\"" + res.city + "\"," + Environment.NewLine;
            contents += "\"zipcode_of_incident\":\"" + res.zipcode_of_incident + "\"," + Environment.NewLine;
            contents += "\"battalion\":\"" + res.battalion + "\"," + Environment.NewLine;
            contents += "\"station_area\":\"" + res.station_area + "\"," + Environment.NewLine; // 20
            contents += "\"box\":\"" + res.box + "\"," + Environment.NewLine;
            contents += "\"original_priority\":\"" + res.original_priority + "\"," + Environment.NewLine;
            contents += "\"priority\":\"" + res.priority + "\"," + Environment.NewLine;
            contents += "\"final_priority\":\"" + res.final_priority + "\"," + Environment.NewLine;
            contents += "\"als_unit\":\"" + res.als_unit + "\"," + Environment.NewLine; // 25
            contents += "\"call_type_group\":\"" + res.call_type_group + "\"," + Environment.NewLine;
            contents += "\"number_of_alarms\":\"" + res.number_of_alarms + "\"," + Environment.NewLine;
            contents += "\"unit_type\":\"" + res.unit_type + "\"," + Environment.NewLine;
            contents += "\"unit_sequence_in_call_dispatch\":\"" + res.unit_sequence_in_call_dispatch + "\"," + Environment.NewLine;
            contents += "\"fire_prevention_district\":\"" + res.fire_prevention_district + "\"," + Environment.NewLine; // 30
            contents += "\"supervisor_district\":\"" + res.supervisor_district + "\"," + Environment.NewLine;
            contents += "\"neighborhoods_analysis_boundaries\":\"" + res.neighborhoods_analysis_boundaries + "\"," + Environment.NewLine;
            contents += CreateLocationString(res.location);
            contents += "\"rowid\":\"" + res.rowid + "\"" + Environment.NewLine; // 34

            contents += "}";            
            return contents;
        }

        public static string CreateLocationString(Location loc)
        {
            string contents = "\"location\":{" + Environment.NewLine;
            contents += "\"latitude\":\"" + loc.latitude + "\"," + Environment.NewLine;
            contents += "\"longitude\":\"" + loc.latitude + "\"" + Environment.NewLine;
            contents += "}," + Environment.NewLine;
            return contents;
        }
    }
}
