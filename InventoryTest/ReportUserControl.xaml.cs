using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace InventoryTest
{
    /// <summary>
    /// Interaction logic for ReportUserControl.xaml
    /// </summary>
    public partial class ReportUserControl : UserControl
    {
        public ReportUserControl()
        {
            InitializeComponent();
            LoadData();
        }



        private InventoryDBEntities db = new InventoryDBEntities();
                private void LoadData()
        {

            List<Product> listProducts = db.Products.ToList();


            var listInventoryReport = listProducts.Select(p => new InventoryReport()
            {
                ProductName = p.Name,
                ProductID = p.ProductID,
                SupplierName = p.Supplier != null ? p.Supplier.Name : string.Empty,
                SoldQuantity = p.OrderItems.Sum(c => c.Quantity.Value),
                CustomerCount = p.OrderItems.Count(c => c.Customer != null),
                BeginningInventory_Level = p.InventoryLevel.Value,
                Inventory_Level = p.InventoryLevel.Value - p.OrderItems.Sum(c => c.Quantity.Value),
            });
            if (TextBox_ProductID.Text == String.Empty && TextBox_ProductName.Text == String.Empty)
            {
                ListView_ReportScreen.ItemsSource = listInventoryReport;
            }
            else if (TextBox_ProductID.Text == String.Empty)
            {
                string sFilterProductName = TextBox_ProductName.Text.ToLower();
                var listInventoryReportByProdName = listInventoryReport.Where(p => p.ProductName != null && p.ProductName.ToLower().StartsWith(sFilterProductName)).ToList();
                ListView_ReportScreen.ItemsSource = listInventoryReportByProdName;
            }
            else if (TextBox_ProductName.Text == String.Empty)
            {
                int sFilterProductID;
                Int32.TryParse(TextBox_ProductID.Text, out sFilterProductID);
                var listInventoryReportByProdID = listInventoryReport.Where(c => c.ProductID == sFilterProductID);
                ListView_ReportScreen.ItemsSource = listInventoryReportByProdID;
            }
        }

        private void BtnOrders_Click(object sender, RoutedEventArgs e)
        {
            //OrderItemForm orderitemWindow = new OrderItemForm();
            //orderitemWindow.ShowDialog();
            LoadData();
        }

        private void BtnProducts_Click(object sender, RoutedEventArgs e)
        {
           //ProductForm productWindow = new ProductForm();
            //productWindow.Show();
            LoadData();
        }

        private void BtnCustomers_Click(object sender, RoutedEventArgs e)
        {
            CustomerUserControl customerWindow = new CustomerUserControl();
            //customerWindow.Show();
            LoadData();
        }

        private void BtnSuppliers_Click(object sender, RoutedEventArgs e)
        {
            SupplierForm supplierWindow = new SupplierForm();
            //supplierWindow.Show();
            LoadData();
        }

        private void Button_OrderItem_Click(object sender, RoutedEventArgs e)
        {
            //OrderItemForm orderWindow = new OrderItemForm();
            //orderWindow.Show();
            LoadData();
        }

        private void Button_ExportToExcel_Click(object sender, RoutedEventArgs e)
        {

            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Workbooks.Add();

            Microsoft.Office.Interop.Excel._Worksheet WorkSheet = excel.ActiveSheet;

            var ListViewReportSource = ListView_ReportScreen.ItemsSource;

            try
            {
                WorkSheet.Cells[1, "A"] = "Product Name";
                WorkSheet.Cells[1, "B"] = "Supplier";
                WorkSheet.Cells[1, "C"] = "Customer Count";
                WorkSheet.Cells[1, "D"] = "Sold Quantity";
                WorkSheet.Cells[1, "E"] = "Current Inventory Level";
                WorkSheet.Cells[1, "F"] = "Beginning Inventory Level";

                int row = 2;
                foreach (var item in ListViewReportSource)
                {
                    WorkSheet.Cells[row, "A"] = ((InventoryReport)item).ProductName;
                    WorkSheet.Cells[row, "B"] = ((InventoryReport)item).SupplierName;
                    WorkSheet.Cells[row, "C"] = ((InventoryReport)item).CustomerCount;
                    WorkSheet.Cells[row, "D"] = ((InventoryReport)item).SoldQuantity;
                    WorkSheet.Cells[row, "E"] = ((InventoryReport)item).Inventory_Level;
                    WorkSheet.Cells[row, "F"] = ((InventoryReport)item).BeginningInventory_Level;
                    row++;
                }

                //Save Dialog for exporting excel
                SaveFileDialog savefiledialog = new SaveFileDialog();
                savefiledialog.RestoreDirectory = true;
                savefiledialog.DefaultExt = ".xlsx";
                savefiledialog.Filter = "Excel File|*.xlsx";
                bool selectedSaveLoc = savefiledialog.ShowDialog().Value;
                string filename = String.Empty;
                if (selectedSaveLoc)
                {
                    filename = savefiledialog.FileName;
                }

                WorkSheet.SaveAs(filename);
                MessageBox.Show("Success");

                Marshal.ReleaseComObject(WorkSheet);
                Marshal.ReleaseComObject(excel);
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            catch (Exception)
            {

            }

        }

        private void ListView_ReportScreen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var selectedItem = (InventoryReport)ListView_ReportScreen.SelectedItem;
            //ProductToCustomerReport ProdToCustomerReportForm = new ProductToCustomerReport(db, selectedItem);
            //ProdToCustomerReportForm.ShowDialog();
            LoadData();
        }

        private void TextBox_ProductName_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadData();
        }

        private void TextBox_ProductID_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadData();
        }

        private void BtnUsers_Click(object sender, RoutedEventArgs e)
        {
            //UserForm userform = new UserForm();
            //userform.Show();
        }

        private void ResetInventoryListClick(object sender, RoutedEventArgs e)
        {
            var buttonresetinvenotry = (Button)sender;
            var item = (InventoryReport)buttonresetinvenotry.DataContext;
            DALforStoredProc.ResetInventoryLevel(item.ProductID);
            db = new InventoryDBEntities();
            LoadData();
        }
    }
}
