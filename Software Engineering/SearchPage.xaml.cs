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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.VisualBasic;

namespace Software_Engineering
{
    /// <summary>
    /// Interaction logic for SearchPage.xaml
    /// </summary>
    public partial class SearchPage : Page
    {
        public SearchPage()
        {
            InitializeComponent();
            FoodDiaryIni();
            Expandedfooddiary();
        }

        public static int UserID;

        //Search text changed event
        private void tbxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Sql connection string
            SqlConnection con = new SqlConnection(Properties.Settings.Default.GeneratePath());

            //Sql string for search
            string sqlsearch = "SELECT [Food Name] FROM [DATA.AP] WHERE [Food Name] LIKE '%" + tbxSearch.Text + "%'";

            //Open connection
            con.Open();
            //sql search command
            SqlCommand cmd = new SqlCommand(sqlsearch, con);
            //Execute search command and add to dataAdapter
            SqlDataAdapter adp = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            adp.Fill(dt);
            //Add search results to datagrid
            datasearch.ItemsSource = dt.DefaultView;
            con.Close();
        }

        private void Button_Click_Clear(object sender, RoutedEventArgs e)
        {
            //Navigate to Add New Food Page
            this.NavigationService.Navigate(new AddNewFood());
        }

        //Add to foodDiary click event
        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            //Make submit box invisible
            inputstack.Visibility = Visibility.Collapsed;

            //Sql Connection String
            SqlConnection con = new SqlConnection(Properties.Settings.Default.GeneratePath());
            //Open Connection
            con.Open();
            //Get Selected Cell Data as string Foodname
            DataGridCellInfo cell = datasearch.SelectedCells[0];
            String Foodname = ((TextBlock)cell.Column.GetCellContent(cell.Item)).Text;
            
            // Sql string to get FoodID
            string sqlFID = "SELECT FoodID FROM [DATA.AP] WHERE [Food Name] = '" + Foodname + "'";
            //Create Sql command and Execute
            SqlCommand cmdFID = new SqlCommand(sqlFID, con);
            String FoodID = (string)cmdFID.ExecuteScalar();
            //string for amount column 
            string amount = tbxamount.Text;
            // sql string to add data to userdiary table
            string sqladd = "INSERT INTO UserDiary(UserID, FoodID, Amount) VALUES ('" + UserID + "', '" + FoodID + "', '" + amount +"' )";

            //create and run sql command to add data to userdiary
            SqlCommand cmd = new SqlCommand(sqladd, con);          
            cmd.ExecuteNonQuery();
            //run FoodDiaryIni Method
            FoodDiaryIni();
            //run ExpandedfoodDiary Method
            Expandedfooddiary();
            //Close Connection
            con.Close();
        }

        //Page change Method
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //If statement to make hide or show search functions
            if (btnchange.Content.ToString() == "Hide")
            {
                Searchlabelstack.Visibility = Visibility.Hidden;
                Searchstack.Visibility = Visibility.Hidden;
                Searchgridstack.Visibility = Visibility.Hidden;
                Searchbuttonstack.Visibility = Visibility.Hidden;
                Foodlabelstack.Visibility = Visibility.Hidden;
                Foodgridstack.Visibility = Visibility.Hidden;
                Expandedfoodstack.Visibility = Visibility.Visible;
                btnchange.Content = "Show";
            }
            else if (btnchange.Content.ToString() == "Show")
            {
                Expandedfoodstack.Visibility = Visibility.Hidden;
                Foodlabelstack.Visibility = Visibility.Visible;
                Foodgridstack.Visibility = Visibility.Visible;
                Searchlabelstack.Visibility = Visibility.Visible;
                Searchstack.Visibility = Visibility.Visible;
                Searchgridstack.Visibility = Visibility.Visible;
                Searchbuttonstack.Visibility = Visibility.Visible;
                btnchange.Content = "Hide";
            }
            
        }
        //Delete from food diary button click event
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(Properties.Settings.Default.GeneratePath());
            con.Open();

            DataGridCellInfo cell;
            //If statement for which datagrid in use
            if (datadiary.IsVisible)
            {
                cell = datadiary.SelectedCells[0];
                
            }
            else if (datadiary.IsVisible == false)
            {
                cell = expandeddata.SelectedCells[0];
            }
            //string get foodname 
            String Foodname = ((TextBlock)cell.Column.GetCellContent(cell.Item)).Text;

            //sql string, command, and execution get foodID based on foodname
            string sqlFID = "SELECT FoodID FROM [DATA.AP] WHERE [Food Name] = '" + Foodname + "'";
            SqlCommand cmdFID = new SqlCommand(sqlFID, con);
            String FoodID = (string)cmdFID.ExecuteScalar();

            //sql string, command, and execution to delete entry
            string sqlDelete = "DELETE from UserDiary where UserID = '" + UserID + "'AND FoodID = '" + FoodID + "'AND [DATE] = (CONVERT([date],getdate()))";

            SqlCommand cmddelete = new SqlCommand(sqlDelete, con);
            cmddelete.ExecuteNonQuery();

            //Execute FoodDiaryIni Method
            FoodDiaryIni();
            //Execute ExpandedfoodDiary Method
            Expandedfooddiary();
        }

        //FoodDiaryIni Method Populates food diary datagrid
        private void FoodDiaryIni()
        {
            SqlConnection con = new SqlConnection(Properties.Settings.Default.GeneratePath());
            con.Open();

            string sqlFD = "SELECT d.[Food name] FROM [DATA.AP] d INNER JOIN UserDiary u ON d.FoodID = u.FoodID WHERE u.[DATE] = (CONVERT([date],getdate()))";

            SqlCommand cmdFD = new SqlCommand(sqlFD, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmdFD);

            DataTable dt = new DataTable();

            adp.Fill(dt);

            datadiary.ItemsSource = dt.DefaultView;

            con.Close();
        }

        //Expandedfooddiary Methon Populates expanded datagrid
        private void Expandedfooddiary()
        {
            SqlConnection con = new SqlConnection(Properties.Settings.Default.GeneratePath());
            con.Open();

            string sqlFD = "SELECT d.[Food name], [Energy, total metabolisable (kJ)] AS [Energy (kJ)], [Fatty acids, total saturated] AS [Fat, Saturated], [Fat, total], [Protein, total; calculated from total nitrogen] AS [Protein], [Sodium], [Sugars, total], [Fibre, total dietary] AS Fibre, [Available carbohydrate, FSANZ] AS Carbohydrates, [amount] FROM [DATA.AP] d INNER JOIN UserDiary u ON d.FoodID = u.FoodID WHERE u.[DATE] = (CONVERT([date],getdate()))";

            SqlCommand cmdFD = new SqlCommand(sqlFD, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmdFD);

            DataTable dt = new DataTable();

            adp.Fill(dt);

            expandeddata.ItemsSource = dt.DefaultView;

            con.Close();
        }

        //Shows submit form
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            inputstack.Visibility = Visibility.Visible;
        }
    }
}
