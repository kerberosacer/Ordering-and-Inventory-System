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
    /// Interaction logic for UserForm.xaml
    /// </summary>
    public partial class UsersUserControl : UserControl
    {
        private ConfigureNewEditDeleteApplyCancelButtons BehaviorNED;
        private InventoryDBEntities db = new InventoryDBEntities();
        public UsersUserControl()
        {
            InitializeComponent();
            LoadData();
            BehaviorNED = new ConfigureNewEditDeleteApplyCancelButtons(Button_New, Button_Edit, Button_Delete, Button_Apply, Button_Cancel);

            //Behavior when New, Edit, or Delete is clicked
            BehaviorNED.ButtonNew((sender, e) =>
            {
                User newuser = new User();
                Grid_Details.DataContext = newuser;
            });

            BehaviorNED.ButtonEdit((sender, e) =>
            {

            });

            BehaviorNED.ButtonDelete((sender, e) =>
            {

            });

            //Behaviors when Apply is clicked
            BehaviorNED.ActionApplyNew((sender, e) =>
            {
                var newuser = (User)Grid_Details.DataContext;
                newuser.PasswordSalt = InventoryEncryption.GenerateSalt();
                newuser.PasswordHash = InventoryEncryption.ComputeHash(TextBox_Password.Text, newuser.PasswordSalt, 10101, 24);
                var userstate = db.Entry<User>(newuser);
                if (userstate.State == EntityState.Detached)
                {
                    try
                    {
                        db.Users.Add(newuser);
                        db.SaveChanges();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Cannot Add New User");
                    }
                }
                LoadData();

            });
            BehaviorNED.ActionApplyEdit((sender, e) =>
            {
                try
                {
                    db.SaveChanges();

                }
                catch (Exception)
                {
                    MessageBox.Show("Cannot Edit User");
                }
                LoadData();
            });
            BehaviorNED.ActionApplyDelete((sender, e) =>
            {
                var UserToDelete = (User)Grid_Details.DataContext;
                try
                {
                    db.Users.Remove(UserToDelete);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    MessageBox.Show("Cannot Delete User");
                }
                LoadData();
            });

            //BehaviorNED.(() =>
            //{

            //});
            //Defines what is readonly or editable textboxes/comboboxes
            BehaviorNED.ActionSetNotReadOnly(() =>
            {
                TextBox_UserName.IsReadOnly = false;
                TextBox_Password.IsReadOnly = false;
                ComboBox_Role.IsEnabled = true;
            });

            BehaviorNED.ActionSetReadOnly(() =>
            {
                TextBox_UserName.IsReadOnly = true;
                TextBox_Password.IsReadOnly = true;
                ComboBox_Role.IsEnabled = false;
            });

        }

        private void LoadData()
        {
            ListView_Users.ItemsSource = db.Users.ToList();
            ComboBox_Role.ItemsSource = db.Roles.ToList();
        }


        private void ListView_Users_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Grid_Details.DataContext = ListView_Users.SelectedValue;
        }
    }
}
