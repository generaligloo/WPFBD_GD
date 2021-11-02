using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public BaseCommande cEssaiSelMult { get; set; }

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
            cEssaiSelMult = new BaseCommande(EssaiSelMult);
        }

        private ObservableCollection<C_TB_categorie> ChargerCategories(string chConn)
        {
            ObservableCollection<C_TB_categorie> rep = new ObservableCollection<C_TB_categorie>();
            List<C_TB_categorie> lTmp = new Model.G_TB_categorie(chConn).Lire("ID_client");
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
                new Model.G_TB_categorie(chConnexion).Supprimer(CategorieSelectionne.ID_categorie);
                BcpCategories.Remove(CategorieSelectionne);
            }
        }

        public void EssaiSelMult(object lListe)
        {
            System.Collections.IList lTmp = (System.Collections.IList)lListe;
            foreach (C_TB_categorie p in lTmp)
            { string s = p.Nom; }
            int nTmp = lTmp.Count;
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
    }

    public class VM_UneCategorie : BasePropriete
    {
        private int _ID;
        private string _Nom;

        //private bool _PegiC;
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

        //public bool PegiC
        //{
        //    get { return _PegiC; }
        //    set { Convert.ToBoolean(_Pegi); }
        //}
    }
}