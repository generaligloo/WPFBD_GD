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
            FlowDocument fd = new FlowDocument();
            Paragraph p = new Paragraph();
            p.Inlines.Add(new Bold(new Run("Table de gestion des clients")));
            p.Inlines.Add(new LineBreak());
            p.Inlines.Add(new Run("Liste des Clients encodées"));
            fd.Blocks.Add(p);
            List l = new List();
            foreach (Model.C_TB_client cp in LocalClient.BcpClients)
            {
                Paragraph pl = new Paragraph(new Run(cp.client_prenom + " " + cp.client_nom
                 + " (" + cp.client_nai.ToShortDateString() + ") " + cp.client_cotisation.ToShortDateString() + " / " + cp.client_crea.ToShortDateString() + " Mail: " + cp.client_mail.ToString()));
                l.ListItems.Add(new ListItem(pl));
            }
            fd.Blocks.Add(l);
            rtbDoc.Document = fd;
            FileStream fs = new FileStream(@"d:\essai.rtf", FileMode.Create);
            TextRange tr = new TextRange(rtbDoc.Document.ContentStart, rtbDoc.Document.ContentEnd);
            tr.Save(fs, System.Windows.DataFormats.Rtf);
        }

        private void dgClients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgClients.SelectedIndex >= 0) LocalClient.ClientSelectionnee2UneClient();
        }
    }
}