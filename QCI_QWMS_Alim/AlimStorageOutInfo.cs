using Qci.Base.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QCI_QWMS_Alim
{
    public class AlimStorageOutInfo
    {
        public string Step { get; set; }
        public List<AlimList> Alim { get; set; }
    }
    public class AlimList
    {
        public string WERKS { get; set; }
        public string LGORT { get; set; }
        public string GRPID { get; set; }
        public string DIDNO { get; set; }
        public string LOCAT { get; set; }
        public string MBLNR { get; set; }
        public string COSTCENTER { get; set; }
        public string MATNR { get; set; }
        public string MENGE { get; set; }
        public string LIFNR { get; set; }
        public string DACOD { get; set; }
        public string LOCOD { get; set; }
        public string Line { get; set; }
        public string Side { get; set; }
        public string Machine { get; set; }
        public string SDTSlot { get; set; }
        public string SDTLr { get; set; }
    }
}
