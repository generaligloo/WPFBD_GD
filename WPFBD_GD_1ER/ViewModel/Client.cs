using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;
using WPFBD_GD_1ER.Model;

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

        #endregion Données extérieures

        #region Commandes

        public BaseCommande cConfirmer { get; set; }
        public BaseCommande cAnnuler { get; set; }
        public BaseCommande cAjouter { get; set; }
        public BaseCommande cModifier { get; set; }
        public BaseCommande cSupprimer { get; set; }

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
            ActiverUneFiche = false;
            cConfirmer = new BaseCommande(Confirmer);
            cAnnuler = new BaseCommande(Annuler);
            cAjouter = new BaseCommande(Ajouter);
            cModifier = new BaseCommande(Modifier);
            cSupprimer = new BaseCommande(Supprimer);
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