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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Entity;
using System.Data;

namespace InventoryTest
{
    /// <summary>
    /// Interaction logic for OrderingSPUserControl.xaml
    /// </summary>
    public partial class OrderingSPUserControl : UserControl
    {
        private InventoryDBEntities db = new InventoryDBEntities();
        public OrderingSPUserControl()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            DataTable orderitemdt;
            DateTime fromdatetime = DatePicker_From.SelectedDate?? DateTime.Now;
            DateTime todatetime = DatePicker_To.SelectedDate?? DateTime.Now;
            orderitemdt = DALforStoredProc.GetOrderItem(fromdatetime, todatetime);
            var listorderitem = orderitemdt.DefaultView;
            ListView_OrderItems.ItemsSource = listorderitem;
        }

        private void ListView_OrderItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var c=(OrderItem)ListView_OrderItems.SelectedItem;
        }

        private void DatePicker_From_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadData();
        }

        private void DatePicker_To_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadData();
        }
    }
}