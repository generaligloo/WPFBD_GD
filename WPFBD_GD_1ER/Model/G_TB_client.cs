#region Ressources extérieures

using System;
using System.Collections.Generic;

#endregion Ressources extérieures

namespace WPFBD_GD_1ER.Model
{
    /// <summary>
    /// Couche intermédiaire de gestion (Business Layer)
    /// </summary>
    public class G_TB_client : G_Base
    {
        #region Constructeurs

        public G_TB_client()
         : base()
        { }

        public G_TB_client(string sChaineConnexion)
         : base(sChaineConnexion)
        { }

        #endregion Constructeurs

        public int Ajouter(string client_prenom, string client_nom, DateTime client_nai, DateTime client_crea, DateTime client_cotisation, string client_mail)
        { return new A_TB_client(ChaineConnexion).Ajouter(client_prenom, client_nom, client_nai, client_crea, client_cotisation, client_mail); }

        public int Modifier(int ID_client, string client_prenom, string client_nom, DateTime client_nai, DateTime client_crea, DateTime client_cotisation, string client_mail)
        { return new A_TB_client(ChaineConnexion).Modifier(ID_client, client_prenom, client_nom, client_nai, client_crea, client_cotisation, client_mail); }

        public List<C_TB_client> Lire(string Index)
        { return new A_TB_client(ChaineConnexion).Lire(Index); }

        public C_TB_client Lire_ID(int ID_client)
        { return new A_TB_client(ChaineConnexion).Lire_ID(ID_client); }

        public int Supprimer(int ID_client)
        { return new A_TB_client(ChaineConnexion).Supprimer(ID_client); }
    }
}