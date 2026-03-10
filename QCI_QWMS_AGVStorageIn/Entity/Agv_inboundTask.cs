using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMS_CommonInfo_Biz
{
    public class Agv_inboundTask
    {
        public string plant { get; set; }
        public string storage { get; set; }
        public string work_station { get; set; }
        public string doc_type { get; set; }
        public string shelf_size { get; set; }
        public string shelf_type { get; set; }
        public string task_id { get; set; }
        public string task_type { get; set; }
        public int task_sequence { get; set; }
        public string priority { get; set; }
    }

    public class inboundTaskResponse
    {
        public int code { get; set; }
        public string message { get; set; }
        public inboundTaskTaskData data { get; set; }
    }

    public class inboundTaskTaskData
    {
        public string task_id { get; set; }
        public int task_status { get; set; }
        public int task_sequence { get; set; }
    }
}
