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
using System.Windows.Shapes;

namespace WPFBD_GD_1ER.View
{
    /// <summary>
    /// Logique d'interaction pour stock_HTML.xaml
    /// </summary>
    public partial class stock_HTML : Window
    {
        public stock_HTML()
        {
            InitializeComponent();
            //génère la destination du ficher html
            string htmlDirectory = System.Windows.Forms.Application.StartupPath+"\\livre_stock.html";
            //ouvrir le navigateur
            WB_stock.Navigate(new Uri(String.Format(htmlDirectory)));
        }
    }
}
