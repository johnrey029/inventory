using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ecci.inv.system.qualitycontrol.CS
{
    public class Hold
    {
        public string purchaseOrder { get; set; }
        public string suppName { get; set; }
        public string brandName { get; set; }
        public int quantity { get; set; }
        public string holdDate { get; set; }
        public string status { get; set; }
        public int id { get; set; }
    }
}