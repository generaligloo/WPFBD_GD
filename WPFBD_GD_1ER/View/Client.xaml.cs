using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace WPFBD_GD_1ER.View
{
    /// <summary>
    /// Logique d'interaction pour Client.xaml
    /// </summary>
    public partial class EcranClient : Window
    {
        private ViewModel.VM_Client LocalClient;

        public EcranClient()
        {
            InitializeComponent();
            LocalClient = new ViewModel.VM_Client();
            DataContext = LocalClient;
            FlowDocument fd = LocalClient.GenererFlow();
            rtbDoc.Document = fd;
            FileStream fs = new FileStream(@"d:\essai.rtf", FileMode.Create);
            TextRange tr = new TextRange(rtbDoc.Document.ContentStart, rtbDoc.Document.ContentEnd);
            tr.Save(fs, System.Windows.DataFormats.Rtf);
        }

        private void dgClients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgClients.SelectedIndex >= 0) 
                LocalClient.ClientSelectionnee2UneClient();
        }
    }
}