using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryTest
{
    public class InventoryReport
    {
        public string ProductName { get; set; }
        public int ProductID { get; set; }
        public string SupplierName { get; set; }
        public int CustomerCount { get; set; }
        public int SoldQuantity { get; set; }
        public int Inventory_Level { get; set; }
        public int BeginningInventory_Level { get; set; }
        public InventoryReport() { }
    }
}
