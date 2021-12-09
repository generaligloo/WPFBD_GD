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
            rtbDoc.Document.Blocks.Clear();
            FlowDocument fd = LocalCategorie.GenererFlow();
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
            FlowDocument fd = LocalCategorie.GenererFlow();
            rtbDoc.Document = fd;
            FileStream fs = new FileStream(@"d:\essai.rtf", FileMode.Create);
            TextRange tr = new TextRange(rtbDoc.Document.ContentStart, rtbDoc.Document.ContentEnd);
            tr.Save(fs, System.Windows.DataFormats.Rtf);
        }
    }
}