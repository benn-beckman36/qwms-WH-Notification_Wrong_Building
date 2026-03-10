using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMS_CommonInfo_Biz
{
    public class Agv_endTask
    {
        public string plant { get; set; }
        public string storage { get; set; }
        public string work_station { get; set; }
        public string task_id { get; set; }
        public string task_type { get; set; }
        public int task_sequence { get; set; }
    }

    public class endTaskResponse
    {
        public int code { get; set; }
        public string message { get; set; }
        public endTaskTaskData data { get; set; }
    }

    public class endTaskTaskData
    {
        public string task_id { get; set; }
        public int task_status { get; set; }
        public int task_sequence { get; set; }
    }
}
