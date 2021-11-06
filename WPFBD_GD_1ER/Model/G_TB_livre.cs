#region Ressources extérieures

using System;
using System.Collections.Generic;

#endregion Ressources extérieures

namespace WPFBD_GD_1ER.Model
{
    /// <summary>
    /// Couche intermédiaire de gestion (Business Layer)
    /// </summary>
    public class G_TB_livre : G_Base
    {
        #region Constructeurs

        public G_TB_livre()
         : base()
        { }

        public G_TB_livre(string sChaineConnexion)
         : base(sChaineConnexion)
        { }

        #endregion Constructeurs

        public int Ajouter(string titre, string auteur, int? ID_categorie, Int16 statut, int? Ann_pub, int ID_edition)
        { return new A_TB_livre(ChaineConnexion).Ajouter(titre, auteur, ID_categorie, statut, Ann_pub, ID_edition); }

        public int Modifier(int ID_livre, string titre, string auteur, int? ID_categorie, Int16 statut, int? Ann_pub, int ID_edition)
        { return new A_TB_livre(ChaineConnexion).Modifier(ID_livre, titre, auteur, ID_categorie, statut, Ann_pub, ID_edition); }

        public List<C_TB_livre> Lire(string Index)
        { return new A_TB_livre(ChaineConnexion).Lire(Index); }

        public C_TB_livre Lire_ID(int ID_livre)
        { return new A_TB_livre(ChaineConnexion).Lire_ID(ID_livre); }

        public int Supprimer(int ID_livre)
        { return new A_TB_livre(ChaineConnexion).Supprimer(ID_livre); }
    }
}