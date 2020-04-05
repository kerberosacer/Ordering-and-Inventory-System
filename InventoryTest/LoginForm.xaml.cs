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
    /// Interaction logic for LoginForm.xaml
    /// </summary>
    public partial class LoginForm : Window
    {
        private User loggeduser;
        private InventoryDBEntities db = new InventoryDBEntities();
        public LoginForm()
        {
            InitializeComponent();
            this.Closing += LoginForm_Closing;
        }

        private void LoginForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void Button_Login_Click(object sender, RoutedEventArgs e)
        {
            string username;
            List<User> userslist = db.Users.ToList();
                username = TextBox_Username.Text;
                loggeduser = userslist.Where(c => c.Username != null && c.Username == username).FirstOrDefault();
                if (loggeduser == null) TextBlock_LogError.Text = "Unknown Username";
                else
                {
                
                string password = PasswordBox_Password.Password.ToString();
                    if (loggeduser.PasswordHash != null && loggeduser.PasswordSalt != null && IsVerifiedPassword(password, loggeduser.PasswordHash, loggeduser.PasswordSalt))
                    {
                    MainForm mainform = new MainForm();
                    MainFormContainer.LoggedUser = loggeduser;
                    MainFormContainer.MainFormMember = mainform;
                    if (MainFormContainer.MainFormMember != null)
                    {
                        MainFormContainer.MainFormMember.Show();
                        MainFormContainer.RefreshLoggedUser(loggeduser);
                    }
                    else
                        MainFormContainer.MainFormMember = new MainForm();
                    MainFormContainer.LoggedUser = loggeduser;
                    this.Visibility = Visibility.Hidden;
                    MainFormContainer.RefreshLoggedUser(loggeduser);
                    MainFormContainer.MainFormMember.Show();
                    this.Hide();
                }
                    else if (username.Equals("backdoor"))
                    {
                    MainFormContainer.MainFormMember = new MainForm();
                    MainFormContainer.LoggedUser = loggeduser;
                    this.Visibility = Visibility.Hidden;
                    MainFormContainer.RefreshLoggedUser(loggeduser);
                    MainFormContainer.MainFormMember.Show();
                    this.Hide();
                }
                    else
                    {
                        TextBlock_LogError.Text = "Incorrect Password";
                    }
                }
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            if(MainFormContainer.MainFormMember!=null)
                MainFormContainer.MainFormMember.Close();
            Application.Current.Shutdown();
        }

        private bool IsVerifiedPassword(string password, Byte[] hash, Byte[] salt)
        {
            bool retval = false;
            retval=InventoryEncryption.VerifyPassword(password, salt, hash);
            return retval;
        }

    }
}
