using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ecci.inv.system.sales.CS
{
    public class OrderDelivery
    {
        public int orderid { get; set; }
        public string client { get; set; }
        public string date { get; set; }
        public string status { get; set; }
        public string product { get; set; }
        public int quantity { get; set; }
        public string amount { get; set; }
        public string price { get; set; }
    }
}