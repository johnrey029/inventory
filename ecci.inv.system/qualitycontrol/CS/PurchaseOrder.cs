using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ecci.inv.system.qualitycontrol.CS
{
    public class OrderDelivery
    {
        public string purchaseOrder { get; set; }
        public string suppName { get; set; }
        public string brandName { get; set; }
        public int quantity { get; set; }
        public string purchaseDate { get; set; }
        public string deliverDate { get; set; }
        public string poStatus { get; set; }
    }
}