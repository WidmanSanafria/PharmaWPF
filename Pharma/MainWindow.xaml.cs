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
using System.Data.SqlClient;
using System.Data;

namespace Pharma
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            cargarGrid();
        }
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-L6D4T30;Initial Catalog=farma;Integrated Security=True");
        public void limpiar()
        {
            txtNombre.Clear();
            txtEdad.Clear();
            txtGenero.Clear();
            txtCiudad.Clear();
            txtBuscar.Clear(); 
        }
        public void cargarGrid()
        {
            SqlCommand cmd = new SqlCommand("select * from usuarios", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            lstUsers.ItemsSource = dt.DefaultView;
        }
        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            limpiar();
        }
        public bool esValido()
        {
            if(txtNombre.Text == string.Empty)
            {
                MessageBox.Show("El campo es requerido", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtEdad.Text == string.Empty)
            {
                MessageBox.Show("El campo es requerido", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtGenero.Text == string.Empty)
            {
                MessageBox.Show("El campo es requerido", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtCiudad.Text == string.Empty)
            {
                MessageBox.Show("El campo es requerido", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        private void btnInsertar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (esValido())
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO usuarios VALUES(@nombre, @edad, @genero, @ciudad)", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
                    cmd.Parameters.AddWithValue("@edad", txtEdad.Text);
                    cmd.Parameters.AddWithValue("@genero", txtGenero.Text);
                    cmd.Parameters.AddWithValue("@ciudad", txtCiudad.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    cargarGrid();
                    MessageBox.Show("Registro Guardado ", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                    limpiar();
                }
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBorrar_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from usuarios Where id = "+txtBuscar.Text+ " ", con);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Registro ha sido borrado", "Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                con.Close();
                limpiar();
                cargarGrid();
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Registro no borrado "+ ex.Message);
            }
            finally
            {
                con.Close();
            }

        }

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("update usuarios set nombre = '"+txtNombre.Text+"', edad = '"+txtEdad.Text+"', genero = '"+txtGenero.Text+"', ciudad = '"+txtCiudad.Text+"' WHERE id = '"+txtBuscar.Text+"' ",con);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Registro actuaizado con exito", "Actualizado", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message); 
            }
            finally
            {
                con.Close();
                limpiar();
                cargarGrid();
            }
        }

        private void btnReport_Click(object sender, RoutedEventArgs e)
        {
            Report rp = new Report();
            rp.Show();
        }
    }
}
