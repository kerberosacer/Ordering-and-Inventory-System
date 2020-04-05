using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InventoryTest
{
    /// <summary>
    /// Interaction logic for ProductToCustomerReport.xaml
    /// </summary>
    public partial class ProductToCustomerReport : Window
    {
        private InventoryDBEntities db;
        ListView listItem;
        private InventoryReport productItem;
        public ProductToCustomerReport(InventoryDBEntities dB, InventoryReport item)
        {
            InitializeComponent();
            db = dB;
            productItem = item;
            LoadData();
        }

        private void LoadData()
        {
            int productID = productItem != null ? productItem.ProductID : 0;
            var listOrderItemToCustomer = db.OrderItems.Where(p => p.ProductID == productID).ToList();

            var ProductToCustomerDrillDown = listOrderItemToCustomer.Select(c =>
                        new
                        {
                            ProductName = c.Product.Name,
                            CustomerOrderCount = c.Quantity,
                            CustomerName = String.Format("{0} {1}",c.Customer.FirstName, c.Customer.LastName),
                            OrderDate=c.DateOrdered
                        });
           ListView_Details.ItemsSource = ProductToCustomerDrillDown;

        }
    }
}
