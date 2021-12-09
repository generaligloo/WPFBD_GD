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
using WPFBD_GD_1ER.ViewModel;

namespace WPFBD_GD_1ER.View
{
    /// <summary>
    /// Logique d'interaction pour changeDate.xaml
    /// </summary>
    public partial class EcranChangeDate : Window
    {
        public EcranChangeDate(int dID, int d2ID)
        {
            InitializeComponent();
            LocalDetail = new VM_Detail(dID, d2ID);
            DataContext = LocalDetail;
        }

        private VM_Detail LocalDetail { get; }

        private void bValiderDate_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
