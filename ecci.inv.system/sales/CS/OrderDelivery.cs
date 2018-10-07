using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ecci.inv.system.sales.CS
{
    public class OrderDelivery
    {
       // public string orderid { get; set; }
        public string client { get; set; }
        public string date { get; set; }
        public string status { get; set; }
        public string product { get; set; }
        public int quantity { get; set; }
        public decimal amount { get; set; }
        public decimal price { get; set; }
    }
}