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
using System.Data.Entity;

namespace InventoryTest
{
    /// <summary>
    /// Interaction logic for OrderForm.xaml
    /// </summary>
    public partial class OrderForm : Window
    {
        private List<Customer> customerlist;
        private InventoryDBEntities db = new InventoryDBEntities();
        public OrderForm()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            //var orderlist = from c in db.Orders orderby c.DateOrdered descending select c ;
            //ListView_Order.ItemsSource = orderlist.ToList();
            //ListView_Order.SelectedIndex = 0;

            var customers = from c in db.Customers select c;

            //orderby c.CustomerID descending
            //select new
            //{
            //    FullName = c.FirstName + " " + c.LastName,
            //    Customer_ID = c.CustomerID
            //};
            customerlist = customers.ToList();
            ComboBox_CustomerID.ItemsSource = customerlist;
        }

        private void ListView_Order_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Grid_Details.DataContext=(Order)ListView_Order.SelectedItem;
        }

        private void Button_NewOrderItem_Click(object sender, RoutedEventArgs e)
        {
            ////Order neworder = new Order();
            //neworder.DateOrdered = DateTime.Now;
            //Grid_Details.DataContext = neworder;
        }

        private void Button_SaveOrderItem_Click(object sender, RoutedEventArgs e)
        {
            //Order neworder = (Order)Grid_Details.DataContext;
            //var orderState = db.Entry<Order>(neworder);
            //if(orderState.State==EntityState.Detached)
            //{
            //    db.Orders.Add(neworder);
            //}
            try
            {
                db.SaveChanges();
                LoadData();
            }
            catch(Exception)
            {
                MessageBox.Show("Cannot save order");
            }
        }

        private void Button_DeleteOrderItem_Click(object sender, RoutedEventArgs e)
        {

            //Order deleteorder=(Order)Grid_Details.DataContext;
            //try
            //{
            //    db.Orders.Remove(deleteorder);
            //    db.SaveChanges();
            //}
            //catch(Exception)
            //{
            //    MessageBox.Show("Cannot delete order");
            //}
        }

        private void Button_AddOrderItem_Click(object sender, RoutedEventArgs e)
        {
            //OrderItemForm orderitemWindow = new OrderItemForm((Order)Grid_Details.DataContext);
            //orderitemWindow.ShowDialog();
        }

        private void TextBox_FilterbyOrderID_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (TextBox_FilterbyOrderID.Text != string.Empty)
            //{
            //    var orderlist = from c in db.Orders where c.OrderID.ToString() == TextBox_FilterbyOrderID.Text select c;
            //    ListView_Order.ItemsSource = orderlist.ToList();
            //}
            //else
            //    ListView_Order.ItemsSource = db.Orders.ToList();
        }
    }
}
