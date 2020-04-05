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
    /// Interaction logic for ReportSPUserControl.xaml
    /// </summary>
    public partial class ReportSPUserControl : UserControl
    {
        public ReportSPUserControl()
        {
            InitializeComponent();
            LoadData();
        }



        private InventoryDBEntities db = new InventoryDBEntities();
        private void LoadData()
        {
            var listInventoryReport = DALforStoredProc.GetInventoryReport();
            ListView_ReportScreen.ItemsSource = listInventoryReport.DefaultView;
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

        private void ListView_ReportScreen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            //var selectedItem = (InventoryReport)ListView_ReportScreen.SelectedItem;
            //LoadData();
        }


        private void ResetInventoryListClick(object sender, RoutedEventArgs e)
        {
            //var buttonresetinvenotry = (Button)sender;
            //var item = (InventoryReport)buttonresetinvenotry.DataContext;
            //DALforStoredProc.ResetInventoryLevel(item.ProductID);
            //db = new InventoryDBEntities();
            //LoadData();
        }
    }
}

