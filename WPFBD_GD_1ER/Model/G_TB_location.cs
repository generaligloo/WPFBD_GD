#region Ressources extérieures

using System;
using System.Collections.Generic;

#endregion Ressources extérieures

namespace WPFBD_GD_1ER.Model
{
    /// <summary>
    /// Couche intermédiaire de gestion (Business Layer)
    /// </summary>
    public class G_TB_location : G_Base
    {
        #region Constructeurs

        public G_TB_location()
         : base()
        { }

        public G_TB_location(string sChaineConnexion)
         : base(sChaineConnexion)
        { }

        #endregion Constructeurs

        public int Ajouter(int ID_client, DateTime? dat_location)
        { return new A_TB_location(ChaineConnexion).Ajouter(ID_client, dat_location); }

        public int Modifier(int ID_location, int ID_client, DateTime? dat_location)
        { return new A_TB_location(ChaineConnexion).Modifier(ID_location, ID_client, dat_location); }

        public List<C_TB_location> Lire(string Index)
        { return new A_TB_location(ChaineConnexion).Lire(Index); }

        public C_TB_location Lire_ID(int ID_location)
        { return new A_TB_location(ChaineConnexion).Lire_ID(ID_location); }

        public int Supprimer(int ID_location)
        { return new A_TB_location(ChaineConnexion).Supprimer(ID_location); }
    }
}