using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WPFBD_GD_1ER.Model;

namespace WPFBD_GD_1ER.ViewModel
{
    internal class VM_location : BasePropriete
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
        public BaseCommande cEssaiSelMult { get; set; }

        #endregion Commandes

        public VM_location()
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
            cEssaiSelMult = new BaseCommande(EssaiSelMult);
        }

        public VM_location(int cID)
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
            cEssaiSelMult = new BaseCommande(EssaiSelMult);
        }

        private ObservableCollection<C_TB_location> ChargerLocationID(string chConn, int cID)
        {
            ObservableCollection<C_TB_location> rep = new ObservableCollection<C_TB_location>();
            List<C_TB_location> lTmp = new Model.G_TB_location(chConn).Lire("ID_location");
            foreach(C_TB_location Tmp in lTmp)
            {
                if(Tmp.ID_client == cID)
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
                new Model.G_TB_location(chConnexion).Supprimer(LocationSelectionne.ID_location);
                BcpLocations.Remove(LocationSelectionne);
            }
        }

        public void EssaiSelMult(object lListe)
        {
            System.Collections.IList lTmp = (System.Collections.IList)lListe;
            foreach (C_TB_edition p in lTmp)
            { string s = p.edi_nom; }
            int nTmp = lTmp.Count;
        }

        public void LocationSelectionnee2UneLocation()
        {
            UneLocation.IDL = LocationSelectionne.ID_location;
            UneLocation.IDC = LocationSelectionne.ID_client;
            UneLocation.DatLoc = LocationSelectionne.dat_location;
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