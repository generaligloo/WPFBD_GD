using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using WPFBD_GD_1ER.Model;

namespace WPFBD_GD_1ER.ViewModel
{
    internal class VM_Detail : BasePropriete
    {
        #region Données Écran

        private string chConnexion = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='" + System.Windows.Forms.Application.StartupPath + @"\bibliotheque.mdf';Integrated Security=True;Connect Timeout=30";
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
        
        private bool _ActiverAmende;

        public bool ActiverAmende
        {
            get { return _ActiverAmende; }
            set { AssignerChamp<bool>(ref _ActiverAmende, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }
        private bool _ActiverRendre;

        public bool ActiverRendre
        {
            get { return _ActiverRendre; }
            set { AssignerChamp<bool>(ref _ActiverRendre, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        private C_TB_details _DetailSelectionne;

        public C_TB_details DetailSelectionne
        {
            get { return _DetailSelectionne; }
            set { AssignerChamp<C_TB_details>(ref _DetailSelectionne, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        private AffichLivreDetail _LivreDetailSelectionne;

        public AffichLivreDetail LivreDetailSelectionne
        {
            get { return _LivreDetailSelectionne; }
            set { AssignerChamp<AffichLivreDetail>(ref _LivreDetailSelectionne, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public C_TB_location LocaAct;

        public C_TB_details LocaDet { get; }

        private VM_Livre LivresAct;

        #endregion Données Écran

        #region Données extérieures

        private VM_UnDetail _UnDetail;

        public VM_UnDetail UnDetail
        {
            get { return _UnDetail; }
            set { AssignerChamp<VM_UnDetail>(ref _UnDetail, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        private ObservableCollection<C_TB_details> _BcpDetails = new ObservableCollection<C_TB_details>();

        public ObservableCollection<C_TB_details> BcpDetails
        {
            get { return _BcpDetails; }
            set { _BcpDetails = value; }
        }

        public ObservableCollection<AffichLivreDetail> Prev_Detail
        {
            get { return _Prev_Detail; }
            set { _Prev_Detail = value; }
        }

        private ObservableCollection<AffichLivreDetail> _Prev_Detail = new ObservableCollection<AffichLivreDetail>();

        #endregion Données extérieures

        #region Commandes

        public BaseCommande cConfirmer { get; set; }
        public BaseCommande cPayer { get; set; }
        public BaseCommande cRendre { get; set; }
        public BaseCommande cChanger { get; set; }
        public BaseCommande cValiderDate { get; set; }

        #endregion Commandes

        public VM_Detail()
        {
            UnDetail = new VM_UnDetail();
            UnDetail.ID = 99;
            UnDetail.IDLI = 99;
            UnDetail.IDLO = 99;
            UnDetail.Datemp = DateTime.Today;
            UnDetail.Datlim = DateTime.Today;
            UnDetail.Datren = DateTime.Today;
            BcpDetails = ChargerDetail(chConnexion);
            ActiverUneFiche = false;
            cConfirmer = new BaseCommande(Confirmer);
            cValiderDate = new BaseCommande(ValiderDate);
        }

        public VM_Detail(int lID)
        {
            LocaAct = new Model.G_TB_location(chConnexion).Lire_ID(lID);
            UnDetail = new VM_UnDetail();
            LivresAct = new VM_Livre();
            UnDetail.ID = 99;
            UnDetail.IDLI = 99;
            UnDetail.IDLO = 99;
            UnDetail.Datemp = DateTime.Today;
            UnDetail.Datlim = DateTime.Now.AddMonths(1);
            UnDetail.Datren = DateTime.Today;
            Prev_Detail = ChargerDetailPrev(chConnexion, LivresAct, LocaAct);
            ActiverUneFiche = false;
            cConfirmer = new BaseCommande(Confirmer);
            cPayer = new BaseCommande(Payer);
            cRendre = new BaseCommande(Rendre);
            cChanger = new BaseCommande(Changer);
        }

        public VM_Detail(int lID, int dID)
        {
            LocaAct = new Model.G_TB_location(chConnexion).Lire_ID(lID);
            LocaDet = new G_TB_details(chConnexion).Lire_ID(dID);
            ActiverUneFiche = false;
            cValiderDate = new BaseCommande(ValiderDate);
        }

        private ObservableCollection<C_TB_details> ChargerDetail(string chConn)
        {
            ObservableCollection<C_TB_details> rep = new ObservableCollection<C_TB_details>();
            List<C_TB_details> lTmp = new Model.G_TB_details(chConn).Lire("ID_details");
            foreach (C_TB_details Tmp in lTmp)
                rep.Add(Tmp);
            return rep;
        }

        private ObservableCollection<AffichLivreDetail> ChargerDetailPrev(string chConn, VM_Livre prev_liv, Model.C_TB_location locaAct)
        {
            ObservableCollection<AffichLivreDetail> rep = new ObservableCollection<AffichLivreDetail>();
            List<C_TB_details> lTmp = new Model.G_TB_details(chConn).Lire("ID_details");
            foreach (C_TB_details Tmp in lTmp)
            {
                if (Tmp.ID_location == locaAct.ID_location)
                {
                    AffichLivreDetail affichTmp = new AffichLivreDetail();
                    affichTmp.detailAff = Tmp;
                    foreach (C_TB_livre tmpPrevL in prev_liv.BcpLivres)
                    {
                        if (tmpPrevL.ID_livre == affichTmp.detailAff.ID_livre)
                        {
                            affichTmp.titre_auteur_livre = tmpPrevL.titre + " - " + tmpPrevL.auteur;
                        }
                    }
                    rep.Add(affichTmp);
                }
            }
            return rep;
        }

        public void Confirmer()
        {
            C_TB_livre pegiverifL = new G_TB_livre(chConnexion).Lire_ID(UnDetail.IDLI);
            C_TB_client pegiverifC = new G_TB_client(chConnexion).Lire_ID(LocaAct.ID_client);
            if (pegiverifL.ID_categorie != null)
            {
                C_TB_categorie pegiverifCat = new G_TB_categorie(chConnexion).Lire_ID((int)pegiverifL.ID_categorie);
                if (pegiverifCat.Pegi == 1 && DateTime.Today.Year - pegiverifC.client_nai.Year <= 18)
                {
                    MessageBox.Show("Le client n'est pas majeur !");
                }
                else
                {
                    if (UnDetail.lim == true)
                    {
                        UnDetail.ID = new G_TB_details(chConnexion).Ajouter(LocaAct.ID_location, UnDetail.IDLI, DateTime.Today, UnDetail.Datlim, null, null);
                    }
                    else
                    {
                        UnDetail.ID = new G_TB_details(chConnexion).Ajouter(LocaAct.ID_location, UnDetail.IDLI, DateTime.Today, null, null, null);
                    }
                    Model.C_TB_livre tmp = new G_TB_livre(chConnexion).Lire_ID(UnDetail.IDLI);
                    new Model.G_TB_livre(chConnexion).Modifier(tmp.ID_livre, tmp.titre, tmp.auteur, tmp.ID_categorie, 1, tmp.Ann_pub, tmp.ID_edition);
                }
            }
            else
            {
                if (UnDetail.lim == true)
                {
                    UnDetail.ID = new G_TB_details(chConnexion).Ajouter(LocaAct.ID_location, UnDetail.IDLI, DateTime.Today, UnDetail.Datlim, null, null);
                }
                else
                {
                    UnDetail.ID = new G_TB_details(chConnexion).Ajouter(LocaAct.ID_location, UnDetail.IDLI, DateTime.Today, null, null, null);
                }
                Model.C_TB_livre tmp = new G_TB_livre(chConnexion).Lire_ID(UnDetail.IDLI);
                new Model.G_TB_livre(chConnexion).Modifier(tmp.ID_livre, tmp.titre, tmp.auteur, tmp.ID_categorie, 1, tmp.Ann_pub, tmp.ID_edition);
            }
        }

        public void Payer()
        {
            if (LivreDetailSelectionne != null)
            {
                if (MessageBox.Show("Voulez vous payer l'amende du détail ID:" + LivreDetailSelectionne.detailAff.ID_details.ToString()+" ?","Payer",MessageBoxButton.YesNo,MessageBoxImage.Question)==MessageBoxResult.Yes)
                {
                    new G_TB_details(chConnexion).Modifier(
                        LivreDetailSelectionne.detailAff.ID_details,
                        LivreDetailSelectionne.detailAff.ID_location,
                        LivreDetailSelectionne.detailAff.ID_livre,
                        LivreDetailSelectionne.detailAff.dat_emprunt,
                        LivreDetailSelectionne.detailAff.dat_limite,
                        LivreDetailSelectionne.detailAff.dat_rentre,
                        null);
                    if (MessageBox.Show("Rendre le livre (sinon changer la date)?", "Rendre", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        new G_TB_details(chConnexion).Modifier(
                        LivreDetailSelectionne.detailAff.ID_details,
                        LivreDetailSelectionne.detailAff.ID_location,
                        LivreDetailSelectionne.detailAff.ID_livre,
                        LivreDetailSelectionne.detailAff.dat_emprunt,
                        LivreDetailSelectionne.detailAff.dat_limite,
                        DateTime.Today,
                        null);
                        C_TB_livre tmp = new G_TB_livre(chConnexion).Lire_ID((int)LivreDetailSelectionne.detailAff.ID_livre);
                        new G_TB_livre(chConnexion).Modifier(tmp.ID_livre, tmp.titre, tmp.auteur, tmp.ID_categorie, 0, tmp.Ann_pub, tmp.ID_edition);
                    }
                    else
                    {
                        View.EcranChangeDate changeDat = new View.EcranChangeDate(LivreDetailSelectionne.detailAff.ID_location, LivreDetailSelectionne.detailAff.ID_details);
                        changeDat.ShowDialog();
                        
                    }
                }
            }
        }

        public void Rendre()
        {
            if (LivreDetailSelectionne != null)
            {
                if (ActiverAmende != true)
                {
                    if (MessageBox.Show("Rendre le livre ?", "Rendre", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        new G_TB_details(chConnexion).Modifier(
                        LivreDetailSelectionne.detailAff.ID_details,
                        LivreDetailSelectionne.detailAff.ID_location,
                        LivreDetailSelectionne.detailAff.ID_livre,
                        LivreDetailSelectionne.detailAff.dat_emprunt,
                        LivreDetailSelectionne.detailAff.dat_limite,
                        DateTime.Today,
                        null);
                        C_TB_livre tmp = new G_TB_livre(chConnexion).Lire_ID((int)LivreDetailSelectionne.detailAff.ID_livre);
                        new G_TB_livre(chConnexion).Modifier(tmp.ID_livre, tmp.titre, tmp.auteur, tmp.ID_categorie, 0, tmp.Ann_pub, tmp.ID_edition);
                        
                    }
                }
                else
                {
                    MessageBox.Show("Vous devez d'abord payer l'amende !", "rendre");
                }
            }
        }
        public void Changer()
        {
            if (LivreDetailSelectionne != null)
            {
                if (ActiverAmende != true)
                {
                    View.EcranChangeDate changeDat = new View.EcranChangeDate(LivreDetailSelectionne.detailAff.ID_location, LivreDetailSelectionne.detailAff.ID_details);
                    changeDat.ShowDialog();
                    Prev_Detail.Clear();
                    Prev_Detail = ChargerDetailPrev(chConnexion, LivresAct, LocaAct);
                }
                else
                {
                    MessageBox.Show("Vous devez d'abord payer l'amende !", "rendre");
                }
            }
        }
        public void ValiderDate(object i)
        {
            DateTime dTmp = (DateTime)i;
            new G_TB_details(chConnexion).Modifier(
                        LocaDet.ID_details,
                        LocaDet.ID_location,
                        LocaDet.ID_livre,
                        LocaDet.dat_emprunt,
                        dTmp,
                        null,
                        null);
        }
        public void VerifAmende(int? nID, int dID)
        {
            if(nID != null)
            {
                ActiverAmende = true;
            }
            else
            {
                ActiverAmende = false;
            }
            C_TB_livre verifL = new G_TB_livre(chConnexion).Lire_ID((int)LivreDetailSelectionne.detailAff.ID_livre);
            if(verifL.statut == 1)
            {
                ActiverRendre = true;
            }
            else
            {
                ActiverRendre = false;
            }
        }

    }

    public class VM_UnDetail : BasePropriete
    {
        private int _ID, _IDLO, _IDLI;
        private DateTime _Datemp;
        private DateTime? _Datlim, _Datren;
        private int? _amen;
        private bool _lim;

        public int ID
        {
            get { return _ID; }
            set { AssignerChamp<int>(ref _ID, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public int IDLO
        {
            get { return _IDLO; }
            set { AssignerChamp<int>(ref _IDLO, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public int IDLI
        {
            get { return _IDLI; }
            set { AssignerChamp<int>(ref _IDLI, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public bool lim
        {
            get { return _lim; }
            set { AssignerChamp<bool>(ref _lim, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public DateTime Datemp
        {
            get { return _Datemp; }
            set { AssignerChamp<DateTime>(ref _Datemp, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public DateTime? Datlim
        {
            get { return _Datlim; }
            set { AssignerChamp<DateTime?>(ref _Datlim, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public DateTime? Datren
        {
            get { return _Datren; }
            set { AssignerChamp<DateTime?>(ref _Datren, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public int? amen
        {
            get { return _amen; }
            set { AssignerChamp<int?>(ref _amen, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }
    }
}