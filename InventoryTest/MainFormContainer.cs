using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InventoryTest
{
    public static class MainFormContainer
    {
        public static MainForm MainFormMember;

        public static User LoggedUser;
        public static void RefreshLoggedUser(User loggednew)
        {
            if(MainFormMember!=null)
            {
                LoggedUser = loggednew;
                foreach (FrameworkElement element in MainFormMember.Menu.Children)
                {
                    element.Visibility = Visibility.Collapsed;
                }
                foreach (string c in LoggedUser.Role.ScreenAccess.Split('_'))
                {
                    foreach (FrameworkElement element in MainFormMember.Menu.Children)
                    {
                        if (element.Name.ToLower().Contains(c.ToLower())|| element.Name.ToLower().Contains("welcome")|| element.Name.ToLower().Contains("textblock_user") || element.Name.ToLower().Contains("logout")) element.Visibility = Visibility.Visible;
                    }
                }
                MainFormMember.TextBlock_User.Text = LoggedUser.Username;
            }
        }
    }
}
