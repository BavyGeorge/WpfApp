using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
//The following three lines are used to enable sql and the default string
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace Software_Engineering
{
    /// <summary>
    /// Interaction logic for Registration_Screen.xaml
    /// </summary>
    public partial class Registration_Screen : Page
    {
        public Registration_Screen()
        {
            InitializeComponent();
            MessageBox.Show(GeneratePath());
        }

        public string GeneratePath()
        {
            System.IO.DirectoryInfo myDirectory = new DirectoryInfo(Environment.CurrentDirectory);
            string parentDirectory = myDirectory.Parent.FullName;
            parentDirectory = parentDirectory.Substring(0, parentDirectory.Length - 9);
            parentDirectory = parentDirectory + "SoftwareEngineeringDatabase.mdf";
            string Firstpart = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=";
            string Lastpart = ";Integrated Security = True;Connect Timeout =30";
            string Fullpath = Firstpart + parentDirectory + Lastpart;
            return Fullpath;
        }
        private void SaveButton(object sender, RoutedEventArgs e)
        {
            //Need to add textboxs for first and last name and need to make Age be a date of birth
            //Sqlstring to be used in sqlcommand
            string sqlquery = "INSERT INTO Users (Firstname, Lastname, Gender, DOB, Height, Weight, Ethnicity, Medical)" +
                "VALUES('" + tbxFirst.Text + "', '" + tbxLast.Text + "', '" + tbxGender.Text + "', '" + dpAge.SelectedDate.Value.ToString() + "', '" + tbxHeight.Text + "', '" + tbxWeight.Text + "', '" + tbxEthnicity.Text + "', '" + tbxMedical.Text + "')";

            //Sqlstring to get ID for login details insert
            string UIDquery = "SELECT UserID FROM Users Where Firstname = ('" + tbxFirst.Text + "') AND LASTNAME = ('" + tbxLast.Text + "')";

            //  Connection name default connection string
            SqlConnection con = new SqlConnection(Properties.Settings.Default.GeneratePath());
            SqlCommand cmd = new SqlCommand(sqlquery, con);
            SqlCommand cmdUID = new SqlCommand(UIDquery, con);
            //userId int for registrationquery
            int UId;
            //Opening Connection
            con.Open();
            //Executing Cmd command
            cmd.ExecuteNonQuery();
            //Executing UIDquery and casting it as an int for UId string
            //Previously was UIDQuery
            UId = (Int32)cmdUID.ExecuteScalar();

            //Sqlstring to insert username and password into table
            string Registrationquery = "INSERT INTO Login (UserID, Username, Password) VALUES ('" + UId + "', '" + tbxUsername.Text + "', '" + pbxPassword.Password + "')";

            SqlCommand cmdReg = new SqlCommand(Registrationquery, con);
            //Executing Registration query
            cmdReg.ExecuteNonQuery();
            con.Close();
            this.NavigationService.Navigate(new Login_Screen());
        }

        //Check event show password
        private void ShowPasswordCheckedRegisterScreen(object sender, RoutedEventArgs e)
        {
            TextBoxShownRegister.Text = pbxPassword.Password;
            TextBoxShownRegister.Visibility = Visibility.Visible;
            pbxPassword.Visibility = Visibility.Collapsed;
        }
        //Check event hide password
        private void ShowPasswordUncheckedRegisterScreen(object sender, RoutedEventArgs e)
        {
            TextBoxShownRegister.Visibility = Visibility.Collapsed;
            pbxPassword.Visibility = Visibility.Visible;
        }
        //Text changed event to match text and password boxes
        private void TextBoxShownRegister_TextChanged(object sender, TextChangedEventArgs e)
        {
            pbxPassword.Password = TextBoxShownRegister.Text;
        }
    }
}
