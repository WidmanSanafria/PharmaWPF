using System;
using System.Collections.Generic;
using System.Data;
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
    /// Lógica de interacción para Report.xaml
    /// </summary>
    public partial class Report : Window
    {
        public Report()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(Report_Loaded);
            
        }
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-L6D4T30;Initial Catalog=farma;Integrated Security=True");
        
        private void Report_Loaded(object sender, RoutedEventArgs e)
        {
            this.reportViewer.ReportPath = System.IO.Path.Combine(Environment.CurrentDirectory, @"Resources\sales-order-detail.rdl");
            this.reportViewer.RefreshReport();
        }
    }
}
