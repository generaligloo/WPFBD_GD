using System.Windows;
using WPFBD_GD_1ER.ViewModel;
using WPFBD_GD_1ER.View;
using System.Windows.Controls;

namespace WPFBD_GD_1ER
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private VM_Client prev_client;
        private VM_Location select_loca;
        private VM_Categorie prev_cat;
        private VM_HTML HTMLgen;
        private VM_TXT TXTgen;
        private VM_Livre prev_livre;
        string sCon = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='" + System.Windows.Forms.Application.StartupPath + @"\bibliotheque.mdf';Integrated Security=True;Connect Timeout=30";
        public int? aGen;

        public MainWindow()
        {
            InitializeComponent();
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("fr-FR");
            //MessageBox.Show(testc);
            prev_client = new VM_Client();
            prev_livre = new VM_Livre();
            prev_cat = new VM_Categorie();
            select_loca = new VM_Location();
            HTMLgen = new VM_HTML();
            TXTgen = new VM_TXT();
            dgLivrePrev.DataContext = prev_livre;
            dgClientsPrev.DataContext = prev_client;
            prev_client.Init_View();
            btnHTML.DataContext = HTMLgen;
            btnAjLocation.DataContext = prev_client;
            btntxt.DataContext = TXTgen;
            btnGenererBor.DataContext = select_loca;
        }

        private void btnClient_Click(object sender, RoutedEventArgs e)
        {
            EcranClient fclient = new EcranClient();
            fclient.ShowDialog();
            prev_client = new VM_Client();
            dgClientsPrev.DataContext = prev_client;
        }

        private void btnQuitter_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnCategorie_Click(object sender, RoutedEventArgs e)
        {
            EcranCategorie fcate = new EcranCategorie();
            fcate.ShowDialog();
        }

        private void btnEdtion_Click(object sender, RoutedEventArgs e)
        {
            EcranEdition fedi = new EcranEdition();
            fedi.ShowDialog();
        }

        private void btnLivre_Click(object sender, RoutedEventArgs e)
        {
            EcranLivre flivr = new EcranLivre();
            flivr.ShowDialog();
            prev_livre = new VM_Livre();
            dgLivrePrev.DataContext = prev_livre;
        }

        private void dgClientsPrev_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (dgClientsPrev.SelectedIndex >= 0)
            {
                DataGrid dataGrid = dgClientsPrev;
                DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
                DataGridCell RowColumn = dataGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;
                string CellValue = ((TextBlock)RowColumn.Content).Text;
                int nID = System.Int32.Parse(CellValue);
                if (prev_client.verif_age_CBX(nID))
                {
                    cb_majeur.IsChecked = true;
                }
                else
                {
                    cb_majeur.IsChecked = false;
                }
                RemplirLocation(nID);
            }
        }

        private void RemplirLocation(int cID)
        {
            select_loca = new VM_Location(cID);
            dgLocationPrev.DataContext = select_loca;
        }

        private void btnHTML_Click(object sender, RoutedEventArgs e)
        {
            stock_HTML SH = new stock_HTML();
            SH.ShowDialog();
        }

        private void btnLocation_Click(object sender, RoutedEventArgs e)
        {
            EcranLocation floca = new EcranLocation();
            floca.ShowDialog();
        }

        private void dgLocationPrev_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (dgClientsPrev.SelectedIndex >= 0)
            {
                btnGenererBor.IsEnabled = true;
            }
            else
            {
                btnGenererBor.IsEnabled = false;
            }
        }

        private void btnAffLocationID_Click(object sender, RoutedEventArgs e)
        {
            if (dgClientsPrev.SelectedIndex >= 0)
            {
                DataGrid dataGrid = dgClientsPrev;
                DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
                DataGridCell RowColumn = dataGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;
                string CellValue = ((TextBlock)RowColumn.Content).Text;
                int nID = System.Int32.Parse(CellValue);
                EcranLocation flocaid = new EcranLocation(nID);
                flocaid.ShowDialog();
            }
        }

        private void dgLivrePrev_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if(e.AddedItems != null && e.AddedItems.Count > 0)
            { 
                DataGrid dataGrid = sender as DataGrid;
                DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
                DataGridCell RowColumn = dataGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;
                string CellValue = ((TextBlock)RowColumn.Content).Text;
                int nID = System.Int32.Parse(CellValue);
                cb_Dispo.IsChecked = prev_livre.CB_Dispo_verif(nID);
                cb_Pegi.IsChecked = prev_livre.CB_Pegi_verif(nID);
                cb_Retard.IsChecked = prev_livre.CB_Retard_verif(nID);
            }
        }

        private void btnAjLocation_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void dgClientsPrev_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            prev_client = new VM_Client();
            dgClientsPrev.DataContext = prev_client;
        }

        private void btnGenererBor_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dgLocationPrev_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            prev_client = new VM_Client();
            dgClientsPrev.DataContext = prev_client;
        }
    }
}