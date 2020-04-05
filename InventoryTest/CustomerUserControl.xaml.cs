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
    /// Interaction logic for CustomerForm.xaml
    /// </summary>
    public partial class CustomerUserControl : UserControl
    {
        private InventoryDBEntities db = new InventoryDBEntities();
        private ConfigureNewEditDeleteApplyCancelButtons buttonsNED;
        public CustomerUserControl()
        {
            InitializeComponent();
            buttonsNED = new ConfigureNewEditDeleteApplyCancelButtons(Button_New, Button_Save, Button_Delete, Button_Apply, Button_Cancel);

            buttonsNED.ButtonDelete(
                (sender, e) =>
            {
                var customer = (Customer)Grid_Details.DataContext;
                db.Customers.Remove(customer);
            });


            buttonsNED.ButtonEdit(
                (sender, e) =>
                {

                });
            buttonsNED.ButtonNew(
                (sender, e) =>
                {
                    Customer customer = new Customer();
                    Grid_Details.DataContext = customer;
                });


            buttonsNED.ActionApplyNew(
                (sender, e) =>
                {
                    var customer = (Customer)Grid_Details.DataContext;
                    var entry = db.Entry<Customer>(customer);
                    if (entry.State == EntityState.Detached)
                    {
                        db.Customers.Add(customer);
                    }
                    try
                    {
                        db.SaveChanges();
                        LoadData();
                    }
                    catch (System.Data.Entity.Infrastructure.DbUpdateException)
                    {
                        MessageBox.Show("Cannot Add New Customer");
                    }
                });

            buttonsNED.ActionApplyEdit(
                (sender, e) =>
                {
                    try
                    {
                        db.SaveChanges();
                        LoadData();
                    }
                    catch (System.Data.Entity.Infrastructure.DbUpdateException)
                    {
                        MessageBox.Show("Cannot Edit Customer");
                    }
                });

            buttonsNED.ActionApplyDelete(
                (sender, e) =>
                {
                    try
                    {
                        db.SaveChanges();
                        LoadData();
                    }
                    catch (System.Data.Entity.Infrastructure.DbUpdateException)
                    {
                        MessageBox.Show("Cannot Delete Customer");
                    }
                });


            buttonsNED.ActionSetReadOnly(
                () =>
                {
                    //here goes code for enabling the textboxes for details edit
                    TextBox_FirstName.IsReadOnly = true;
                    TextBox_LastName.IsReadOnly = true;
                    tbAddress.IsReadOnly = TextBox_LastName.IsReadOnly = true;
                    ;
                });


            buttonsNED.ActionSetNotReadOnly(
                () =>
                {
                    //here goes code for enabling the textboxes for details edit
                    TextBox_FirstName.IsReadOnly = false;
                    TextBox_LastName.IsReadOnly = false;
                    tbAddress.IsReadOnly = TextBox_LastName.IsReadOnly = false;
                    ;
                });

            LoadData();
        }

        public void LoadData()
        {
            ListViewCustomer.ItemsSource = db.Customers.ToList();
            ListViewCustomer.SelectedIndex = 0;
        }



        private void ListViewCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Customer customer = (Customer)ListViewCustomer.SelectedItem;
            Grid_Details.DataContext = customer;
        }
    }
}
