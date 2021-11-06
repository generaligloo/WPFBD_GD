#region Ressources extérieures

using System;

#endregion Ressources extérieures

namespace WPFBD_GD_1ER.Model
{
    /// <summary>
    /// Classe de définition des données
    /// </summary>
    public class C_TB_details
    {
        #region Données membres

        private int _ID_details;
        private int _ID_location;
        private int? _ID_livre;
        private DateTime _dat_emprunt;
        private DateTime? _dat_limite;
        private DateTime? _dat_rentre;
        private int? _amende;

        #endregion Données membres

        #region Constructeurs

        public C_TB_details()
        { }

        public C_TB_details(int ID_location_, int? ID_livre_, DateTime dat_emprunt_, DateTime? dat_limite_, DateTime? dat_rentre_, int? amende_)
        {
            ID_location = ID_location_;
            ID_livre = ID_livre_;
            dat_emprunt = dat_emprunt_;
            dat_limite = dat_limite_;
            dat_rentre = dat_rentre_;
            amende = amende_;
        }

        public C_TB_details(int ID_details_, int ID_location_, int? ID_livre_, DateTime dat_emprunt_, DateTime? dat_limite_, DateTime? dat_rentre_, int? amende_)
         : this(ID_location_, ID_livre_, dat_emprunt_, dat_limite_, dat_rentre_, amende_)
        {
            ID_details = ID_details_;
        }

        #endregion Constructeurs

        #region Accesseurs

        public int ID_details
        {
            get { return _ID_details; }
            set { _ID_details = value; }
        }

        public int ID_location
        {
            get { return _ID_location; }
            set { _ID_location = value; }
        }

        public int? ID_livre
        {
            get { return _ID_livre; }
            set { _ID_livre = value; }
        }

        public DateTime dat_emprunt
        {
            get { return _dat_emprunt; }
            set { _dat_emprunt = value; }
        }

        public DateTime? dat_limite
        {
            get { return _dat_limite; }
            set { _dat_limite = value; }
        }

        public DateTime? dat_rentre
        {
            get { return _dat_rentre; }
            set { _dat_rentre = value; }
        }

        public int? amende
        {
            get { return _amende; }
            set { _amende = value; }
        }

        #endregion Accesseurs
    }
}