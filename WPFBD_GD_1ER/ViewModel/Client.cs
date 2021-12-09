using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Documents;
using WPFBD_GD_1ER.Model;
using WPFBD_GD_1ER.View;

namespace WPFBD_GD_1ER.ViewModel
{
    internal class VM_Client : BasePropriete
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

        private C_TB_client _ClientSelectionne;

        public C_TB_client ClientSelectionne
        {
            get { return _ClientSelectionne; }
            set { AssignerChamp<C_TB_client>(ref _ClientSelectionne, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        private AffichClient _AClientSelectionne;

        public AffichClient AClientSelectionne
        {
            get { return _AClientSelectionne; }
            set { AssignerChamp<AffichClient>(ref _AClientSelectionne, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public List<C_TB_client> Lst_retard_cot = new List<C_TB_client>();

        #endregion Données Écran

        #region Données extérieures

        private VM_UnClient _UnClient;

        public VM_UnClient UnClient
        {
            get { return _UnClient; }
            set { AssignerChamp<VM_UnClient>(ref _UnClient, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        private ObservableCollection<C_TB_client> _BcpClients = new ObservableCollection<C_TB_client>();

        public ObservableCollection<C_TB_client> BcpClients
        {
            get { return _BcpClients; }
            set { _BcpClients = value; }
        }

        public ObservableCollection<AffichClient> Prev_Client
        {
            get { return _Prev_Client; }
            set { _Prev_Client = value; }
        }

        private ObservableCollection<AffichClient> _Prev_Client = new ObservableCollection<AffichClient>();

        #endregion Données extérieures

        #region Commandes

        public BaseCommande cConfirmer { get; set; }
        public BaseCommande cAnnuler { get; set; }
        public BaseCommande cAjouter { get; set; }
        public BaseCommande cModifier { get; set; }
        public BaseCommande cSupprimer { get; set; }
        public BaseCommande cReglecot { get; set; }
        public BaseCommande cAjLocation { get; set; }

        #endregion Commandes

        public VM_Client()
        {
            UnClient = new VM_UnClient();
            UnClient.ID = 24;
            UnClient.Pre = "Largo";
            UnClient.Nom = "Winch";
            UnClient.Nai = DateTime.Today;
            UnClient.Coti = DateTime.Today;
            UnClient.Crea = DateTime.Today;
            UnClient.Mail = "largowinch@gmail.com";
            BcpClients = ChargerClients(chConnexion);
            Prev_Client = ChargerPrevClients(chConnexion);
            ActiverUneFiche = false;
            cConfirmer = new BaseCommande(Confirmer);
            cAnnuler = new BaseCommande(Annuler);
            cAjouter = new BaseCommande(Ajouter);
            cModifier = new BaseCommande(Modifier);
            cSupprimer = new BaseCommande(Supprimer);
            cReglecot = new BaseCommande(Reglercot);
            cAjLocation = new BaseCommande(AjouterLocaClient);
        }

        private ObservableCollection<AffichClient> ChargerPrevClients(string chConn)
        {
            ObservableCollection<AffichClient> rep = new ObservableCollection<AffichClient>();
            List<C_TB_client> lTmp = new Model.G_TB_client(chConn).Lire("ID_client");
            AffichClient Atmp = new AffichClient();
            foreach (C_TB_client Tmp in lTmp)
            {
                Atmp = new AffichClient();
                Atmp.clientAff = Tmp;
                if (DateTime.Today.Month - Tmp.client_cotisation.Month >= 1)
                {
                    Atmp.retardCota = true;
                }
                else
                {
                    Atmp.retardCota = false;
                }
                rep.Add(Atmp);
            }
            return rep;
        }

        private ObservableCollection<C_TB_client> ChargerClients(string chConn)
        {
            ObservableCollection<C_TB_client> rep = new ObservableCollection<C_TB_client>();
            List<C_TB_client> lTmp = new Model.G_TB_client(chConn).Lire("ID_client");
            foreach (C_TB_client Tmp in lTmp)
                rep.Add(Tmp);
            return rep;
        }

        public void Confirmer()
        {
            if (nAjout == -1)
            {
                if (TestStringIsNullOrEmpty(UnClient.Pre) || TestStringIsNullOrEmpty(UnClient.Nom))
                {
                    MessageBox.Show("Erreur Nom ou prénom invalide");
                    ActiverUneFiche = false;
                    return;
                }
                else if (!IsValidEmailAddress(UnClient.Mail))
                {
                    MessageBox.Show("Email invalide ou manquant");
                    ActiverUneFiche = false;
                    return;
                }
                UnClient.ID = new G_TB_client(chConnexion).Ajouter(UnClient.Nom, UnClient.Pre, UnClient.Nai, UnClient.Crea, UnClient.Coti, UnClient.Mail);
                BcpClients.Add(new C_TB_client(UnClient.ID, UnClient.Nom, UnClient.Pre, UnClient.Nai, UnClient.Crea, UnClient.Coti, UnClient.Mail));
            }
            else
            {
                new G_TB_client(chConnexion).Modifier(UnClient.ID, UnClient.Nom, UnClient.Pre, UnClient.Nai, UnClient.Crea, UnClient.Coti, UnClient.Mail);
                BcpClients[nAjout] = new C_TB_client(UnClient.ID, UnClient.Nom, UnClient.Pre, UnClient.Nai, UnClient.Crea, UnClient.Coti, UnClient.Mail);
            }
            ActiverUneFiche = false;
        }

        public void Annuler()
        { ActiverUneFiche = false; }

        public void Ajouter()
        {
            UnClient = new VM_UnClient();
            nAjout = -1;
            UnClient.Nai = DateTime.Today;
            UnClient.Coti = DateTime.Today;
            UnClient.Crea = DateTime.Today;
            ActiverUneFiche = true;
        }

        public void Modifier()
        {
            if (ClientSelectionne != null)
            {
                C_TB_client Tmp = new Model.G_TB_client(chConnexion).Lire_ID(ClientSelectionne.ID_client);
                UnClient = new VM_UnClient();
                UnClient.ID = Tmp.ID_client;
                UnClient.Pre = Tmp.client_prenom;
                UnClient.Nom = Tmp.client_nom;
                UnClient.Nai = Tmp.client_nai;
                UnClient.Coti = Tmp.client_cotisation;
                UnClient.Crea = Tmp.client_crea;
                UnClient.Mail = Tmp.client_mail;
                nAjout = BcpClients.IndexOf(ClientSelectionne);
                ActiverUneFiche = true;
            }
        }

        public void Supprimer()
        {
            if (ClientSelectionne != null)
            {
                new Model.G_TB_client(chConnexion).Supprimer(ClientSelectionne.ID_client);
                BcpClients.Remove(ClientSelectionne);
            }
        }

        public void ClientSelectionnee2UneClient()
        {
            UnClient.ID = ClientSelectionne.ID_client;
            UnClient.Nom = ClientSelectionne.client_nom;
            UnClient.Pre = ClientSelectionne.client_prenom;
            UnClient.Nai = ClientSelectionne.client_nai;
            UnClient.Coti = ClientSelectionne.client_cotisation;
            UnClient.Crea = ClientSelectionne.client_crea;
            UnClient.Mail = ClientSelectionne.client_mail;
        }

        public bool TestStringIsNullOrEmpty(string s)
        {
            bool result;
            result = s == null || s == string.Empty;
            return result;
        }

        public bool IsValidEmailAddress(string s)
        {
            if(TestStringIsNullOrEmpty(s))
            {
                return false;
            }
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(s);
        }

        public void Init_View()
        {
            List<C_TB_client> cTmp = new G_TB_client(chConnexion).Lire("ID_client");
            foreach (C_TB_client c in cTmp)
            {
                if (DateTime.Today.Month - c.client_cotisation.Month >= 1)
                {
                    Lst_retard_cot.Add((C_TB_client)c);
                }
            }
            foreach (C_TB_client r in Lst_retard_cot)
            {
                int Mois_retard = (DateTime.Today.Month - r.client_cotisation.Month) + 12 * (DateTime.Today.Year - r.client_cotisation.Year);
                MessageBox.Show("Le client ID: " + r.ID_client + " est en retard de cotisation de plus de " + Mois_retard + " mois.", "Retard", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            List<C_TB_details> lst_verif_rendu = new G_TB_details(chConnexion).Lire("ID_details");
            foreach (C_TB_details rr in lst_verif_rendu)
            {
                if (rr.dat_limite != null)
                {
                    int rComp = DateTime.Compare(rr.dat_limite.Value, DateTime.Today);
                    if (rComp < 0)
                    {
                        int montant_amende = 10 + (DateTime.Today.Month - rr.dat_limite.Value.Month) + 12 * (DateTime.Today.Year - rr.dat_limite.Value.Year);
                        C_TB_location LocaTMP = new G_TB_location(chConnexion).Lire_ID(rr.ID_location);
                        C_TB_client clientTMP = new G_TB_client(chConnexion).Lire_ID(LocaTMP.ID_client);
                        MessageBox.Show("Une amende est dressé pour: " + clientTMP.client_nom + " " + clientTMP.client_prenom + " d'un montant de : " + montant_amende + " euros (10+[1*nombre de mois de retard]).");
                        new G_TB_details(chConnexion).Modifier(rr.ID_details, rr.ID_location, rr.ID_livre, rr.dat_emprunt, rr.dat_limite, rr.dat_rentre, montant_amende);
                    }
                }
            }
        }

        public bool verif_age_CBX(int cID)
        {
            C_TB_client tmp = new G_TB_client(chConnexion).Lire_ID(cID);
            if (DateTime.Today.Year - tmp.client_nai.Year > 18)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void AjouterLocaClient()
        {
            C_TB_client ajout_loca = new G_TB_client(chConnexion).Lire_ID(AClientSelectionne.clientAff.ID_client); //récupère le client avec son ID
            bool retard_client = false; //vérif cotisation
            foreach (C_TB_client ret in Lst_retard_cot)
            {
                if (ajout_loca.ID_client == ret.ID_client)
                {
                    retard_client = true;
                }
            }
            if (retard_client == true)
            {
                MessageBox.Show("Impossible d'ajouter une location car ce client est en retard de cotisation.");
            }
            else
            {
                if (MessageBox.Show("Ajouter une location au client: " + ajout_loca.ID_client + "?", "Ajout location ?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    int ajoutID = new G_TB_location(chConnexion).Ajouter(AClientSelectionne.clientAff.ID_client, DateTime.Today);
                    bool encore = true;
                    if (MessageBox.Show("Ajouter un emprunt ?", "Ajout emprunt ?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        while (encore == true)
                        {
                            //ajout d'un détail
                            EcranAjoutDetail EAD = new EcranAjoutDetail(chConnexion, ajoutID);
                            EAD.ShowDialog();
                            if (MessageBox.Show("Ajouter encore un emprunt ?", "Ajout détail ?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                            {
                                encore = false;
                            }
                        }
                        encore = true;
                    }
                }
            }
        }

        public void Reglercot(object i)
        {
            AffichClient tmp = (AffichClient)i;
            C_TB_client client_selec = new G_TB_client(chConnexion).Lire_ID(tmp.clientAff.ID_client);
            if (MessageBox.Show("Régler la cotisation du client: " + client_selec.ID_client + " ? \n Date de dernière cotisation:" + client_selec.client_cotisation.ToString(), "Cotisation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                new G_TB_client(chConnexion).Modifier(tmp.clientAff.ID_client, client_selec.client_prenom, client_selec.client_nom, client_selec.client_nai, client_selec.client_crea, DateTime.Today, client_selec.client_mail);
                MessageBox.Show("Cotisation du client: " + client_selec.ID_client + " avancé à " + DateTime.Today, "Cotisation", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public FlowDocument GenererFlow()
        {
            FlowDocument fd = new FlowDocument();
            Paragraph p = new Paragraph();
            p.Inlines.Add(new Bold(new Run("Table de gestion des clients")));
            p.Inlines.Add(new LineBreak());
            p.Inlines.Add(new Run("Liste des Clients encodées"));
            fd.Blocks.Add(p);
            List l = new List();
            foreach (C_TB_client cp in BcpClients)
            {
                Paragraph pl = new Paragraph(new Run(cp.client_prenom + " " + cp.client_nom
                 + " (" + cp.client_nai.ToShortDateString() + ") " + cp.client_cotisation.ToShortDateString() + " / " + cp.client_crea.ToShortDateString() + " Mail: " + cp.client_mail.ToString()));
                l.ListItems.Add(new ListItem(pl));
            }
            fd.Blocks.Add(l);
            return fd;
        }
    }

    public class VM_UnClient : BasePropriete
    {
        private int _ID;
        private string _Nom, _Pre;
        private DateTime _Nai, _Coti, _Crea;
        private string _Mail;

        public int ID
        {
            get { return _ID; }
            set { AssignerChamp<int>(ref _ID, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public string Pre
        {
            get { return _Pre; }
            set { AssignerChamp<string>(ref _Pre, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public string Nom
        {
            get { return _Nom; }
            set { AssignerChamp<string>(ref _Nom, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public DateTime Nai
        {
            get { return _Nai; }
            set { AssignerChamp<DateTime>(ref _Nai, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public DateTime Coti
        {
            get { return _Coti; }
            set { AssignerChamp<DateTime>(ref _Coti, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public DateTime Crea
        {
            get { return _Crea; }
            set { AssignerChamp<DateTime>(ref _Crea, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        public string Mail
        {
            get { return _Mail; }
            set { AssignerChamp<string>(ref _Mail, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }
    }

}