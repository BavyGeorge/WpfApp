
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Software_Engineering
{
    /// <summary>
    /// Interaction logic for Login_Screen.xaml
    /// </summary>
    public partial class Login_Screen : Page
    {
        public Login_Screen()
        {
            InitializeComponent();
        }

        //Login Button Click event
        private void LoginButton(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(Properties.Settings.Default.GeneratePath());
            try
            {
                //Login sql strings, commands, and executions
                if (con.State == ConnectionState.Closed)
                con.Open();
                string query = "Select count(1) from Login where Username='" + usrnmlgn.Text + "' and Password= '" + PasswordBoxHidden.Password + "'";
                string IDquery = "Select UserID from Login Where Username='" + usrnmlgn.Text + "' and Password= '" + PasswordBoxHidden.Password + "'";
                SqlCommand sqlCmd = new SqlCommand(query, con);
                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                //Check if login details are correct or not
                if (count == 1)
                {
                    //If correct
                    SqlCommand IDcmd = new SqlCommand(IDquery, con);
                    int UID = (Int32)IDcmd.ExecuteScalar();
                    SearchPage.UserID = UID;
                    this.NavigationService.Navigate(new SearchPage());
                }
                else
                {
                    //If incorrect
                    MessageBox.Show("Username or password is incorrect");
                    PasswordBoxHidden.Password = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        //Naviate to Resitration screen button
        private void RegisterButton(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Registration_Screen());
        }

        //Show Password Check event
        private void ShowPasswordChecked(object sender, RoutedEventArgs e)
        {
            PasswordTextBoxShown.Text = PasswordBoxHidden.Password;
            PasswordBoxHidden.Visibility = Visibility.Collapsed;
            PasswordTextBoxShown.Visibility = Visibility.Visible;
        }
        //Hide Password click event
        private void ShowPasswordUnchecked(object sender, RoutedEventArgs e)
        {
            PasswordBoxHidden.Visibility = Visibility.Visible;
            PasswordTextBoxShown.Visibility = Visibility.Collapsed;
        }
        //Text changed event to match text and password boxes
        private void PasswordTextBoxShown_TextChanged(object sender, TextChangedEventArgs e)
        {
            PasswordBoxHidden.Password = PasswordTextBoxShown.Text;
        }
    }
}