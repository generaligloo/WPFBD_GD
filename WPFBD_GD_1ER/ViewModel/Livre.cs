using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using WPFBD_GD_1ER.Model;

namespace WPFBD_GD_1ER.ViewModel
{
    internal class VM_Livre : BasePropriete
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

        #endregion Données extérieures

        #region Commandes

        public BaseCommande cConfirmer { get; set; }
        public BaseCommande cAnnuler { get; set; }
        public BaseCommande cAjouter { get; set; }
        public BaseCommande cModifier { get; set; }
        public BaseCommande cSupprimer { get; set; }
        public BaseCommande cEssaiSelMult { get; set; }

        #endregion Commandes

        public VM_Livre()
        {
            UnLivre = new VM_UnLivre();
            UnLivre.ID = 99;
            UnLivre.Titre = "Livre...";
            UnLivre.Auteur = "Auteur ...";
            UnLivre.Pub = 2000;
            BcpLivres = ChargerLivres(chConnexion);
            ActiverUneFiche = false;
            cConfirmer = new BaseCommande(Confirmer);
            cAnnuler = new BaseCommande(Annuler);
            cAjouter = new BaseCommande(Ajouter);
            cModifier = new BaseCommande(Modifier);
            cSupprimer = new BaseCommande(Supprimer);
            cEssaiSelMult = new BaseCommande(EssaiSelMult);    
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
                UnLivre.ID = new G_TB_livre(chConnexion).Ajouter(UnLivre.Titre, UnLivre.Auteur,UnLivre.IDC,UnLivre.Statut,UnLivre.Pub,UnLivre.IDE);
                BcpLivres.Add(new C_TB_livre(UnLivre.ID, UnLivre.Titre, UnLivre.Auteur, UnLivre.IDC, UnLivre.Statut, UnLivre.Pub, UnLivre.IDE));
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
                new Model.G_TB_livre(chConnexion).Supprimer(LivreSelectionne.ID_livre);
                BcpLivres.Remove(LivreSelectionne);
            }
        }

        public void EssaiSelMult(object lListe)
        {
            System.Collections.IList lTmp = (System.Collections.IList)lListe;
            foreach (C_TB_livre p in lTmp)
            { string s = p.titre; }
            int nTmp = lTmp.Count;
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
