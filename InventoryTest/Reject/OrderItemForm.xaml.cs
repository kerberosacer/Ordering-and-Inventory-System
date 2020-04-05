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
    /// Interaction logic for OrderItemForm.xaml
    /// </summary>
    public partial class OrderItemForm : Window
    {
        private InventoryDBEntities db = new InventoryDBEntities();
        private ConfigureNewEditDeleteApplyCancelButtons BehaviorNED;
        public OrderItemForm()
        {
            InitializeComponent();
            LoadData();
            BehaviorNED = new ConfigureNewEditDeleteApplyCancelButtons(Button_NewOrderItem, Button_EditOrderItem, Button_DeleteOrderItem, Button_Apply, Button_Cancel);

            BehaviorNED.ButtonNew((sender, e) =>
            {
                OrderItem neworderitem = new OrderItem();
                neworderitem.DateOrdered = DateTime.Now;
                Grid_Details.DataContext = neworderitem;
            }
            );
            BehaviorNED.ButtonEdit((sender, e) =>
            {

            }
            );
            BehaviorNED.ButtonDelete((sender, e) =>
            {

            }
            );

            BehaviorNED.ActionApplyNew((sender, e) =>
            {
                OrderItem neworderitem = (OrderItem)Grid_Details.DataContext;
                var orderitemstate = db.Entry<OrderItem>(neworderitem);
                if (orderitemstate.State == EntityState.Detached)
                {
                    db.OrderItems.Add(neworderitem);
                }
                try
                {
                    db.SaveChanges();
                    LoadData();
                }
                catch (Exception)
                {
                    MessageBox.Show("Cannot Add New Order");
                }
            }
            );
            BehaviorNED.ActionApplyEdit((sender, e) =>
            {

                try
                {
                    db.SaveChanges();
                    LoadData();
                }
                catch (Exception)
                {
                    MessageBox.Show("Cannot Edit Order");
                }
            }
            );
            BehaviorNED.ActionApplyDelete((sender, e) =>
            {
                OrderItem deleteorderitem = (OrderItem)Grid_Details.DataContext;
                try
                {
                    db.OrderItems.Remove(deleteorderitem);
                    db.SaveChanges();
                    LoadData();
                }
                catch (Exception)
                {
                    MessageBox.Show("Cannot Delete Order");
                }
            }
            );

            BehaviorNED.ActionSetNotReadOnly(() =>
            {
                TextBox_OrderITemID.IsReadOnly = false;
                ComboBox_CustomerID.IsEnabled = true;
                ComboBox_ProductID.IsEnabled = true;
                TextBox_Quantity.IsReadOnly = false;
                TextBox_Discount.IsReadOnly = false;
                DatePicker_DateOrdered.IsEnabled = true;
                TextBox_Notes.IsReadOnly = false;
            }
            );

            BehaviorNED.ActionSetReadOnly(() =>
            {

                TextBox_OrderITemID.IsReadOnly = true;
                ComboBox_CustomerID.IsEnabled = false;
                ComboBox_ProductID.IsEnabled = false;
                TextBox_Quantity.IsReadOnly = true;
                TextBox_Discount.IsReadOnly = true;
                DatePicker_DateOrdered.IsEnabled = false;
                TextBox_Notes.IsReadOnly = true;
            }
            );

        }
        private void LoadData()
        {
            List<OrderItem> listorderitem= db.OrderItems.ToList();
            if (TextBox_sCustomerName.Text== String.Empty && TextBox_sProductName.Text== String.Empty&& TextBox_sSupplierName.Text==String.Empty)
            {
                ListView_OrderItems.ItemsSource = listorderitem;
            }
            else if (TextBox_sProductName.Text == String.Empty && TextBox_sSupplierName.Text == String.Empty)
            {
                //if TextBox_sCustomer has text
                string sCustomerName = TextBox_sCustomerName.Text.ToLower();
                List<OrderItem> orderitemfilteredByCus = listorderitem.Where(c => ((c.Customer.FirstName != null && c.Customer.FirstName.ToLower().Contains(sCustomerName)) || ((c.Customer.LastName != null) && c.Customer.LastName.ToLower().Contains(sCustomerName)))).ToList();
                ListView_OrderItems.ItemsSource = orderitemfilteredByCus;
            }
            else if (TextBox_sCustomerName.Text == String.Empty && TextBox_sProductName.Text == String.Empty)
            {
                //if TextBox_sSupplier has text
                string sSupplierName = TextBox_sSupplierName.Text.ToLower();
                List<OrderItem> orderitemfilteredBySup = listorderitem.Where(c => (c.Product.Supplier.Name != null && c.Product.Supplier.Name.ToLower().Contains(sSupplierName))).ToList();
                ListView_OrderItems.ItemsSource = orderitemfilteredBySup;

            }
            else if (TextBox_sCustomerName.Text == String.Empty && TextBox_sSupplierName.Text == String.Empty)
            {
                //if TextBox_sProduct has text
                string sProductName = TextBox_sProductName.Text.ToLower();
                List<OrderItem> orderitemfilteredByProd = listorderitem.Where(c => (c.Product.Name != null && c.Product.Name.ToLower().Contains(sProductName))).ToList();
                ListView_OrderItems.ItemsSource = orderitemfilteredByProd;
            }
            var customerlist = from c in db.Customers select c;
            ComboBox_CustomerID.ItemsSource = customerlist.ToList();

            var productlist = from c in db.Products select c;
            ComboBox_ProductID.ItemsSource = productlist.ToList();

            ListView_OrderItems.SelectedIndex = 0;

        }

        private void ListView_OrderItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Grid_Details.DataContext=(OrderItem)ListView_OrderItems.SelectedItem;
        }

        private void TextBox_sProductName_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadData();
        }

        private void TextBox_sCustomerName_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadData();
        }

        private void TextBox_sSupplierName_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadData();
        }
    }
}
