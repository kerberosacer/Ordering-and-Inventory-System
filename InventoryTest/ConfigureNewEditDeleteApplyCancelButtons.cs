using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace InventoryTest
{
    public class ConfigureNewEditDeleteApplyCancelButtons
    {
        public Button Button_New { get; set; }
        public Button Button_Edit { get; set; }
        public Button Button_Delete { get; set; }
        public Button Button_Apply { get; set; }
        public Button Button_Cancel { get; set; }
        private Action<object, RoutedEventArgs> Action_New { get; set; }
        private Action<object, RoutedEventArgs> Action_Delete { get; set; }
        private Action<object, RoutedEventArgs> Action_Edit { get; set; }


        private Action<object, RoutedEventArgs> Action_ApplyEdit { get; set; }
        private Action<object, RoutedEventArgs> Action_ApplyNew { get; set; }
        private Action<object, RoutedEventArgs> Action_ApplyDelete { get; set; }
        private Action Action_SetReadOnly { get; set; }
        private Action Action_SetNotReadOnly { get; set; }

        private Button CurrentButton;
        public ConfigureNewEditDeleteApplyCancelButtons(Button Button_new, Button Button_edit, Button Button_delete, Button Button_apply, Button Button_cancel)
        {
            Button_New = Button_new;
            Button_Edit = Button_edit;
            Button_Delete = Button_delete;
            Button_Apply = Button_apply;
            Button_Cancel = Button_cancel;
            SetupBehaviour();
            ClickedApplyCancel();
            CurrentButton = null;
            SetReadOnly();
        }
        public void ButtonDelete(Action<object, RoutedEventArgs> actionDelete)
        {
            Action_Delete = actionDelete;
        }
        public void ButtonNew(Action<object, RoutedEventArgs> actionNew)
        {
            Action_New = actionNew;
        }
        public void ButtonEdit(Action<object, RoutedEventArgs> actionEdit)
        {
            Action_Edit = actionEdit;
        }

        public void ActionApplyNew(Action<object, RoutedEventArgs> actionApplyNew)
        {
            Action_ApplyNew = actionApplyNew;
        }
        public void ActionApplyEdit(Action<object, RoutedEventArgs> actionApplyEdit)
        {
            Action_ApplyEdit = actionApplyEdit;
        }
        public void ActionApplyDelete(Action<object, RoutedEventArgs> actionApplyDelete)
        {
            Action_ApplyDelete = actionApplyDelete;
        }
        public void ActionSetReadOnly(Action setNoEdit)
        {
            Action_SetReadOnly = setNoEdit;
        }
        public void ActionSetNotReadOnly(Action setEditable)
        {
            Action_SetNotReadOnly = setEditable;
        }
        public void SetupBehaviour()
        {
            if (Button_Delete != null)
            {
                Button_Delete.Click -= Button_Delete_Click;
                Button_Delete.Click += Button_Delete_Click;
            }
            if (Button_Edit != null)
            {
                Button_Edit.Click -= Button_Edit_Click;
                Button_Edit.Click += Button_Edit_Click;
            }
            if (Button_New != null)
            {
                Button_New.Click -= Button_New_Click;
                Button_New.Click += Button_New_Click;
            }
            if (Button_Apply != null)
            {
                Button_Apply.Click -= Button_Apply_Click;
                Button_Apply.Click += Button_Apply_Click;
            }
            if (Button_Cancel != null)
            {
                Button_Cancel.Click -= Button_Cancel_Click;
                Button_Cancel.Click += Button_Cancel_Click;

            }
        }

        private void Button_New_Click(object sender, RoutedEventArgs e)
        {
            ClickedNewEditDelete();
            SetNotReadOnly();
            if (Action_New != null)
                Action_New.Invoke(sender, e);

            //saves current button
            CurrentButton = ((Button)sender);
        }
        private void Button_Edit_Click(object sender, RoutedEventArgs e)
        {
            ClickedNewEditDelete();
            SetNotReadOnly();
            if (Action_Edit != null)
                Action_Edit.Invoke(sender, e);
            //saves current button
            CurrentButton = ((Button)sender);
        }
        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            ClickedNewEditDelete();

            if (Action_Delete != null)
                Action_Delete.Invoke(sender, e);

            //saves current button
            CurrentButton = ((Button)sender);
        }
        private void Button_Apply_Click(object sender, RoutedEventArgs e)
        {

            if (CurrentButton==Button_Edit)
            {

                if (Button_Apply != null)
                {
                    Action_ApplyEdit.Invoke(sender, e);
                    ClickedApplyCancel();
                }

            }


            if (CurrentButton ==Button_New)
            {

                if (Button_Apply != null)
                {
                    Action_ApplyNew.Invoke(sender, e);
                    ClickedApplyCancel();
                }

            }

            if (CurrentButton == Button_Delete)
            {

                if (Button_Apply != null)
                {
                    Action_ApplyDelete.Invoke(sender, e);
                    ClickedApplyCancel();
                }
            }


            //last line to set read only details textboxes
            SetReadOnly();
        }
        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            SetReadOnly();
            ClickedApplyCancel();
        }
        //sets details text box to read only
        private void SetReadOnly()
        {
            if (Action_SetReadOnly != null)
                Action_SetReadOnly.Invoke();
        }

        //sets details text box to edit mode
        private void SetNotReadOnly()
        {
            if (Action_SetNotReadOnly != null)
                Action_SetNotReadOnly.Invoke();
        }
        private void ClickedNewEditDelete()
        {
            //enabled property
            Button_Edit.Visibility = Visibility.Collapsed;
            Button_New.Visibility = Visibility.Collapsed;
            Button_Delete.Visibility = Visibility.Collapsed;


            //visibility property
            Button_Apply.Visibility = Visibility.Visible;
            Button_Cancel.Visibility = Visibility.Visible;


        }
        private void ClickedApplyCancel()
        {
            //enabled property
            Button_Edit.Visibility = Visibility.Visible;
            Button_New.Visibility = Visibility.Visible;
            Button_Delete.Visibility = Visibility.Visible;


            //visibility property
            Button_Apply.Visibility = Visibility.Collapsed;
            Button_Cancel.Visibility = Visibility.Collapsed;

        }
    }
}
