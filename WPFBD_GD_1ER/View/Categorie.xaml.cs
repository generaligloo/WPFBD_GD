using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace WPFBD_GD_1ER.View
{
    /// <summary>
    /// Logique d'interaction pour Categorie.xaml
    /// </summary>
    public partial class EcranCategorie : Window
    {
        private ViewModel.VM_Categorie LocalCategorie;

        public EcranCategorie()
        {
            InitializeComponent();
            LocalCategorie = new ViewModel.VM_Categorie();
            DataContext = LocalCategorie;
            FlowDocument fd = new FlowDocument();
            Paragraph p = new Paragraph();
            p.Inlines.Add(new Bold(new Run("Table de gestion des catégories")));
            p.Inlines.Add(new LineBreak());
            p.Inlines.Add(new Run("Liste des catégories encodées"));
            fd.Blocks.Add(p);
            List l = new List();
            foreach (Model.C_TB_categorie cp in LocalCategorie.BcpCategories)
            {
                if (cp.Pegi == 1)
                {
                    Paragraph pl = new Paragraph(new Run("(" + cp.ID_categorie + "): " + cp.Nom
                     + " (PEGI 18+) "));
                    l.ListItems.Add(new ListItem(pl));
                }
                else
                {
                    Paragraph pl = new Paragraph(new Run("(" + cp.ID_categorie + "): " + cp.Nom
                     + " () "));
                    l.ListItems.Add(new ListItem(pl));
                }
            }
            fd.Blocks.Add(l);
            rtbDoc.Document = fd;
            FileStream fs = new FileStream(@"d:\essai.rtf", FileMode.Create);
            TextRange tr = new TextRange(rtbDoc.Document.ContentStart, rtbDoc.Document.ContentEnd);
            tr.Save(fs, System.Windows.DataFormats.Rtf);
        }

        private void dgCategorie_SelectedChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgCategorie.SelectedIndex >= 0)
            {
                LocalCategorie.CategorieSelectionnee2UneCategorie((bool)cbPegi.IsChecked);
            }
            if (e.AddedItems != null && e.AddedItems.Count > 0)
            {
                DataGrid dataGrid = sender as DataGrid;
                DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
                DataGridCell RowColumn = dataGrid.Columns[2].GetCellContent(row).Parent as DataGridCell;
                string CellValue = ((TextBlock)RowColumn.Content).Text;
                int pegitest = System.Int32.Parse(CellValue);
                if (pegitest == 1)
                {
                    cbPegi.IsChecked = true;
                }
                else
                {
                    cbPegi.IsChecked = false;
                }
            }
        }

        private void bActu_Click(object sender, RoutedEventArgs e)
        {
            rtbDoc.Document.Blocks.Clear();
            FlowDocument fd = new FlowDocument();
            Paragraph p = new Paragraph();
            p.Inlines.Add(new Bold(new Run("Table de gestion des catégories")));
            p.Inlines.Add(new LineBreak());
            p.Inlines.Add(new Run("Liste des catégories encodées"));
            fd.Blocks.Add(p);
            List l = new List();
            foreach (Model.C_TB_categorie cp in LocalCategorie.BcpCategories)
            {
                if (cp.Pegi == 1)
                {
                    Paragraph pl = new Paragraph(new Run("(" + cp.ID_categorie + "): " + cp.Nom
                     + " (PEGI 18+) "));
                    l.ListItems.Add(new ListItem(pl));
                }
                else
                {
                    Paragraph pl = new Paragraph(new Run("(" + cp.ID_categorie + "): " + cp.Nom
                     + " () "));
                    l.ListItems.Add(new ListItem(pl));
                }
            }
            fd.Blocks.Add(l);
            rtbDoc.Document = fd;
            FileStream fs = new FileStream(@"d:\essai.rtf", FileMode.Create);
            TextRange tr = new TextRange(rtbDoc.Document.ContentStart, rtbDoc.Document.ContentEnd);
            tr.Save(fs, System.Windows.DataFormats.Rtf);
        }
    }
}