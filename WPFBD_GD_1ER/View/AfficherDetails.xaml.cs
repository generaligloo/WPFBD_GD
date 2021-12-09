using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace WPFBD_GD_1ER.View
{
    /// <summary>
    /// Logique d'interaction pour AfficherDetails.xaml
    /// </summary>
    /// 

    public partial class EcranAfficherDetails : Window
    {
        private string sCon;
        private int ID_loca;
        private ViewModel.VM_Livre LocalLivre;
        private ViewModel.VM_Detail LocalDetail;
        public EcranAfficherDetails(string sC, int lID)
        {
            InitializeComponent();
            sCon = sC;
            ID_loca = lID;
            LocalLivre = new ViewModel.VM_Livre();
            LocalDetail = new ViewModel.VM_Detail(ID_loca);
            DataContext = LocalDetail;
        }

        private void dgDetails_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (dgDetails.SelectedIndex >= 0)
            {
                int? nID = null;
                DataGrid dataGrid = dgDetails;
                DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
                DataGridCell RowColumn = dataGrid.Columns[5].GetCellContent(row).Parent as DataGridCell;
                string CellValue = ((TextBlock)RowColumn.Content).Text;
                if (CellValue == "N/A")
                {
                    nID = null;
                }
                else
                {
                    nID = System.Int32.Parse(CellValue);
                }
                DataGrid dataGrid2 = dgDetails;
                DataGridRow row2 = (DataGridRow)dataGrid2.ItemContainerGenerator.ContainerFromIndex(dataGrid2.SelectedIndex);
                DataGridCell RowColumn2 = dataGrid2.Columns[0].GetCellContent(row2).Parent as DataGridCell;
                string CellValue2 = ((TextBlock)RowColumn2.Content).Text;
                int dID = System.Int32.Parse(CellValue2);
                LocalDetail.VerifAmende(nID, dID);
            }
        }

        private void bRefresh_Click(object sender, RoutedEventArgs e)
        {
            LocalDetail = new ViewModel.VM_Detail(ID_loca);
            DataContext = LocalDetail;
        }
    }
}
