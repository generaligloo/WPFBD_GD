#region Ressources extérieures

using System;
using System.Collections.Generic;

#endregion Ressources extérieures

namespace WPFBD_GD_1ER.Model
{
    /// <summary>
    /// Couche intermédiaire de gestion (Business Layer)
    /// </summary>
    public class G_TB_details : G_Base
    {
        #region Constructeurs

        public G_TB_details()
         : base()
        { }

        public G_TB_details(string sChaineConnexion)
         : base(sChaineConnexion)
        { }

        #endregion Constructeurs

        public int Ajouter(int ID_location, int? ID_livre, DateTime dat_emprunt, DateTime? dat_limite, DateTime? dat_rentre, int? amende)
        { return new A_TB_details(ChaineConnexion).Ajouter(ID_location, ID_livre, dat_emprunt, dat_limite, dat_rentre, amende); }

        public int Modifier(int ID_details, int ID_location, int? ID_livre, DateTime dat_emprunt, DateTime? dat_limite, DateTime? dat_rentre, int? amende)
        { return new A_TB_details(ChaineConnexion).Modifier(ID_details, ID_location, ID_livre, dat_emprunt, dat_limite, dat_rentre, amende); }

        public List<C_TB_details> Lire(string Index)
        { return new A_TB_details(ChaineConnexion).Lire(Index); }

        public C_TB_details Lire_ID(int ID_details)
        { return new A_TB_details(ChaineConnexion).Lire_ID(ID_details); }

        public int Supprimer(int ID_details)
        { return new A_TB_details(ChaineConnexion).Supprimer(ID_details); }
    }
}