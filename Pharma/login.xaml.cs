using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace Pharma
{
    /// <summary>
    /// Lógica de interacción para login.xaml
    /// </summary>
    public partial class login : Window
    {
        public login()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-L6D4T30;Initial Catalog=farma;Integrated Security=True");
        private void btnSubmit_Click(object sender, RoutedEventArgs e)

        {
            esValido();
            con.Open();
            SqlCommand comando = new SqlCommand("select userName, password from tblUser Where userName = @vuserName AND password = @vpassword", con);
            comando.Parameters.AddWithValue("@vuserName", txtUsername.Text);
            comando.Parameters.AddWithValue("@vpassword", txtPassword.Password);
            SqlDataReader lector = comando.ExecuteReader();
            if (lector.Read())
            {
                con.Close();
                MainWindow pantalla = new MainWindow();
                pantalla.Show();
                this.Close();
            }
            

        }
        public bool esValido()
        {
            if (txtUsername.Text == string.Empty)
            {
                MessageBox.Show("El campo es requerido", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtPassword.DataContext == string.Empty)
            {
                MessageBox.Show("El campo es requerido", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
    }
}
