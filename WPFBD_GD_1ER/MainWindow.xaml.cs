using System.Windows;

namespace WPFBD_GD_1ER
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("fr-FR");
            //string testc = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='" + System.Windows.Forms.Application.StartupPath + @"\bibliotheque.mdf';Integrated Security=True;Connect Timeout=30";
            //MessageBox.Show(testc);
        }

        private void btnClient_Click(object sender, RoutedEventArgs e)
        {
            View.EcranClient fclient = new View.EcranClient();
            fclient.ShowDialog();
        }

        private void btnQuitter_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnCategorie_Click(object sender, RoutedEventArgs e)
        {
            View.EcranCategorie fcate = new View.EcranCategorie();
            fcate.ShowDialog();
        }

        private void btnEdtion_Click(object sender, RoutedEventArgs e)
        {
            View.EcranEdition fedi = new View.EcranEdition();
            fedi.ShowDialog();
        }
    }
}