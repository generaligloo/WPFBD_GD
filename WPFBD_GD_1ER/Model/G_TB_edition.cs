#region Ressources extérieures

using System;
using System.Collections.Generic;

#endregion Ressources extérieures

namespace WPFBD_GD_1ER.Model
{
    /// <summary>
    /// Couche intermédiaire de gestion (Business Layer)
    /// </summary>
    public class G_TB_edition : G_Base
    {
        #region Constructeurs

        public G_TB_edition()
         : base()
        { }

        public G_TB_edition(string sChaineConnexion)
         : base(sChaineConnexion)
        { }

        #endregion Constructeurs

        public int Ajouter(string edi_nom, DateTime edi_dat, string edi_pdg_nom, string edi_pdg_prenom)
        { return new A_TB_edition(ChaineConnexion).Ajouter(edi_nom, edi_dat, edi_pdg_nom, edi_pdg_prenom); }

        public int Modifier(int ID_edition, string edi_nom, DateTime edi_dat, string edi_pdg_nom, string edi_pdg_prenom)
        { return new A_TB_edition(ChaineConnexion).Modifier(ID_edition, edi_nom, edi_dat, edi_pdg_nom, edi_pdg_prenom); }

        public List<C_TB_edition> Lire(string Index)
        { return new A_TB_edition(ChaineConnexion).Lire(Index); }

        public C_TB_edition Lire_ID(int ID_edition)
        { return new A_TB_edition(ChaineConnexion).Lire_ID(ID_edition); }

        public int Supprimer(int ID_edition)
        { return new A_TB_edition(ChaineConnexion).Supprimer(ID_edition); }
    }
}