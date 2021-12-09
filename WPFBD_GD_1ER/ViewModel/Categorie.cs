using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Documents;
using System.Windows.Forms;
using WPFBD_GD_1ER.Model;

namespace WPFBD_GD_1ER.ViewModel
{
    internal class VM_Categorie : BasePropriete
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

        private C_TB_categorie _CategorieSelectionne;

        public C_TB_categorie CategorieSelectionne
        {
            get { return _CategorieSelectionne; }
            set { AssignerChamp<C_TB_categorie>(ref _CategorieSelectionne, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        #endregion Données Écran

        #region Données extérieures

        private VM_UneCategorie _UneCategorie;

        public VM_UneCategorie UneCategorie
        {
            get { return _UneCategorie; }
            set { AssignerChamp<VM_UneCategorie>(ref _UneCategorie, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        private ObservableCollection<C_TB_categorie> _BcpCategories = new ObservableCollection<C_TB_categorie>();

        public ObservableCollection<C_TB_categorie> BcpCategories
        {
            get { return _BcpCategories; }
            set { _BcpCategories = value; }
        }

        #endregion Données extérieures

        #region Commandes

        public BaseCommande cConfirmer { get; set; }
        public BaseCommande cAnnuler { get; set; }
        public BaseCommande cAjouter { get; set; }
        public BaseCommande cModifier { get; set; }
        public BaseCommande cSupprimer { get; set; }

        #endregion Commandes

        public VM_Categorie()
        {
            UneCategorie = new VM_UneCategorie();
            UneCategorie.ID = 99;
            UneCategorie.Nom = "Catégorie...";
            BcpCategories = ChargerCategories(chConnexion);
            ActiverUneFiche = false;
            cConfirmer = new BaseCommande(Confirmer);
            cAnnuler = new BaseCommande(Annuler);
            cAjouter = new BaseCommande(Ajouter);
            cModifier = new BaseCommande(Modifier);
            cSupprimer = new BaseCommande(Supprimer);
        }

        private ObservableCollection<C_TB_categorie> ChargerCategories(string chConn)
        {
            ObservableCollection<C_TB_categorie> rep = new ObservableCollection<C_TB_categorie>();
            List<C_TB_categorie> lTmp = new Model.G_TB_categorie(chConn).Lire("ID_categorie");
            foreach (C_TB_categorie Tmp in lTmp)
                rep.Add(Tmp);
            return rep;
        }

        public void Confirmer()
        {
            if (nAjout == -1)
            {
                UneCategorie.ID = new G_TB_categorie(chConnexion).Ajouter(UneCategorie.Nom, UneCategorie.Pegi);
                BcpCategories.Add(new C_TB_categorie(UneCategorie.ID, UneCategorie.Nom, UneCategorie.Pegi));
            }
            else
            {
                new G_TB_categorie(chConnexion).Modifier(UneCategorie.ID, UneCategorie.Nom, UneCategorie.Pegi);
                BcpCategories[nAjout] = new C_TB_categorie(UneCategorie.ID, UneCategorie.Nom, UneCategorie.Pegi);
            }
            ActiverUneFiche = false;
        }

        public void Annuler()
        { ActiverUneFiche = false; }

        public void Ajouter()
        {
            UneCategorie = new VM_UneCategorie();
            nAjout = -1;
            ActiverUneFiche = true;
        }

        public void Modifier()
        {
            if (CategorieSelectionne != null)
            {
                C_TB_categorie Tmp = new Model.G_TB_categorie(chConnexion).Lire_ID(CategorieSelectionne.ID_categorie);
                UneCategorie = new VM_UneCategorie();
                UneCategorie.ID = Tmp.ID_categorie;
                UneCategorie.Nom = Tmp.Nom;
                UneCategorie.Pegi = Tmp.Pegi;
                nAjout = BcpCategories.IndexOf(CategorieSelectionne);
                ActiverUneFiche = true;
            }
        }

        public void Supprimer()
        {
            if (CategorieSelectionne != null)
            {
                bool verif_cat = true;
                List<C_TB_livre> livretmp = new G_TB_livre(chConnexion).Lire("ID_livre");
                for (int i = 0; i < livretmp.Count; i++)
                {
                    if (livretmp[i].ID_categorie == CategorieSelectionne.ID_categorie)
                    {
                        verif_cat = false;
                    }
                }
                if (verif_cat == true)
                {
                    if (MessageBox.Show("Supprimer ?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        new Model.G_TB_categorie(chConnexion).Supprimer(CategorieSelectionne.ID_categorie);
                        BcpCategories.Remove(CategorieSelectionne);
                    }
                }
                else
                {
                    MessageBox.Show("Des livres utilisent cette catégorie impossible de la supprimer !");
                }
                verif_cat = true;
            }
        }

        public void CategorieSelectionnee2UneCategorie(bool check)
        {
            UneCategorie.ID = CategorieSelectionne.ID_categorie;
            UneCategorie.Nom = CategorieSelectionne.Nom;
            if (check == true)
            {
                UneCategorie.Pegi = 1;
            }
            else
            {
                UneCategorie.Pegi = 0;
            }
        }

        public FlowDocument GenererFlow()
        {
            FlowDocument fd = new FlowDocument();
            Paragraph p = new Paragraph();
            p.Inlines.Add(new Bold(new Run("Table de gestion des catégories")));
            p.Inlines.Add(new LineBreak());
            p.Inlines.Add(new Run("Liste des catégories encodées"));
            fd.Blocks.Add(p);
            List l = new List();
            foreach (C_TB_categorie cp in BcpCategories)
            {
                if (cp.Pegi == 1)
                {
                    Paragraph pl = new Paragraph(new Run("(" + cp.ID_categorie + "): " + cp.Nom
                     + " (PEGI 18+) "));
                    l.ListItems.Add(new ListItem(pl));
                }
                else
                {
                    Paragraph pl = new Paragraph(new Run("(" + cp.ID_categorie + "): " + cp.Nom
                     + " () "));
                    l.ListItems.Add(new ListItem(pl));
                }
            }
            fd.Blocks.Add(l);
            return fd;
        }
    }

    public class VM_UneCategorie : BasePropriete
    {
        private int _ID;
        private string _Nom;
        private short _Pegi;

        public int ID
        {
            get { return _ID; }
            set { AssignerChamp<int>(ref _ID, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public string Nom
        {
            get { return _Nom; }
            set { AssignerChamp<string>(ref _Nom, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public short Pegi
        {
            get { return _Pegi; }
            set { AssignerChamp<short>(ref _Pegi, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }
    }
}