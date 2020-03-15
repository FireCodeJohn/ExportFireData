using System;
using System.Collections.Generic;
using System.Text;
using GeoJSON.Net.Geometry;

namespace ExportFireData.BusinessObject
{
    public class Response
    {
        public string call_number { get; set; }
        public string unit_id { get; set; }
        public string incident_number { get; set; }
        public string call_type { get; set; }
        public string call_date { get; set; } // 5
        public string watch_date { get; set; }
        public string received_dttm { get; set; }
        public string entry_dttm { get; set; }
        public string dispatch_dttm { get; set; }
        public string response_dttm { get; set; } // 10
        public string on_scene_dttm { get; set; }
        public string transport_dttm { get; set; }
        public string hospital_dttm { get; set; }
        public string call_final_disposition { get; set; }
        public string available_dttm { get; set; } // 15
        public string address { get; set; }
        public string city { get; set; }
        public string zipcode_of_incident { get; set; }
        public string battalion { get; set; }
        public string station_area { get; set; } // 20
        public string box { get; set; }
        public string original_priority { get; set; }
        public string priority { get; set; }
        public string final_priority { get; set; }
        public bool   als_unit { get; set; } // 25
        public string call_type_group { get; set; }
        public int    number_of_alarms { get; set; }
        public string unit_type { get; set; }
        public int    unit_sequence_in_call_dispatch { get; set; }
        public string fire_prevention_district { get; set; } // 30
        public string supervisor_district { get; set; }
        public string neighborhoods_analysis_boundaries { get; set; }
        public MyPoint location { get; set; }
        public string rowid { get; set; } // 34
    }

    public class MyPoint
    {
        public string type { get; set; }
        public double[] coordinates { get; set; }
    }
}
