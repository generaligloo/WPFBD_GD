using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace WPFBD_GD_1ER.View
{
    /// <summary>
    /// Logique d'interaction pour Location.xaml
    /// </summary>
    public partial class EcranLocation : Window
    {
        private ViewModel.VM_Location LocalLocation;
        private ViewModel.VM_Client LocalClient;
        private string sCon = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='" + System.Windows.Forms.Application.StartupPath + @"\bibliotheque.mdf';Integrated Security=True;Connect Timeout=30";
        public EcranLocation()
        {
            InitializeComponent();
            LocalLocation = new ViewModel.VM_Location();
            LocalClient = new ViewModel.VM_Client();
            DataContext = LocalLocation;
            cbClient.IsEnabled = true;
            cbClient.ItemsSource = LocalClient.BcpClients;
            cbClient.SelectedValuePath = "ID_client"; ;
            FlowDocument fd = LocalLocation.GenererFlow();
            rtbDoc.Document = fd;
            FileStream fs = new FileStream(@"d:\essai.rtf", FileMode.Create);
            TextRange tr = new TextRange(rtbDoc.Document.ContentStart, rtbDoc.Document.ContentEnd);
            tr.Save(fs, System.Windows.DataFormats.Rtf);
            fs.Close();
        }

        public EcranLocation(int cID)
        {
            InitializeComponent();
            LocalLocation = new ViewModel.VM_Location(cID);
            LocalClient = new ViewModel.VM_Client();
            DataContext = LocalLocation;
            cbClient.IsEnabled = false;
            cbClient.ItemsSource = LocalClient.BcpClients;
            cbClient.SelectedValuePath = "ID_client";
            cbClient.SelectedItem = cID;
            FlowDocument fd = LocalLocation.GenererFlow();
            rtbDoc.Document = fd;
            FileStream fs = new FileStream(@"d:\essai.rtf", FileMode.Create);
            TextRange tr = new TextRange(rtbDoc.Document.ContentStart, rtbDoc.Document.ContentEnd);
            tr.Save(fs, System.Windows.DataFormats.Rtf);
            fs.Close();
        }

        private void dgLocation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgLocation.SelectedIndex >= 0)
            {
                LocalLocation.LocationSelectionnee2UneLocation();
            }
        }

        private void cbClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgLocation.SelectedIndex >= 0)
            {
                if(cbClient.SelectedValue != null)
                    tbIDC.Text = cbClient.SelectedValue.ToString();
            }
        }
    }
}