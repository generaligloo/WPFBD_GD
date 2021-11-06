using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace WPFBD_GD_1ER.View
{
    /// <summary>
    /// Logique d'interaction pour Edition.xaml
    /// </summary>
    public partial class EcranEdition : Window
    {
        private ViewModel.VM_Edition LocalEdition;

        public EcranEdition()
        {
            InitializeComponent();
            LocalEdition = new ViewModel.VM_Edition();
            DataContext = LocalEdition;
            FlowDocument fd = new FlowDocument();
            Paragraph p = new Paragraph();
            p.Inlines.Add(new Bold(new Run("Table de gestion des maisons d'édition")));
            p.Inlines.Add(new LineBreak());
            p.Inlines.Add(new Run("Liste des maisons encodées"));
            fd.Blocks.Add(p);
            List l = new List();
            foreach (Model.C_TB_edition cp in LocalEdition.BcpEditions)
            {
                if (cp.edi_pdg_nom == "" && cp.edi_pdg_prenom == "")
                {
                    Paragraph pl = new Paragraph(new Run("(" + cp.ID_edition + ") " + cp.edi_nom+ " - " + cp.edi_dat.Year));
                    l.ListItems.Add(new ListItem(pl));
                }
                else 
                {
                    Paragraph pl = new Paragraph(new Run("(" + cp.ID_edition + ") " + cp.edi_nom + " - " + cp.edi_dat.Year
                        + " PDG: " + cp.edi_pdg_nom + " " +cp.edi_pdg_prenom));
                    l.ListItems.Add(new ListItem(pl));
                }
            }
            fd.Blocks.Add(l);
            rtbDoc.Document = fd;
            FileStream fs = new FileStream(@"d:\essai.rtf", FileMode.Create);
            TextRange tr = new TextRange(rtbDoc.Document.ContentStart, rtbDoc.Document.ContentEnd);
            tr.Save(fs, System.Windows.DataFormats.Rtf);
            fs.Close();
        }

        private void bActu_Click(object sender, RoutedEventArgs e)
        {
            rtbDoc.Document.Blocks.Clear();
            FlowDocument fd = new FlowDocument();
            Paragraph p = new Paragraph();
            p.Inlines.Add(new Bold(new Run("Table de gestion des maisons d'édition")));
            p.Inlines.Add(new LineBreak());
            p.Inlines.Add(new Run("Liste des maisons encodées"));
            fd.Blocks.Add(p);
            List l = new List();
            foreach (Model.C_TB_edition cp in LocalEdition.BcpEditions)
            {
                if (cp.edi_pdg_nom == "" && cp.edi_pdg_prenom == "")
                {
                    Paragraph pl = new Paragraph(new Run("(" + cp.ID_edition + ") " + cp.edi_nom + " - " + cp.edi_dat.Year));
                    l.ListItems.Add(new ListItem(pl));
                }
                else
                {
                    Paragraph pl = new Paragraph(new Run("(" + cp.ID_edition + ") " + cp.edi_nom + " - " + cp.edi_dat.Year
                        + " PDG: " + cp.edi_pdg_nom + " " + cp.edi_pdg_prenom));
                    l.ListItems.Add(new ListItem(pl));
                }
            }
            fd.Blocks.Add(l);
            rtbDoc.Document = fd;
            FileStream fs = new FileStream(@"d:\essai.rtf", FileMode.Create);
            TextRange tr = new TextRange(rtbDoc.Document.ContentStart, rtbDoc.Document.ContentEnd);
            tr.Save(fs, System.Windows.DataFormats.Rtf);
            fs.Close();
        }

        private void dgEdition_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgEdition.SelectedIndex >= 0)
            {
                LocalEdition.EditionSelectionnee2UneEdition();
            }
        }
    }
}