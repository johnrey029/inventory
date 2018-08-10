using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ecci.inv.system.warehouse.CS
{
    public class RawMaterials
    {

        public string purchaseOrder { get; set; }
        public string suppName { get; set; }
        public string brandName { get; set; }
        public int quantity { get; set; }
        //public int dispatch { get; set; }
        //public string purchaseDate { get; set; }
        public string receivedDate { get; set; }
        public string status { get; set; }
        public int id { get; set; }

    }
}