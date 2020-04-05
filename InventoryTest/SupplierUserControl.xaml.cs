using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Data.Entity;

namespace InventoryTest
{
    /// <summary>
    /// Interaction logic for SupplierForm.xaml
    /// </summary>
    public partial class SupplierForm : UserControl
    {
        private ConfigureNewEditDeleteApplyCancelButtons buttonsNED;
        private InventoryDBEntities db = new InventoryDBEntities();
        public SupplierForm()
        {
            InitializeComponent();
            buttonsNED = new ConfigureNewEditDeleteApplyCancelButtons(Button_New, Button_Edit, Button_Delete, Button_Apply, Button_Cancel);



            //Action for the Clicked New Button
            buttonsNED.ButtonNew(
                (sender, e) =>
                {
                    Supplier sup = new Supplier();
                    Grid_Details.DataContext = sup;
                });


            //Action for the Clicked Edit Button
            buttonsNED.ButtonEdit(
                (sender, e) =>
                {
                });


            //Action for the Clicked Delete Button
            buttonsNED.ButtonDelete(
                (sender, e) =>
                {
                    Supplier sup = (Supplier)Grid_Details.DataContext;
                    db.Suppliers.Remove(sup);
                });


            //Action for the Apply New Button
            buttonsNED.ActionApplyNew(
                (sender, e) =>
                {
                    var supplier = (Supplier)Grid_Details.DataContext;
                    var entry = db.Entry<Supplier>(supplier);
                    if (entry.State == EntityState.Detached)
                    {
                        db.Suppliers.Add(supplier);
                    }
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (System.Data.Entity.Infrastructure.DbUpdateException)
                    {
                        MessageBox.Show("Cannot Add New Supplier");
                    }
                    LoadData();
                });
            

            //Action for the Apply Edit Button
            buttonsNED.ActionApplyEdit(
                (sender, e) =>
                {
                    var supplier = (Supplier)Grid_Details.DataContext;
                    var entry = db.Entry<Supplier>(supplier);
                    if (entry.State == EntityState.Detached)
                    {
                        db.Suppliers.Add(supplier);
                    }
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (System.Data.Entity.Infrastructure.DbUpdateException)
                    {
                        MessageBox.Show("Cannot Edit Supplier");
                    }
                    LoadData();
                });


            //Action for the Apply Delete Button
            buttonsNED.ActionApplyDelete(
                (sender, e) =>
                {
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (System.Data.Entity.Infrastructure.DbUpdateException)
                    {
                        MessageBox.Show("Cannot Delete Supplier");
                    }
                    LoadData();
                });

            //Action for the Apply Button
            buttonsNED.ActionSetReadOnly(
                () =>
                {
                    tbSupplierName.IsReadOnly = true;
                    tbSupplierAddress.IsReadOnly = true;
                });

            buttonsNED.ActionSetNotReadOnly(
                () =>
                {
                    tbSupplierName.IsReadOnly = false;
                    tbSupplierAddress.IsReadOnly = false;
                });

            LoadData();
        }
        public void LoadData()
        {
            lvSupplier.ItemsSource = db.Suppliers.ToList();
            lvSupplier.SelectedIndex = 0;
        }

        private void setNotReadOnly()
        {
            tbSupplierName.IsReadOnly = false;
            tbSupplierAddress.IsReadOnly = false;
        }

        private void LvSupplier_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Supplier sup = (Supplier)lvSupplier.SelectedItem;
            Grid_Details.DataContext = sup;
        }

    }
}