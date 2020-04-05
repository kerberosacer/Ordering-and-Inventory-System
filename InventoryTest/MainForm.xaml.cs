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
    /// Interaction logic for MainForm.xaml
    /// </summary>
    public partial class MainForm : Window
    {
        InventoryDBEntities db = new InventoryDBEntities();
        public MainForm()
        {
            InitializeComponent();
            this.Closing += MainForm_Closing;
        }

        private void MainForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Ordering_Click(object sender, RoutedEventArgs e)
        {
            this.Grid_ToContainUserControl.Children.Clear();
            var userControl = new OrderItemUserControl();
            this.Grid_ToContainUserControl.Children.Add(userControl);
        }

        private void Button_Products_Click(object sender, RoutedEventArgs e)
        {

            this.Grid_ToContainUserControl.Children.Clear();
            var userControl = new ProductUserControl();
            this.Grid_ToContainUserControl.Children.Add(userControl);
        }

        private void Button_Customers_Click(object sender, RoutedEventArgs e)
        {

            this.Grid_ToContainUserControl.Children.Clear();
            var userControl = new CustomerUserControl();
            this.Grid_ToContainUserControl.Children.Add(userControl);
        }

        private void Button_Suppliers_Click(object sender, RoutedEventArgs e)
        {

            this.Grid_ToContainUserControl.Children.Clear();
            var userControl = new ProductUserControl();
            this.Grid_ToContainUserControl.Children.Add(userControl);
        }

        private void Button_Users_Click(object sender, RoutedEventArgs e)
        {

            this.Grid_ToContainUserControl.Children.Clear();
            var userControl = new UsersUserControl();
            this.Grid_ToContainUserControl.Children.Add(userControl);
        }

        private void Button_Report_Click(object sender, RoutedEventArgs e)
        {

            this.Grid_ToContainUserControl.Children.Clear();
            var userControl = new ReportUserControl();
            this.Grid_ToContainUserControl.Children.Add(userControl);
        }

        private void Button_Logout_Click(object sender, RoutedEventArgs e)
        {
            var loginForm = new LoginForm();
            loginForm.Show();
            MainFormContainer.LoggedUser = null;
            this.Hide();
        }

        private void Button_ReportSP_Click(object sender, RoutedEventArgs e)
        {
            this.Grid_ToContainUserControl.Children.Clear();
            ReportSPUserControl usercontrol = new ReportSPUserControl();
            this.Grid_ToContainUserControl.Children.Add(usercontrol);
        }

        private void Button_OrderingSP_Click(object sender, RoutedEventArgs e)
        {
            this.Grid_ToContainUserControl.Children.Clear();
            var userControl = new OrderingSPUserControl();
            this.Grid_ToContainUserControl.Children.Add(userControl);
        }
    }
}
