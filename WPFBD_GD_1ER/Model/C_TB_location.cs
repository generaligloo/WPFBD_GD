#region Ressources extérieures

using System;

#endregion Ressources extérieures

namespace WPFBD_GD_1ER.Model
{
    /// <summary>
    /// Classe de définition des données
    /// </summary>
    public class C_TB_location
    {
        #region Données membres

        private int _ID_location;
        private int _ID_client;
        private DateTime? _dat_location;

        #endregion Données membres

        #region Constructeurs

        public C_TB_location()
        { }

        public C_TB_location(int ID_client_, DateTime? dat_location_)
        {
            ID_client = ID_client_;
            dat_location = dat_location_;
        }

        public C_TB_location(int ID_location_, int ID_client_, DateTime? dat_location_)
         : this(ID_client_, dat_location_)
        {
            ID_location = ID_location_;
        }

        #endregion Constructeurs

        #region Accesseurs

        public int ID_location
        {
            get { return _ID_location; }
            set { _ID_location = value; }
        }

        public int ID_client
        {
            get { return _ID_client; }
            set { _ID_client = value; }
        }

        public DateTime? dat_location
        {
            get { return _dat_location; }
            set { _dat_location = value; }
        }

        #endregion Accesseurs
    }
}