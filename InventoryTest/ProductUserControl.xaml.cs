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

namespace InventoryTest
{
    /// <summary>
    /// Interaction logic for ProductUserControl.xaml
    /// </summary>
    public partial class ProductUserControl : UserControl
    {
        private ConfigureNewEditDeleteApplyCancelButtons ButtonsNED;
        private InventoryDBEntities db = new InventoryDBEntities();
        public ProductUserControl()
        {
            
        InitializeComponent();
        ButtonsNED = new ConfigureNewEditDeleteApplyCancelButtons(Button_New, Button_Edit, Button_Delete, Button_Apply, Button_Cancel);

        //Action for Clicked New Button
        ButtonsNED.ButtonNew(
            (sender, e) =>
            {
                Product newProd = new Product();
                Grid_Details.DataContext = newProd;
            });
        //Action for Clicked Edit Button
        ButtonsNED.ButtonEdit(
            (sender, e) =>
            {

            });
        //Action for Clicked Delete Button
        ButtonsNED.ButtonDelete(
            (sender, e) =>
            {

                Product prod = (Product)Grid_Details.DataContext;

                db.Products.Remove(prod);

            });
        //Action for Apply New
        ButtonsNED.ActionApplyEdit(
            (sender, e) =>
            {
                try
                {
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    MessageBox.Show("Cannot Edit Product");
                }
                LoadData();
            });
        //Action for Apply Edit
        ButtonsNED.ActionApplyNew(
            (sender, e) =>
            {
                Product prod = (Product)Grid_Details.DataContext;
                var current = db.Entry<Product>(prod);
                if (current.State == EntityState.Detached)
                {
                    db.Products.Add(prod);
                }
                try
                {
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    MessageBox.Show("Cannot Add New Product");
                }
                LoadData();
            });

        //Action for Apply Delete
        ButtonsNED.ActionApplyDelete(
            (sender, e) =>
            {

                try
                {
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    MessageBox.Show("Cannot Delete Product");
                    LoadData();
                }

            });

        //Action for Set ReadOnly
        ButtonsNED.ActionSetNotReadOnly(() =>

        {
            ComboBox_Supplier.IsEnabled = true;
            tbName.IsReadOnly = false;
            tbUnitPrice.IsReadOnly = false;
            tbInventoryLevel.IsReadOnly = false;
            ComboBox_SKU.IsEnabled = true;
        });
        ButtonsNED.ActionSetReadOnly(() =>
        {
            ComboBox_Supplier.IsEnabled = false;
            tbName.IsReadOnly = true;
            tbUnitPrice.IsReadOnly = true;
            tbInventoryLevel.IsReadOnly = true;
            ComboBox_SKU.IsEnabled = false;
        });

        LoadData();
    }

    public void LoadData()
    {
        List<Supplier> list = db.Suppliers.ToList();
        ComboBox_Supplier.ItemsSource = list;
        var skus = from c in db.SKUs
                   select new
                   {
                       SKU_ID = c.SKUID,
                       SKUName = c.Name
                   };
        ComboBox_SKU.ItemsSource = skus.ToList();

        List<Product> listProducts = db.Products.ToList();
        if (TextBox_FilterProductID.Text == String.Empty && TextBox_FilterProductName.Text == String.Empty)
        {
            lvProduct.ItemsSource = listProducts;
        }
        else if (TextBox_FilterProductID.Text == String.Empty)
        {
            string sFilterProductName = TextBox_FilterProductName.Text.ToLower();
            lvProduct.ItemsSource = listProducts.Where(c => c.Name != null && (c.Name.ToString().ToLower().StartsWith(sFilterProductName)));
        }
        else if (TextBox_FilterProductName.Text == String.Empty)
        {
            int sFilterProductID;
            Int32.TryParse(TextBox_FilterProductID.Text, out sFilterProductID);
            lvProduct.ItemsSource = listProducts.Where(c => c.ProductID == sFilterProductID);
        }

        lvProduct.SelectedIndex = 0;
    }


    private void LvProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        Grid_Details.DataContext = (Product)lvProduct.SelectedItem;
    }

    private void TextBox_ProductName_TextChanged(object sender, TextChangedEventArgs e)
    {
        LoadData();
    }

    private void TextBox_FilterProductID_TextChanged(object sender, TextChangedEventArgs e)
    {
        LoadData();
    }
}
}