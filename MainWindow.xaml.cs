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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2
{
    using System.Data.SqlClient;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class UserSettingsWindow : Window
    {
        public string Email { get; set; }
        public string Neév { get; set; }
        public int Asztalszám { get; set; }
        public UserSettingsWindow(string email)
        {
            InitializeComponent();
            Email = email;

   
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection("server=api.uniassist.hu;uid=QuickServe;pwd=Csütörtök8;database=QuickServe");
                conn.Open();

                string query = "SELECT Nev, Asztalszem FROM Vendegek WHERE Email = @Email";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", Email);

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    Név = reader["Nev"].ToString();
                    Asztalszám = int.Parse(reader["Asztalszam"].ToString());
                }

                reader.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Hiba az adatbázis-kapcsolatban: " + ex.Message, "Adatbázis hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                conn?.Close();
            }
        }

        private void InitializeComponent()
        {
            throw new NotImplementedException();
        }

        private void Mentes(object sender, RoutedEventArgs e)
        {
           
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection("server=api.uniassist.hu;uid=QuickServe;pwd=Csütörtök8;database=QuickServe");
                conn.Open();

                string query = "UPDATE Vendegek SET Nev = @Nev, Asztalszam = @Asztalszam WHERE Email = @Email";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@Nev", Név);
                cmd.Parameters.AddWithValue("@Asztalszam", Asztalszám);

                cmd.ExecuteNonQuery();

                MessageBox.Show("A beállítások mentve!", "Siker", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Hiba az adatbázis-kapcsolatban: " + ex.Message, "Adatbázis hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                conn?.Close();
            }
        }
    }

}
