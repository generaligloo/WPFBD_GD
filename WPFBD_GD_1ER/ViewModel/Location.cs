using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Documents;
using System.Windows;
using WPFBD_GD_1ER.Model;
using WPFBD_GD_1ER.View;
using ListItemrt = System.Windows.Documents.ListItem;
using Listrt = System.Windows.Documents.List;
using Paragraph = iTextSharp.text.Paragraph;
using Paragraphrt = System.Windows.Documents.Paragraph;
using ListItem = System.Windows.Documents.ListItem;
using List = iTextSharp.text.List;

namespace WPFBD_GD_1ER.ViewModel
{
    internal class VM_Location : BasePropriete
    {
        #region Données Écran

        private string chConnexion = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='" + System.Windows.Forms.Application.StartupPath + @"\bibliotheque.mdf';Integrated Security=True;Connect Timeout=30";
        private int nAjout;
        private bool _ActiverUneFiche;

        public bool ActiverUneFiche
        {
            get { return _ActiverUneFiche; }
            set
            {
                AssignerChamp<bool>(ref _ActiverUneFiche, value, System.Reflection.MethodBase.GetCurrentMethod().Name);
                ActiverBcpFiche = !ActiverUneFiche;
            }
        }

        private bool _ActiverBcpFiche;

        public bool ActiverBcpFiche
        {
            get { return _ActiverBcpFiche; }
            set { AssignerChamp<bool>(ref _ActiverBcpFiche, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        private C_TB_location _LocationSelectionne;

        public C_TB_location LocationSelectionne
        {
            get { return _LocationSelectionne; }
            set { AssignerChamp<C_TB_location>(ref _LocationSelectionne, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        #endregion Données Écran

        #region Données extérieures

        private VM_UneLocation _UneLocation;

        public VM_UneLocation UneLocation
        {
            get { return _UneLocation; }
            set { AssignerChamp<VM_UneLocation>(ref _UneLocation, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        private ObservableCollection<C_TB_location> _BcpLocations = new ObservableCollection<C_TB_location>();

        public ObservableCollection<C_TB_location> BcpLocations
        {
            get { return _BcpLocations; }
            set { _BcpLocations = value; }
        }

        #endregion Données extérieures

        #region Commandes

        public BaseCommande cConfirmer { get; set; }
        public BaseCommande cAnnuler { get; set; }
        public BaseCommande cAjouter { get; set; }
        public BaseCommande cModifier { get; set; }
        public BaseCommande cSupprimer { get; set; }
        public BaseCommande cGenBordereau { get; set; }
        public BaseCommande cAffDetails { get; set; }

        #endregion Commandes

        public VM_Location()
        {
            UneLocation = new VM_UneLocation();
            UneLocation.IDL = 99;
            UneLocation.IDC = 99;
            UneLocation.DatLoc = DateTime.Today;
            BcpLocations = ChargerLocation(chConnexion);
            ActiverUneFiche = false;
            cConfirmer = new BaseCommande(Confirmer);
            cAnnuler = new BaseCommande(Annuler);
            cAjouter = new BaseCommande(Ajouter);
            cModifier = new BaseCommande(Modifier);
            cSupprimer = new BaseCommande(Supprimer);
            cAffDetails = new BaseCommande(AffDetails);
            cGenBordereau = new BaseCommande(GenBordereau);
        }

        public VM_Location(int cID)
        {
            UneLocation = new VM_UneLocation();
            UneLocation.IDL = 99;
            UneLocation.IDC = cID;
            UneLocation.DatLoc = DateTime.Today;
            BcpLocations = ChargerLocationID(chConnexion, cID);
            ActiverUneFiche = false;
            cConfirmer = new BaseCommande(Confirmer);
            cAnnuler = new BaseCommande(Annuler);
            cAjouter = new BaseCommande(Ajouter);
            cModifier = new BaseCommande(Modifier);
            cSupprimer = new BaseCommande(Supprimer);
            cGenBordereau = new BaseCommande(GenBordereau);
            cAffDetails = new BaseCommande(AffDetails);
        }

        private ObservableCollection<C_TB_location> ChargerLocationID(string chConn, int cID)
        {
            ObservableCollection<C_TB_location> rep = new ObservableCollection<C_TB_location>();
            List<C_TB_location> lTmp = new Model.G_TB_location(chConn).Lire("ID_location");
            foreach (C_TB_location Tmp in lTmp)
            {
                if (Tmp.ID_client == cID)
                {
                    rep.Add(Tmp);
                }
            }
            return rep;
        }

        private ObservableCollection<C_TB_location> ChargerLocation(string chConn)
        {
            ObservableCollection<C_TB_location> rep = new ObservableCollection<C_TB_location>();
            List<C_TB_location> lTmp = new Model.G_TB_location(chConn).Lire("ID_location");
            foreach (C_TB_location Tmp in lTmp)
                rep.Add(Tmp);
            return rep;
        }

        public void Confirmer()
        {
            if (nAjout == -1)
            {
                UneLocation.IDL = new G_TB_location(chConnexion).Ajouter(UneLocation.IDC, UneLocation.DatLoc);
                BcpLocations.Add(new C_TB_location(UneLocation.IDL, UneLocation.IDC, UneLocation.DatLoc));
            }
            else
            {
                new G_TB_location(chConnexion).Modifier(UneLocation.IDL, UneLocation.IDC, UneLocation.DatLoc);
                BcpLocations[nAjout] = new C_TB_location(UneLocation.IDL, UneLocation.IDC, UneLocation.DatLoc);
            }
            ActiverUneFiche = false;
        }

        public void Annuler()
        { ActiverUneFiche = false; }

        public void Ajouter()
        {
            //UneLocation = new VM_UneLocation();
            //UneLocation.DatLoc = DateTime.Today;
            nAjout = -1;
            ActiverUneFiche = true;
        }

        public void Modifier()
        {
            if (LocationSelectionne != null)
            {
                C_TB_location Tmp = new Model.G_TB_location(chConnexion).Lire_ID(LocationSelectionne.ID_location);
                UneLocation = new VM_UneLocation();
                UneLocation.IDL = Tmp.ID_location;
                UneLocation.IDC = Tmp.ID_client;
                UneLocation.DatLoc = Tmp.dat_location;
                nAjout = BcpLocations.IndexOf(LocationSelectionne);
                ActiverUneFiche = true;
            }
        }

        public void Supprimer()
        {
            if (LocationSelectionne != null)
            {
                C_TB_client cTmp = new G_TB_client(chConnexion).Lire_ID(LocationSelectionne.ID_client);
                VM_Client cot = new VM_Client();
                C_TB_location lTmp = new G_TB_location(chConnexion).Lire_ID(LocationSelectionne.ID_location);
                bool retard_client = false;
                foreach (C_TB_client ret in cot.Lst_retard_cot)
                {
                    if (lTmp.ID_client == ret.ID_client)
                    {
                        retard_client = true;
                    }
                }
                if (retard_client == true)
                {
                    MessageBox.Show("Impossible de supprimer la location car ce client est en retard de cotisation.");
                }
                else
                {
                    if (MessageBox.Show("Supprimer la location N°" + lTmp.ID_location + " du client: " + lTmp.ID_client, "supprimer la location ?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        //vérifier que cette location n'a pas de livre non rentré
                        bool rentre = true;
                        List<C_TB_details> dTmp = new G_TB_details(chConnexion).Lire("ID_details");
                        foreach (C_TB_details d in dTmp.Where(n => n.ID_location == lTmp.ID_location))
                        {
                            if (d.dat_rentre == null)
                            {
                                rentre = false;
                            }
                        }
                        if (rentre == false)
                        {
                            MessageBox.Show("Livre(s) non rentré(s) ! Impossible de supprimer la location");
                        }
                        else
                        {
                            //supprime d'abord les détails pour éviter les erreurs puis la location
                            foreach (C_TB_details d in dTmp.Where(n => n.ID_location == lTmp.ID_location))
                            {
                                new G_TB_details(chConnexion).Supprimer(d.ID_details);
                            }
                            new Model.G_TB_location(chConnexion).Supprimer(LocationSelectionne.ID_location);
                            BcpLocations.Remove(LocationSelectionne);
                        }
                    }
                }
            }
        }

        public void LocationSelectionnee2UneLocation()
        {
            UneLocation.IDL = LocationSelectionne.ID_location;
            UneLocation.IDC = LocationSelectionne.ID_client;
            UneLocation.DatLoc = LocationSelectionne.dat_location;
        }

        private void GenBordereau(object i)
        {
            System.Collections.IList items = (System.Collections.IList)i;
            IEnumerable<C_TB_location> dtmp = items.Cast<C_TB_location>();
            C_TB_location tmp = dtmp.First();
            MessageBoxResult res = MessageBox.Show("Souhaitez vous générer un bordereau d'emprunt ?", "Location", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.Yes)
            {
                C_TB_location loca_bordeau = new G_TB_location(chConnexion).Lire_ID(tmp.ID_location);
                C_TB_client loca_bordeau_client = new G_TB_client(chConnexion).Lire_ID(loca_bordeau.ID_client);
                List<C_TB_details> list_det_bor = new G_TB_details(chConnexion).Lire("ID_details");
                var PDF = new Document();
                string chemin = System.Windows.Forms.Application.StartupPath;
                string fichier = chemin + "\\BordereauEmpr" + loca_bordeau_client.ID_client.ToString() + "_" + loca_bordeau.ID_location.ToString() + ".pdf";
                PdfWriter.GetInstance(PDF, new FileStream(fichier, FileMode.Create));
                PDF.Open();

                Image Logo = Image.GetInstance(chemin + "\\39859.png");
                Logo.Alignment = Image.TEXTWRAP | Image.ALIGN_RIGHT;
                Logo.ScaleAbsolute(50f, 50f);
                PDF.Add(Logo);
                Paragraph titre = new Paragraph("Bordereau d'emprunt de la bibliothèque")
                {
                    Alignment = Element.ALIGN_CENTER
                };
                PDF.Add(titre);

                Paragraph space = new Paragraph("")
                {
                    SpacingAfter = 10f,
                    SpacingBefore = 10f
                };
                PDF.Add(space);
                string info = "Nom: " + loca_bordeau_client.client_nom.ToString() + "  Prénom: " + loca_bordeau_client.client_prenom.ToString() +
                    "                Date de location : " + loca_bordeau.dat_location.ToString() + "\n ID location:" + loca_bordeau.ID_location.ToString();

                Paragraph Nom = new Paragraph(info)
                {
                    Alignment = Element.ALIGN_LEFT
                };
                PDF.Add(Nom);

                Paragraph space2 = new Paragraph("")
                {
                    SpacingAfter = 20f,
                    SpacingBefore = 20f
                };
                PDF.Add(space2);

                List lst = new List(List.UNORDERED, 10f);
                foreach (C_TB_details d in list_det_bor.Where(n => n.ID_location == tmp.ID_location))
                {
                    C_TB_livre ltmp = new G_TB_livre(chConnexion).Lire_ID((int)d.ID_livre);
                    C_TB_edition etmp = new G_TB_edition(chConnexion).Lire_ID(ltmp.ID_edition);
                    if (d.dat_limite == null)
                    {
                        string Detail = "  ID livre: " + d.ID_livre + " \nTitre: " + ltmp.titre + " \nAuteur: " + ltmp.auteur + " \nMaison d'édition: " + etmp.edi_nom + " \nDate limite: N/A\n\n";
                        lst.Add(Detail);
                    }
                    else
                    {
                        string Detail = "  ID livre: " + d.ID_livre + " \nTitre: " + ltmp.titre + " \nAuteur: " + ltmp.auteur + " \nMaison d'édition: " + etmp.edi_nom + " \nDate limite: " + d.dat_limite.ToString() + "\n\n";
                        lst.Add(Detail);
                    }
                }
                PDF.Add(lst);
                PDF.Close();
                MessageBox.Show("PDF généré dans le dossier " + chemin + " !");
            }
        }

        private void AffDetails(object i)
        {
            C_TB_location tmp = (C_TB_location)i;
            C_TB_location loca_selec = new G_TB_location(chConnexion).Lire_ID(tmp.ID_location);
            if (MessageBox.Show("Afficher le détail de: " + loca_selec.ID_location + " ?", "Cotisation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                EcranAfficherDetails es = new EcranAfficherDetails(chConnexion, loca_selec.ID_location);
                es.ShowDialog();
            }
        }

        public FlowDocument GenererFlow()
        {
            FlowDocument fd = new FlowDocument();
            Paragraphrt p = new Paragraphrt();
            p.Inlines.Add(new Bold(new Run("Table de gestion des locations")));
            p.Inlines.Add(new LineBreak());
            p.Inlines.Add(new Run("Liste des locations"));
            fd.Blocks.Add(p);
            Listrt l = new Listrt();
            foreach (Model.C_TB_location cp in BcpLocations)
            {
                Model.C_TB_client cTmp = new Model.G_TB_client(chConnexion).Lire_ID(cp.ID_client);
                Paragraphrt pl = new Paragraphrt(new Run("(" + cp.ID_location + ") id du client:" + cp.ID_client + " - " + cTmp.client_nom + " " + cTmp.client_prenom +
                    " date de location: " + cp.dat_location.Value.Year.ToString() + "-" + cp.dat_location.Value.Month.ToString() + "-" + cp.dat_location.Value.Day.ToString()));
                l.ListItems.Add(new ListItem(pl));
            }
            fd.Blocks.Add(l);
            return fd;
        }
    }

    public class VM_UneLocation : BasePropriete
    {
        private int _IDL, _IDC;
        private DateTime? _DatLoc;

        public int IDL
        {
            get { return _IDL; }
            set { AssignerChamp<int>(ref _IDL, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public int IDC
        {
            get { return _IDC; }
            set { AssignerChamp<int>(ref _IDC, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public DateTime? DatLoc
        {
            get { return _DatLoc; }
            set { AssignerChamp<DateTime?>(ref _DatLoc, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }
    }
}