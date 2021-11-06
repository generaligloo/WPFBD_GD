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
        private ViewModel.VM_location LocalLocation;
        private ViewModel.VM_Client LocalClient;
        private string sCon = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='" + System.Windows.Forms.Application.StartupPath + @"\bibliotheque.mdf';Integrated Security=True;Connect Timeout=30";
        public EcranLocation()
        {
            InitializeComponent();
            LocalLocation = new ViewModel.VM_location();
            LocalClient = new ViewModel.VM_Client();
            DataContext = LocalLocation;
            cbClient.IsEnabled = true;
            cbClient.ItemsSource = LocalClient.BcpClients;
            cbClient.SelectedValuePath = "ID_client"; ;
            FlowDocument fd = new FlowDocument();
            Paragraph p = new Paragraph();
            p.Inlines.Add(new Bold(new Run("Table de gestion des locations")));
            p.Inlines.Add(new LineBreak());
            p.Inlines.Add(new Run("Liste des locations"));
            fd.Blocks.Add(p);
            List l = new List();
            foreach (Model.C_TB_location cp in LocalLocation.BcpLocations)
            {
                Model.C_TB_client cTmp = new Model.G_TB_client(sCon).Lire_ID(cp.ID_client);
                Paragraph pl = new Paragraph(new Run("(" + cp.ID_location + ") id du client:" + cp.ID_client + " - " + cTmp.client_nom + " " + cTmp.client_nom + 
                    " date de location: " + cp.dat_location.ToString())); 
                l.ListItems.Add(new ListItem(pl));
            }
            fd.Blocks.Add(l);
            rtbDoc.Document = fd;
            FileStream fs = new FileStream(@"d:\essai.rtf", FileMode.Create);
            TextRange tr = new TextRange(rtbDoc.Document.ContentStart, rtbDoc.Document.ContentEnd);
            tr.Save(fs, System.Windows.DataFormats.Rtf);
            fs.Close();
        }

        public EcranLocation(int cID)
        {
            InitializeComponent();
            LocalLocation = new ViewModel.VM_location(cID);
            LocalClient = new ViewModel.VM_Client();
            DataContext = LocalLocation;
            cbClient.IsEnabled = false;
            cbClient.ItemsSource = LocalClient.BcpClients;
            cbClient.SelectedValuePath = "ID_client";
            cbClient.SelectedItem = cID;
            FlowDocument fd = new FlowDocument();
            Paragraph p = new Paragraph();
            p.Inlines.Add(new Bold(new Run("Table de gestion des locations")));
            p.Inlines.Add(new LineBreak());
            p.Inlines.Add(new Run("Liste des locations"));
            fd.Blocks.Add(p);
            List l = new List();
            foreach (Model.C_TB_location cp in LocalLocation.BcpLocations)
            {
                Model.C_TB_client cTmp = new Model.G_TB_client(sCon).Lire_ID(cp.ID_client);
                Paragraph pl = new Paragraph(new Run("(" + cp.ID_location + ") id du client: " + cp.ID_client + " - " + cTmp.client_nom + " " + cTmp.client_prenom +
                    " date de location: " + cp.dat_location.ToString()));
                l.ListItems.Add(new ListItem(pl));
            }
            fd.Blocks.Add(l);
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
                tbIDC.Text = cbClient.SelectedValue.ToString();
            }
        }
    }
}