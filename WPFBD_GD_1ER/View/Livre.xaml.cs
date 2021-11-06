using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;

namespace WPFBD_GD_1ER.View
{
    /// <summary>
    /// Logique d'interaction pour Categorie.xaml
    /// </summary>
    public partial class EcranLivre : Window
    {
        private ViewModel.VM_Categorie LocalCategorie;
        private ViewModel.VM_Edition LocalEdition;
        private ViewModel.VM_Livre LocalLivre;

        public EcranLivre()
        {
            InitializeComponent();
            LocalEdition = new ViewModel.VM_Edition();
            LocalCategorie = new ViewModel.VM_Categorie();
            LocalLivre = new ViewModel.VM_Livre();
            DataContext = LocalLivre;
            cboCat.ItemsSource = LocalCategorie.BcpCategories;
            cboCat.DisplayMemberPath = "Nom";
            cboCat.SelectedValuePath = "ID_categorie";
            cboEdi.ItemsSource = LocalEdition.BcpEditions;
            cboEdi.DisplayMemberPath = "edi_nom";
            cboEdi.SelectedValuePath = "ID_edition";
            FlowDocument fd = new FlowDocument();
            Paragraph p = new Paragraph();
            p.Inlines.Add(new Bold(new Run("Table de gestion des livres")));
            p.Inlines.Add(new LineBreak());
            p.Inlines.Add(new Run("Liste des livres encodées"));
            fd.Blocks.Add(p);
            List l = new List();
            foreach (Model.C_TB_livre cp in LocalLivre.BcpLivres)
            {
                if (cp.statut == 1)
                {
                    Paragraph pl = new Paragraph(new Run("(" + cp.ID_livre + "): " + cp.titre + " - " + cp.ID_livre
                     + " (indisponible) "));
                    l.ListItems.Add(new ListItem(pl));
                }
                else
                {
                    Paragraph pl = new Paragraph(new Run("(" + cp.ID_livre + "): " + cp.titre + " - " + cp.ID_livre
                     + " (disponible) "));
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
            p.Inlines.Add(new Bold(new Run("Table de gestion des livres")));
            p.Inlines.Add(new LineBreak());
            p.Inlines.Add(new Run("Liste des livres encodées"));
            fd.Blocks.Add(p);
            List l = new List();
            foreach (Model.C_TB_livre cp in LocalLivre.BcpLivres)
            {
                if (cp.statut == 1)
                {
                    Paragraph pl = new Paragraph(new Run("(" + cp.ID_livre + "): " + cp.titre + " - " + cp.ID_livre
                     + " (indisponible) "));
                    l.ListItems.Add(new ListItem(pl));
                }
                else
                {
                    Paragraph pl = new Paragraph(new Run("(" + cp.ID_livre + "): " + cp.titre + " - " + cp.ID_livre
                     + " (disponible) "));
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

        private void dgLivre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgLivre.SelectedIndex >= 0)
            {
                LocalLivre.LivreSelectionnee2UnLivre((bool)cbStat.IsChecked);
            }
            if (e.AddedItems != null && e.AddedItems.Count > 0)
            {
                DataGrid dataGrid = sender as DataGrid;
                DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
                DataGridCell RowColumn = dataGrid.Columns[6].GetCellContent(row).Parent as DataGridCell;
                string CellValue = ((TextBlock)RowColumn.Content).Text;
                int statut_test = System.Int32.Parse(CellValue);
                if (statut_test == 1)
                {
                    cbStat.IsChecked = true;
                }
                else
                {
                    cbStat.IsChecked = false;
                }
            }
        }

        private void dpPub_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}