using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Forms;
using WPFBD_GD_1ER.Model;
using ListItemrt = System.Windows.Documents.ListItem;
using Listrt = System.Windows.Documents.List;
using Paragraph = iTextSharp.text.Paragraph;
using Paragraphrt = System.Windows.Documents.Paragraph;

namespace WPFBD_GD_1ER.ViewModel
{
    internal class VM_Livre : BasePropriete
    {
        #region Données Écran

        private string chConnexion = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='" + System.Windows.Forms.Application.StartupPath + @"\bibliotheque.mdf';Integrated Security=True;Connect Timeout=30";
        private int nAjout;
        private int idd;
        private bool _ActiverUneFiche;
        private VM_Categorie prev_cat;
        private VM_Edition prev_edi;

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

        private C_TB_livre _LivreSelectionne;

        public C_TB_livre LivreSelectionne
        {
            get { return _LivreSelectionne; }
            set { AssignerChamp<C_TB_livre>(ref _LivreSelectionne, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        #endregion Données Écran

        #region Données extérieures

        private VM_UnLivre _UnLivre;

        public VM_UnLivre UnLivre
        {
            get { return _UnLivre; }
            set { AssignerChamp<VM_UnLivre>(ref _UnLivre, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        private ObservableCollection<C_TB_livre> _BcpLivres = new ObservableCollection<C_TB_livre>();

        public ObservableCollection<C_TB_livre> BcpLivres
        {
            get { return _BcpLivres; }
            set { _BcpLivres = value; }
        }
        private ObservableCollection<C_TB_livre> _BcpLivresDispo = new ObservableCollection<C_TB_livre>();
        public ObservableCollection<C_TB_livre> BcpLivresDispo
        {
            get { return _BcpLivresDispo; }
            set { _BcpLivresDispo = value; }
        }

        public ObservableCollection<AffichLivre> Prev_Livre
        {
            get { return _Prev_Livre; }
            set { _Prev_Livre = value; }
        }

        private ObservableCollection<AffichLivre> _Prev_Livre = new ObservableCollection<AffichLivre>();

        #endregion Données extérieures

        #region Commandes

        public BaseCommande cConfirmer { get; set; }
        public BaseCommande cAnnuler { get; set; }
        public BaseCommande cAjouter { get; set; }
        public BaseCommande cModifier { get; set; }
        public BaseCommande cSupprimer { get; set; }

        #endregion Commandes

        public VM_Livre()
        {
            prev_cat = new VM_Categorie();
            prev_edi = new VM_Edition();
            UnLivre = new VM_UnLivre();
            UnLivre.ID = 99;
            UnLivre.Titre = "Livre...";
            UnLivre.Auteur = "Auteur ...";
            UnLivre.Pub = 2000;
            BcpLivres = ChargerLivres(chConnexion);
            Prev_Livre = ChargerLivresPrev(chConnexion, prev_cat, prev_edi);
            BcpLivresDispo = ChargerLivresDispo(chConnexion);
            ActiverUneFiche = false;
            cConfirmer = new BaseCommande(Confirmer);
            cAnnuler = new BaseCommande(Annuler);
            cAjouter = new BaseCommande(Ajouter);
            cModifier = new BaseCommande(Modifier);
            cSupprimer = new BaseCommande(Supprimer);
        }

        private ObservableCollection<C_TB_livre> ChargerLivresDispo(string chConnexion)
        {
            ObservableCollection<C_TB_livre> rep = new ObservableCollection<C_TB_livre>();
            List<C_TB_livre> lTmp = new G_TB_livre(chConnexion).Lire("ID_livre");
            foreach (C_TB_livre Tmp in lTmp)
            {
                if (Tmp.statut == 0)
                {
                    rep.Add(Tmp);
                }
            }
            return rep;
        }

        private ObservableCollection<AffichLivre> ChargerLivresPrev(string chConn, VM_Categorie prev_cat, VM_Edition prev_edi)
        {
            ObservableCollection<AffichLivre> rep = new ObservableCollection<AffichLivre>();
            List<C_TB_livre> lTmp = new G_TB_livre(chConn).Lire("ID_livre");
            foreach (C_TB_livre Tmp in lTmp)
            {
                AffichLivre AffichTmp = new AffichLivre();
                AffichTmp.livreAff = Tmp;
                if (Tmp.ID_categorie != null)
                {
                    foreach (C_TB_categorie tmpPrevC in prev_cat.BcpCategories)
                    {
                        if (tmpPrevC.ID_categorie == AffichTmp.livreAff.ID_categorie)
                        {
                            AffichTmp.nomCat = tmpPrevC.Nom;
                        }
                    }
                }
                else
                {
                    AffichTmp.nomCat = "Non catégorisé";
                }
                foreach (C_TB_edition tmpPrevE in prev_edi.BcpEditions)
                {
                    if (tmpPrevE.ID_edition == AffichTmp.livreAff.ID_edition)
                    {
                        AffichTmp.nomEdi = tmpPrevE.edi_nom;
                    }
                }
                rep.Add(AffichTmp);
            }
            return rep;
        }

        private ObservableCollection<C_TB_livre> ChargerLivres(string chConn)
        {
            ObservableCollection<C_TB_livre> rep = new ObservableCollection<C_TB_livre>();
            List<C_TB_livre> lTmp = new Model.G_TB_livre(chConn).Lire("ID_livre");
            foreach (C_TB_livre Tmp in lTmp)
                rep.Add(Tmp);
            return rep;
        }

        public void Confirmer()
        {
            if (nAjout == -1)
            {
                //MessageBox.Show(UnLivre.Titre +"/"+ UnLivre.Auteur + "/" + UnLivre.IDC + "/" + UnLivre.Statut + "/" + UnLivre.Pub + "/" + UnLivre.IDE);
                UnLivre.ID = new G_TB_livre(chConnexion).Ajouter(UnLivre.Titre, UnLivre.Auteur, UnLivre.IDC, UnLivre.Statut, UnLivre.Pub, UnLivre.IDE);
                BcpLivres.Add(new C_TB_livre(UnLivre.ID, UnLivre.Titre, UnLivre.Auteur, UnLivre.IDC, UnLivre.Statut, UnLivre.Pub, UnLivre.IDE));
                MessageBoxResult res = System.Windows.MessageBox.Show("Souhaitez vous générer un bordereau de commande ?", "Commande", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (res == MessageBoxResult.Yes)
                {
                    C_TB_edition edit_bordeau = new G_TB_edition(chConnexion).Lire_ID(UnLivre.IDE);
                    C_TB_categorie cate_bordeau_client = new C_TB_categorie();
                    if (UnLivre.IDC != null)
                    {
                        cate_bordeau_client = new G_TB_categorie(chConnexion).Lire_ID((int)UnLivre.IDC);
                    }
                    var PDF = new Document();
                    string chemin = System.Windows.Forms.Application.StartupPath;
                    string fichier = chemin + "\\Bordereau" + UnLivre.ID.ToString() + "_" + UnLivre.IDE.ToString() + ".pdf";
                    PdfWriter.GetInstance(PDF, new FileStream(fichier, FileMode.Create));
                    PDF.Open();

                    Image Logo = Image.GetInstance(chemin + "\\39859.png");
                    Logo.Alignment = Image.TEXTWRAP | Image.ALIGN_RIGHT;
                    Logo.ScaleAbsolute(50f, 50f);
                    PDF.Add(Logo);
                    Font titreF = FontFactory.GetFont("Arial", 30, Font.BOLD, BaseColor.BLACK);
                    Font titreF2 = FontFactory.GetFont("Arial", 25, Font.BOLD, BaseColor.BLACK);
                    Paragraph titre = new Paragraph("Bordereau de commande de la maison d'édition", titreF)
                    {
                        Alignment = Element.ALIGN_CENTER,

                    };
                    PDF.Add(titre);

                    Paragraph space = new Paragraph("")
                    {
                        SpacingAfter = 10f,
                        SpacingBefore = 10f
                    };
                    PDF.Add(space);
                    string info = "\nTitre: " + UnLivre.Titre.ToString() + "  \nAuteur: " + UnLivre.Auteur.ToString() + "\nDate de commande : " + DateTime.Today.ToString() +
                        "\nAnné de publication: " + UnLivre.Pub.ToString() + "\nID Catégorie: " + UnLivre.IDC.ToString() + "\nID édition: " + UnLivre.IDE.ToString();
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

                    string ct = "\nDétails de commande : \nCatégorie:";
                    Paragraph dta = new Paragraph(ct, titreF2)
                    {
                        Alignment = Element.ALIGN_LEFT
                    };
                    PDF.Add(dta);
                    string Cat;
                    if (UnLivre.IDC == null)
                    {
                        Cat = "Non catégorisé";
                    }
                    else
                    {
                        if (cate_bordeau_client.Pegi == 1)
                        {
                            Cat = "ID catégorie: " + cate_bordeau_client.ID_categorie.ToString() + " \nNom: " + cate_bordeau_client.Nom.ToString() + " \nPegi: +18";
                        }
                        else
                        {
                            Cat = "ID catégorie: " + cate_bordeau_client.ID_categorie.ToString() + " \nNom: " + cate_bordeau_client.Nom.ToString() + " \nPegi: non";
                        }
                    }
                    Paragraph Catt = new Paragraph(Cat)
                    {
                        Alignment = Element.ALIGN_LEFT
                    };
                    PDF.Add(Catt);
                    string dt = "Maison d'édition:";
                    Paragraph dtb = new Paragraph(dt, titreF2)
                    {
                        Alignment = Element.ALIGN_LEFT
                    };
                    PDF.Add(dtb);
                    string Edi;
                    if (edit_bordeau.edi_pdg_nom == "")
                    {
                        Edi = "ID édition: " + edit_bordeau.ID_edition.ToString() + " \nNom d'édition: " + edit_bordeau.edi_nom.ToString() + " \ndate de création: " + edit_bordeau.edi_dat.ToString();
                    }
                    else
                    {
                        Edi = "ID édition: " + edit_bordeau.ID_edition.ToString() + " \nNom d'édition: " + edit_bordeau.edi_nom.ToString() + " \ndate de création: " + edit_bordeau.edi_dat.ToString() + " \nPDG: " + edit_bordeau.edi_pdg_nom.ToString() + " " + edit_bordeau.edi_pdg_prenom.ToString();
                    }

                    Paragraph Editt = new Paragraph(Edi)
                    {
                        Alignment = Element.ALIGN_LEFT
                    };
                    PDF.Add(Editt);
                    PDF.Close();
                    System.Windows.MessageBox.Show("PDF généré dans le dossier " + chemin + " !");
                }
                MessageBoxResult res2 = System.Windows.MessageBox.Show("Souhaitez vous prévenir par mail tout les clients du nouveau livre disponible?", "Commande", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (res2 == MessageBoxResult.Yes)
                {
                    string mail = "Le livre " + UnLivre.Titre.ToString() + " de " + UnLivre.Auteur.ToString() + " est maintenant disponible en biblothèque !";
                    string sujet = "Nouveauté à la bibliothèque";
                    List<C_TB_client> listCli = new G_TB_client(chConnexion).Lire("ID_client");
                    foreach (C_TB_client client in listCli)
                    {
                        MailMessage msg = new MailMessage("bibliogd3008@gmail.com", client.client_mail, sujet, mail);
                        SmtpClient cli = new SmtpClient("smtp.gmail.com");
                        cli.Port = 587;
                        cli.Credentials = new System.Net.NetworkCredential("bibliogd3008@gmail.com", "1234567890GD");
                        cli.EnableSsl = true;
                        System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object s,
                        System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                        System.Security.Cryptography.X509Certificates.X509Chain chain,
                        System.Net.Security.SslPolicyErrors sslPolicyErrors)
                        {
                            return true;
                        };
                        cli.Send(msg);
                    }
                    System.Windows.MessageBox.Show("Les mails ont été envoyés correctement !");
                }
            }
            else
            {
                new G_TB_livre(chConnexion).Modifier(UnLivre.ID, UnLivre.Titre, UnLivre.Auteur, UnLivre.IDC, UnLivre.Statut, UnLivre.Pub, UnLivre.IDE);
                BcpLivres[nAjout] = new C_TB_livre(UnLivre.ID, UnLivre.Titre, UnLivre.Auteur, UnLivre.IDC, UnLivre.Statut, UnLivre.Pub, UnLivre.IDE);
            }
            ActiverUneFiche = false;
        }

        public void Annuler()
        { ActiverUneFiche = false; }

        public void Ajouter()
        {
            UnLivre = new VM_UnLivre();
            nAjout = -1;
            ActiverUneFiche = true;
        }

        public void Modifier()
        {
            if (LivreSelectionne != null)
            {
                C_TB_livre Tmp = new Model.G_TB_livre(chConnexion).Lire_ID(LivreSelectionne.ID_livre);
                UnLivre = new VM_UnLivre();
                UnLivre.ID = Tmp.ID_livre;
                UnLivre.Titre = Tmp.titre;
                UnLivre.Auteur = Tmp.auteur;
                UnLivre.IDC = Tmp.ID_categorie;
                UnLivre.IDE = Tmp.ID_edition;
                UnLivre.Statut = Tmp.statut;
                UnLivre.Pub = Tmp.Ann_pub;
                nAjout = BcpLivres.IndexOf(LivreSelectionne);
                ActiverUneFiche = true;
            }
        }

        public void Supprimer()
        {
            if (LivreSelectionne != null)
            {
                if (System.Windows.Forms.MessageBox.Show("Supprimer ?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    int iID = LivreSelectionne.ID_livre;
                    C_TB_livre TestDispo = new G_TB_livre(chConnexion).Lire_ID(iID);
                    bool supp_poss = true;
                    List<C_TB_details> dTmp = new G_TB_details(chConnexion).Lire("ID_details"); //parcourt les détails 
                    foreach (C_TB_details c in dTmp)
                    {
                        if (c.ID_livre == iID)
                        {
                            supp_poss = false;
                            idd = c.ID_details;
                        }
                    }
                    if (TestDispo.statut == 1) //seulement si le livre est disponible et que les details le concernant n'existe pas
                    {
                        System.Windows.Forms.MessageBox.Show("Erreur le livre n'est pas disponible !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (supp_poss == false) //détail avec ID_livre (éviter les erreurs)
                    {
                        System.Windows.Forms.MessageBox.Show("Un détail (ID:" + idd + ") utilise le livre !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        new Model.G_TB_livre(chConnexion).Supprimer(iID);
                        BcpLivres.Remove(LivreSelectionne);
                    }
                }
            }
        }

        public void LivreSelectionnee2UnLivre(bool st)
        {
            UnLivre.ID = LivreSelectionne.ID_livre;
            UnLivre.Titre = LivreSelectionne.titre;
            UnLivre.Auteur = LivreSelectionne.auteur;
            UnLivre.IDC = LivreSelectionne.ID_categorie;
            UnLivre.IDE = LivreSelectionne.ID_edition;
            if (st == true)
            {
                UnLivre.Statut = 1;
            }
            else
            {
                UnLivre.Statut = 0;
            }
        }
        public bool CB_Dispo_verif(int nID) 
        {
            C_TB_livre check_livre = new G_TB_livre(chConnexion).Lire_ID(nID);
            if (check_livre.statut == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool CB_Pegi_verif(int nID)
        {
            C_TB_livre check_livre = new G_TB_livre(chConnexion).Lire_ID(nID);
            if (check_livre.ID_categorie != null)
            {
                C_TB_categorie check_cat = new G_TB_categorie(chConnexion).Lire_ID((int)check_livre.ID_categorie);
                if (check_cat.Pegi == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public bool CB_Retard_verif(int nID)
        {
            List<C_TB_details> lst_verif_rendu = new G_TB_details(chConnexion).Lire("ID_details");
            foreach (C_TB_details d in lst_verif_rendu.Where(n => n.ID_livre == nID))
            {
                if (d.dat_limite != null)
                {
                    int rComp = DateTime.Compare(d.dat_limite.Value, DateTime.Today);
                    if (rComp < 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public FlowDocument GenererFlow()
        {
            FlowDocument fd = new FlowDocument();
            Paragraphrt p = new Paragraphrt();
            p.Inlines.Add(new Bold(new Run("Table de gestion des livres")));
            p.Inlines.Add(new LineBreak());
            p.Inlines.Add(new Run("Liste des livres encodées"));
            fd.Blocks.Add(p);
            Listrt l = new Listrt();
            foreach (Model.C_TB_livre cp in BcpLivres)
            {
                if (cp.statut == 1)
                {
                    Paragraphrt pl = new Paragraphrt(new Run("(" + cp.ID_livre + "): " + cp.titre + " - " + cp.ID_livre
                     + " (indisponible) "));
                    l.ListItems.Add(new ListItemrt(pl));
                }
                else
                {
                    Paragraphrt pl = new Paragraphrt(new Run("(" + cp.ID_livre + "): " + cp.titre + " - " + cp.ID_livre
                     + " (disponible) "));
                    l.ListItems.Add(new ListItemrt(pl));
                }
            }
            fd.Blocks.Add(l);
            return fd;
        }
    }

    public class VM_UnLivre : BasePropriete
    {
        private int _ID, _IDE;
        private int? _IDC, _Pub;
        private string _Titre, _Auteur;
        private short _Statut;

        public int ID
        {
            get { return _ID; }
            set { AssignerChamp<int>(ref _ID, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public int? IDC
        {
            get { return _IDC; }
            set { AssignerChamp<int?>(ref _IDC, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public int IDE
        {
            get { return _IDE; }
            set { AssignerChamp<int>(ref _IDE, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public string Titre
        {
            get { return _Titre; }
            set { AssignerChamp<string>(ref _Titre, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public string Auteur
        {
            get { return _Auteur; }
            set { AssignerChamp<string>(ref _Auteur, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public short Statut
        {
            get { return _Statut; }
            set { AssignerChamp<short>(ref _Statut, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public int? Pub
        {
            get { return _Pub; }
            set { AssignerChamp<int?>(ref _Pub, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }
    }
}