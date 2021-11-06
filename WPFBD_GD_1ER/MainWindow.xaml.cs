using System.Windows;
using WPFBD_GD_1ER.ViewModel;
using WPFBD_GD_1ER.View;
using WPFBD_GD_1ER.Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace WPFBD_GD_1ER
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private VM_Client prev_client;
        private VM_location select_loca;
        private C_TB_client select_client;
        string sCon = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='" + System.Windows.Forms.Application.StartupPath + @"\bibliotheque.mdf';Integrated Security=True;Connect Timeout=30";
        public MainWindow()
        {
            InitializeComponent();
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("fr-FR");
            //MessageBox.Show(testc);
            prev_client = new VM_Client();
            dgClientsPrev.DataContext = prev_client;
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
        }

        private void dgClientsPrev_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (dgClientsPrev.SelectedIndex >= 0)
            {
                select_client = (Model.C_TB_client)dgClientsPrev.SelectedItem;
                int cID = select_client.ID_client;
                if (DateTime.Today.Year - select_client.client_nai.Year >= 18)
                {
                    cb_majeur.IsChecked = true;
                }
                else
                {
                    cb_majeur.IsChecked = false;
                }
                RemplirLocation(cID);
            }
        }

        private void RemplirLocation(int cID)
        {
            select_loca = new VM_location(cID);
            dgLocationPrev.DataContext = select_loca;
        }

        private void btnHTML_Click(object sender, RoutedEventArgs e)
        {
            List<C_TB_livre> lTmp = new G_TB_livre(sCon).Lire("ID_livre");
            List<C_TB_edition> eTmp = new G_TB_edition(sCon).Lire("ID_edition");
            StreamWriter sw;
            sw = File.CreateText("livre_stock.html");
            sw.Write("<html>");
            sw.Write("<title> Stock de la bibliothèque </title>");
            sw.Write("<meta charset = 'utf-8'>");
            sw.Write("<link rel='shortcut icon' href='" + System.Windows.Forms.Application.StartupPath + "\\39859.png'>");
            sw.Write("<link rel='stylesheet' href='"+System.Windows.Forms.Application.StartupPath+"\\index.css'>");
            sw.Write("<body>");
            sw.Write("<table class='content-table'>");
            sw.Write("<thead>");
            sw.Write("<tr><th>Auteur</th><th>Titre</th><th>Statut (dispo = 0)</th><th>Maison d'édition</th></tr>");
            sw.Write("</thead>");
            sw.Write("<tbody>");
            foreach (C_TB_livre l in lTmp)
            {
                C_TB_edition edit_tmp = new G_TB_edition(sCon).Lire_ID(l.ID_edition);
                if (l.statut == 1)
                {
                    sw.Write("<tr><td>" + l.auteur + "</td><td>" + l.titre + "</td><td>" + l.statut + "</td><td>" + edit_tmp.edi_nom + "</td></tr>");
                }
                else
                {
                    sw.Write("<tr><td>" + l.auteur + "</td><td>" + l.titre + "</td><td>" + l.statut + "</td><td>" + edit_tmp.edi_nom + "</td></tr>");
                }
            }
            sw.Write("</tbody>");
            sw.Write("</table>");
            sw.Write("</body>");
            sw.Write("</html>");
            sw.Close();
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

        }

        private void btnAffLocationID_Click(object sender, RoutedEventArgs e)
        {
            if (dgClientsPrev.SelectedIndex >= 0)
            {
                EcranLocation flocaid = new EcranLocation(dgClientsPrev.SelectedIndex+1);
                flocaid.ShowDialog();
            }
        }
    }
}