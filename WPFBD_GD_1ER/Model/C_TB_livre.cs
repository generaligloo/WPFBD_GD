#region Ressources extérieures

using System;

#endregion Ressources extérieures

namespace WPFBD_GD_1ER.Model
{
    /// <summary>
    /// Classe de définition des données
    /// </summary>
    public class C_TB_livre
    {
        #region Données membres

        private int _ID_livre;
        private string _titre;
        private string _auteur;
        private int? _ID_categorie;
        private Int16 _statut;
        private int? _Ann_pub;
        private int _ID_edition;

        #endregion Données membres

        #region Constructeurs

        public C_TB_livre()
        { }

        public C_TB_livre(string titre_, string auteur_, int? ID_categorie_, Int16 statut_, int? Ann_pub_, int ID_edition_)
        {
            titre = titre_;
            auteur = auteur_;
            ID_categorie = ID_categorie_;
            statut = statut_;
            Ann_pub = Ann_pub_;
            ID_edition = ID_edition_;
        }

        public C_TB_livre(int ID_livre_, string titre_, string auteur_, int? ID_categorie_, Int16 statut_, int? Ann_pub_, int ID_edition_)
         : this(titre_, auteur_, ID_categorie_, statut_, Ann_pub_, ID_edition_)
        {
            ID_livre = ID_livre_;
        }

        #endregion Constructeurs

        #region Accesseurs

        public int ID_livre
        {
            get { return _ID_livre; }
            set { _ID_livre = value; }
        }

        public string titre
        {
            get { return _titre; }
            set { _titre = value; }
        }

        public string auteur
        {
            get { return _auteur; }
            set { _auteur = value; }
        }

        public int? ID_categorie
        {
            get { return _ID_categorie; }
            set { _ID_categorie = value; }
        }

        public Int16 statut
        {
            get { return _statut; }
            set { _statut = value; }
        }

        public int? Ann_pub
        {
            get { return _Ann_pub; }
            set { _Ann_pub = value; }
        }

        public int ID_edition
        {
            get { return _ID_edition; }
            set { _ID_edition = value; }
        }

        #endregion Accesseurs
    }
}