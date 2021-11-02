using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WPFBD_GD_1ER.Model;

namespace WPFBD_GD_1ER.ViewModel
{
    internal class VM_Edition : BasePropriete
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

        private C_TB_edition _EditionSelectionne;

        public C_TB_edition EditionSelectionne
        {
            get { return _EditionSelectionne; }
            set { AssignerChamp<C_TB_edition>(ref _EditionSelectionne, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        #endregion Données Écran

        #region Données extérieures

        private VM_UneEdition _UneEdition;

        public VM_UneEdition UneEdition
        {
            get { return _UneEdition; }
            set { AssignerChamp<VM_UneEdition>(ref _UneEdition, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        private ObservableCollection<C_TB_edition> _BcpEditions = new ObservableCollection<C_TB_edition>();

        public ObservableCollection<C_TB_edition> BcpEditions
        {
            get { return _BcpEditions; }
            set { _BcpEditions = value; }
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

        public VM_Edition()
        {
            UneEdition = new VM_UneEdition();
            UneEdition.ID = 99;
            UneEdition.Nom = "Nom maison...";
            UneEdition.Dat = DateTime.Today;
            BcpEditions = ChargerEdition(chConnexion);
            ActiverUneFiche = false;
            cConfirmer = new BaseCommande(Confirmer);
            cAnnuler = new BaseCommande(Annuler);
            cAjouter = new BaseCommande(Ajouter);
            cModifier = new BaseCommande(Modifier);
            cSupprimer = new BaseCommande(Supprimer);
            cEssaiSelMult = new BaseCommande(EssaiSelMult);
        }

        private ObservableCollection<C_TB_edition> ChargerEdition(string chConn)
        {
            ObservableCollection<C_TB_edition> rep = new ObservableCollection<C_TB_edition>();
            List<C_TB_edition> lTmp = new Model.G_TB_edition(chConn).Lire("ID_client");
            foreach (C_TB_edition Tmp in lTmp)
                rep.Add(Tmp);
            return rep;
        }

        public void Confirmer()
        {
            string nompdg, prenompdg;
            if (UneEdition.NomP == "")
            {
                nompdg = null;
            }
            else
            {
                nompdg = UneEdition.NomP;
            }
            if (UneEdition.PreP == "")
            {
                prenompdg = null;
            }
            else
            {
                prenompdg = UneEdition.PreP;
            }
            if (nAjout == -1)
            {
                UneEdition.ID = new G_TB_edition(chConnexion).Ajouter(UneEdition.Nom, UneEdition.Dat, nompdg, prenompdg);
                BcpEditions.Add(new C_TB_edition(UneEdition.ID, UneEdition.Nom, UneEdition.Dat, nompdg, prenompdg));
            }
            else
            {
                new G_TB_edition(chConnexion).Modifier(UneEdition.ID, UneEdition.Nom, UneEdition.Dat, nompdg, prenompdg);
                BcpEditions[nAjout] = new C_TB_edition(UneEdition.ID, UneEdition.Nom, UneEdition.Dat, nompdg, prenompdg);
            }
            ActiverUneFiche = false;
        }

        public void Annuler()
        { ActiverUneFiche = false; }

        public void Ajouter()
        {
            UneEdition = new VM_UneEdition();
            UneEdition.Dat = DateTime.Today;
            nAjout = -1;
            ActiverUneFiche = true;
        }

        public void Modifier()
        {
            if (EditionSelectionne != null)
            {
                C_TB_edition Tmp = new Model.G_TB_edition(chConnexion).Lire_ID(EditionSelectionne.ID_edition);
                UneEdition = new VM_UneEdition();
                UneEdition.ID = Tmp.ID_edition;
                UneEdition.Nom = Tmp.edi_nom;
                UneEdition.Dat = Tmp.edi_dat;
                UneEdition.NomP = Tmp.edi_pdg_nom;
                UneEdition.PreP = Tmp.edi_pdg_prenom;
                nAjout = BcpEditions.IndexOf(EditionSelectionne);
                ActiverUneFiche = true;
            }
        }

        public void Supprimer()
        {
            if (EditionSelectionne != null)
            {
                new Model.G_TB_edition(chConnexion).Supprimer(EditionSelectionne.ID_edition);
                BcpEditions.Remove(EditionSelectionne);
            }
        }

        public void EssaiSelMult(object lListe)
        {
            System.Collections.IList lTmp = (System.Collections.IList)lListe;
            foreach (C_TB_edition p in lTmp)
            { string s = p.edi_nom; }
            int nTmp = lTmp.Count;
        }

        public void EditionSelectionnee2UneEdition()
        {
            UneEdition.ID = EditionSelectionne.ID_edition;
            UneEdition.Nom = EditionSelectionne.edi_nom;
            UneEdition.Dat = EditionSelectionne.edi_dat;
            UneEdition.NomP = EditionSelectionne.edi_pdg_nom;
            UneEdition.PreP = EditionSelectionne.edi_pdg_prenom;
        }
    }

    public class VM_UneEdition : BasePropriete
    {
        private int _ID;
        private string _Nom;
        private DateTime _Dat;
        private string _NomP, _PreP;

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

        public DateTime Dat
        {
            get { return _Dat; }
            set { AssignerChamp<DateTime>(ref _Dat, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public string NomP
        {
            get { return _NomP; }
            set { AssignerChamp<string>(ref _NomP, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public string PreP
        {
            get { return _PreP; }
            set { AssignerChamp<string>(ref _PreP, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }
    }
}