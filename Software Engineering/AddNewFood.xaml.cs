using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Configuration;

using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Software_Engineering
{
    /// <summary>
    /// Interaction logic for AddNewFood.xaml
    /// </summary>
    public partial class AddNewFood : Page
    {
        public AddNewFood()
        {
            InitializeComponent();
        }

        private void SaveFoodButton(object sender, RoutedEventArgs e)
        {
            //sql string to get new foodid
            string sqlgetint = "SELECT MAX(Incrementer) FROM [DATA.AP]";

            //sql connection
            SqlConnection con = new SqlConnection(Properties.Settings.Default.GeneratePath());
            //sqlcommand for get new foodid
            SqlCommand cmdget = new SqlCommand(sqlgetint, con);
            //Open connection
            con.Open();
            //running get foodid command
            int foodid1 = (Int32)cmdget.ExecuteScalar();
            //increment foodid
            foodid1 = foodid1 + 1;
            //casting foodid1 as string
            string foodid = foodid1.ToString();

            //sql string to add new food data to database
            string sqladdFood = "INSERT INTO [DATA.AP]([FoodID], [Food name], [Energy, total metabolisable (kJ)], [Fatty acids, total saturated], [Fat, total], [Protein, total; calculated from total nitrogen], [Sugars, total], [Fibre, total dietary], [Available carbohydrate, FSANZ], Incrementer) VALUES ('" + foodid + "', '" + tbxFoodName.Text + "', '" + tbxEnergy.Text + "', '" + tbxSatFat.Text + "', '" + tbxTotalFat.Text + "', '" + tbxProtein.Text + "', '" + tbxSugars.Text + "', '" + tbxFibres.Text + "', '" + tbxCarbohydrates.Text + "', '" + foodid1 + "')";
            //sqlcommand to add new food
            SqlCommand cmd = new SqlCommand(sqladdFood, con);
            //executing sqlcommand to add new food
            cmd.ExecuteNonQuery();
            con.Close();
            //Display Messagebox on success
            MessageBox.Show("New Food added successfully");
            //Navigate to search page
            this.NavigationService.Navigate(new SearchPage());
        }
    }
}
