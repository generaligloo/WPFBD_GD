using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFBD_GD_1ER.Model;

namespace WPFBD_GD_1ER.ViewModel
{
    internal class VM_HTML: BasePropriete
    {
        private VM_Livre prev_livre;
        private VM_Edition prev_edition;
        private string sCon = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='" + System.Windows.Forms.Application.StartupPath + @"\bibliotheque.mdf';Integrated Security=True;Connect Timeout=30";
        public VM_HTML()
        {
            cHTML = new BaseCommande(Generer);
        }

        public BaseCommande cHTML { get; set; }

        public void Generer()
        {
            prev_livre = new VM_Livre();
            prev_edition = new VM_Edition();
            StreamWriter sw;
            sw = File.CreateText("livre_stock.html");
            sw.Write("<html>");
            sw.Write("<title> Stock de la bibliothèque </title>");
            sw.Write("<meta charset = 'utf-8'>");
            sw.Write("<link rel='shortcut icon' href='" + System.Windows.Forms.Application.StartupPath + "\\39859.png'>");
            sw.Write("<link rel='stylesheet' href='" + System.Windows.Forms.Application.StartupPath + "\\index.css'>");
            sw.Write("<body>");
            sw.Write("<table class='content-table'>");
            sw.Write("<thead>");
            sw.Write("<tr><th>Auteur</th><th>Titre</th><th>Statut (dispo = 0)</th><th>Maison d'édition</th></tr>");
            sw.Write("</thead>");
            sw.Write("<tbody>");
            foreach (C_TB_livre l in prev_livre.BcpLivres)
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
        }
    }
}
