using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFBD_GD_1ER.Model;

namespace WPFBD_GD_1ER.ViewModel
{
    internal class VM_TXT : BasePropriete
    {
        private string sCon = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='" + System.Windows.Forms.Application.StartupPath + @"\bibliotheque.mdf';Integrated Security=True;Connect Timeout=30";
        public VM_TXT()
        {
            cTXT = new BaseCommande(Generer);
        }

        public BaseCommande cTXT { get; set; }

        public void Generer()
        {
            List<C_TB_livre> lTmp = new G_TB_livre(sCon).Lire("ID_livre");
            List<C_TB_edition> eTmp = new G_TB_edition(sCon).Lire("ID_edition");
            List<C_TB_categorie> cTmp = new G_TB_categorie(sCon).Lire("ID_categorie");
            Microsoft.Win32.SaveFileDialog saveTXT = new Microsoft.Win32.SaveFileDialog();
            saveTXT.FileName = "Document"; // Default file name
            saveTXT.DefaultExt = ".txt"; // Default file extension
            saveTXT.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension
            if (saveTXT.ShowDialog() == true)
            {
                string sFichier = saveTXT.FileName;
                StreamWriter sw = new StreamWriter(sFichier);
                foreach (C_TB_livre livre in lTmp)
                {
                    sw.WriteLine("ID du livre:" + livre.ID_livre);
                    sw.WriteLine("Titre: " + livre.titre + "       " + "Auteur: " + livre.auteur);
                    sw.WriteLine("Année de publication: " + livre.Ann_pub);
                    foreach (C_TB_categorie cat in cTmp.Where(n => n.ID_categorie == livre.ID_categorie))
                    {
                        sw.WriteLine("Catégorie: " + cat.Nom);
                    }
                    foreach (C_TB_edition edi in eTmp.Where(n => n.ID_edition == livre.ID_edition))
                    {
                        sw.WriteLine("Maison d'édition: " + edi.edi_nom);
                    }
                    sw.WriteLine("Année de publication: " + livre.Ann_pub);
                    if (livre.statut == 0)
                    {
                        sw.WriteLine("Statut: Disponible");
                    }
                    else
                    {
                        List<C_TB_details> verif_detail = new G_TB_details(sCon).Lire("ID_details");
                        foreach (C_TB_details d in verif_detail.Where(n => n.ID_livre == livre.ID_livre))
                        {
                            C_TB_location verif_location = new G_TB_location(sCon).Lire_ID(d.ID_location);
                            C_TB_client verif_client = new G_TB_client(sCon).Lire_ID(verif_location.ID_client);
                            if (d.dat_limite != null)
                            {
                                sw.WriteLine("Statut: possédé par: " + verif_client.client_nom + " " + verif_client.client_prenom + " jusqu'au: " + d.dat_limite);
                            }
                            else
                            {
                                sw.WriteLine("Statut: possédé par: " + verif_client.client_nom + " " + verif_client.client_prenom + " jusqu'au: Indéfini");
                            }
                        }

                    }
                    sw.WriteLine("--------------------------------------------------------------------------------");
                }
                sw.Close();
            }
        }
    }
}
