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
            rtbDoc.Document.Blocks.Clear();
            FlowDocument fd = LocalEdition.GenererFlow();
            rtbDoc.Document = fd;
            FileStream fs = new FileStream(@"d:\essai.rtf", FileMode.Create);
            TextRange tr = new TextRange(rtbDoc.Document.ContentStart, rtbDoc.Document.ContentEnd);
            tr.Save(fs, System.Windows.DataFormats.Rtf);
            fs.Close();
        }

        private void bActu_Click(object sender, RoutedEventArgs e)
        {
            rtbDoc.Document.Blocks.Clear();
            FlowDocument fd = LocalEdition.GenererFlow();
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