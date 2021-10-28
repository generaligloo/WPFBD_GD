using System;
using System.Collections.Generic;
using System.IO;
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
            p.Inlines.Add(new Bold(new Run("Titre de document")));
            p.Inlines.Add(new LineBreak());
            p.Inlines.Add(new Run("Liste des Clients encodées"));
            fd.Blocks.Add(p);
            List l = new List();
            foreach (Model.C_Client cp in LocalClient.BcpClients)
            {
                Paragraph pl = new Paragraph(new Run(cp.Pre + " " + cp.Nom
                 + " (" + cp.Nai.ToShortDateString() + ") "+ cp.Coti.ToShortDateString()+" / "+ cp.Crea.ToShortDateString()));
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
