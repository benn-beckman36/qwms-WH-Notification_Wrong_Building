using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMS_CommonInfo_Biz
{
    public class Agv_shelfMaterialRenewal
    {
        public string plant { get; set; }
        public string storage { get; set; }
        public string shelf_id { get; set; }
        public string action { get; set; }
        public string location { get; set; }
        public List<LocationDetailItem> location_detail { get; set; }
    }

    public class LocationDetailItem
    {
        public string pn { get; set; }
        public string version { get; set; }
        public string vendor_code { get; set; }
        public string date_code { get; set; }
        public int quantity { get; set; }
    }

    public class shelfMaterialRenewalResponse
    {
        public int code { get; set; }
        public string message { get; set; }
        public string data { get; set; }
    }
}
