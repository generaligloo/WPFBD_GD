#region Ressources extérieures

using System;

#endregion Ressources extérieures

namespace WPFBD_GD_1ER.Model
{
    /// <summary>
    /// Classe de définition des données
    /// </summary>
    public class C_TB_client
    {
        #region Données membres

        private int _ID_client;
        private string _client_prenom;
        private string _client_nom;
        private DateTime _client_nai;
        private DateTime _client_crea;
        private DateTime _client_cotisation;
        private string _client_mail;

        #endregion Données membres

        #region Constructeurs

        public C_TB_client()
        { }

        public C_TB_client(string client_prenom_, string client_nom_, DateTime client_nai_, DateTime client_crea_, DateTime client_cotisation_, string client_mail_)
        {
            client_prenom = client_prenom_;
            client_nom = client_nom_;
            client_nai = client_nai_;
            client_crea = client_crea_;
            client_cotisation = client_cotisation_;
            client_mail = client_mail_;
        }

        public C_TB_client(int ID_client_, string client_prenom_, string client_nom_, DateTime client_nai_, DateTime client_crea_, DateTime client_cotisation_, string client_mail_)
         : this(client_prenom_, client_nom_, client_nai_, client_crea_, client_cotisation_, client_mail_)
        {
            ID_client = ID_client_;
        }

        #endregion Constructeurs

        #region Accesseurs

        public int ID_client
        {
            get { return _ID_client; }
            set { _ID_client = value; }
        }

        public string client_prenom
        {
            get { return _client_prenom; }
            set { _client_prenom = value; }
        }

        public string client_nom
        {
            get { return _client_nom; }
            set { _client_nom = value; }
        }

        public DateTime client_nai
        {
            get { return _client_nai; }
            set { _client_nai = value; }
        }

        public DateTime client_crea
        {
            get { return _client_crea; }
            set { _client_crea = value; }
        }

        public DateTime client_cotisation
        {
            get { return _client_cotisation; }
            set { _client_cotisation = value; }
        }

        public string client_mail
        {
            get { return _client_mail; }
            set { _client_mail = value; }
        }

        #endregion Accesseurs
    }
}