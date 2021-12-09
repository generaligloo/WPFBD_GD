using System;
using System.Windows;
using System.Windows.Controls;

namespace WPFBD_GD_1ER.View
{
    /// <summary>
    /// Logique d'interaction pour ajoutDetail.xaml
    /// </summary>
    public partial class EcranAjoutDetail : Window
    {
        private ViewModel.VM_Livre LocalLivre;
        private ViewModel.VM_Detail LocalDetail;
        private int ID_loca;
        public EcranAjoutDetail(string sC, int lID)
        {
            InitializeComponent();
            ID_loca = lID;
            LocalLivre = new ViewModel.VM_Livre();
            LocalDetail = new ViewModel.VM_Detail(ID_loca);
            DataContext = LocalDetail;
            cboLivre.ItemsSource = LocalLivre.BcpLivresDispo;
            //cboLivre.DisplayMemberPath = "Titre";
            cboLivre.SelectedValuePath = "ID_livre";
        }

        private void cboLivre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void btn_Annuler_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_conf_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
