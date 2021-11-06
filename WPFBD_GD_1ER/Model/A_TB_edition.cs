#region Ressources extérieures
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using WPFBD_GD_1ER.Model;
#endregion

namespace WPFBD_GD_1ER.Model
{
 /// <summary>
 /// Couche d'accès aux données (Data Access Layer)
 /// </summary>
 public class A_TB_edition : ADBase
 {
  #region Constructeurs
  public A_TB_edition(string sChaineConnexion)
  	: base(sChaineConnexion)
  { }
  #endregion
  public int Ajouter(string edi_nom, DateTime edi_dat, string edi_pdg_nom, string edi_pdg_prenom)
  {
   CreerCommande("AjouterTB_edition");
   int res = 0;
   Commande.Parameters.Add("ID_edition", SqlDbType.Int);
   Direction("ID_edition", ParameterDirection.Output);
   Commande.Parameters.AddWithValue("@edi_nom", edi_nom);
   Commande.Parameters.AddWithValue("@edi_dat", edi_dat);
   if(edi_pdg_nom == null) Commande.Parameters.AddWithValue("@edi_pdg_nom", Convert.DBNull);
   else Commande.Parameters.AddWithValue("@edi_pdg_nom", edi_pdg_nom);
   if(edi_pdg_prenom == null) Commande.Parameters.AddWithValue("@edi_pdg_prenom", Convert.DBNull);
   else Commande.Parameters.AddWithValue("@edi_pdg_prenom", edi_pdg_prenom);
   Commande.Connection.Open();
   Commande.ExecuteNonQuery();
   res = int.Parse(LireParametre("ID_edition"));
   Commande.Connection.Close();
   return res;
  }
  public int Modifier(int ID_edition, string edi_nom, DateTime edi_dat, string edi_pdg_nom, string edi_pdg_prenom)
  {
   CreerCommande("ModifierTB_edition");
   int res = 0;
   Commande.Parameters.AddWithValue("@ID_edition", ID_edition);
   Commande.Parameters.AddWithValue("@edi_nom", edi_nom);
   Commande.Parameters.AddWithValue("@edi_dat", edi_dat);
   if(edi_pdg_nom == null) Commande.Parameters.AddWithValue("@edi_pdg_nom", Convert.DBNull);
   else Commande.Parameters.AddWithValue("@edi_pdg_nom", edi_pdg_nom);
   if(edi_pdg_prenom == null) Commande.Parameters.AddWithValue("@edi_pdg_prenom", Convert.DBNull);
   else Commande.Parameters.AddWithValue("@edi_pdg_prenom", edi_pdg_prenom);
   Commande.Connection.Open();
   Commande.ExecuteNonQuery();
   Commande.Connection.Close();
   return res;
  }
  public List<C_TB_edition> Lire(string Index)
  {
   CreerCommande("SelectionnerTB_edition");
   Commande.Parameters.AddWithValue("@Index", Index);
   Commande.Connection.Open();
   SqlDataReader dr = Commande.ExecuteReader();
   List<C_TB_edition> res = new List<C_TB_edition>();
   while (dr.Read())
   {
    C_TB_edition tmp = new C_TB_edition();
    tmp.ID_edition = int.Parse(dr["ID_edition"].ToString());
    tmp.edi_nom = dr["edi_nom"].ToString();
    tmp.edi_dat = DateTime.Parse(dr["edi_dat"].ToString());
    tmp.edi_pdg_nom = dr["edi_pdg_nom"].ToString();
    tmp.edi_pdg_prenom = dr["edi_pdg_prenom"].ToString();
    res.Add(tmp);
			}
			dr.Close();
			Commande.Connection.Close();
			return res;
		}
  public C_TB_edition Lire_ID(int ID_edition)
  {
   CreerCommande("SelectionnerTB_edition_ID");
   Commande.Parameters.AddWithValue("@ID_edition", ID_edition);
   Commande.Connection.Open();
   SqlDataReader dr = Commande.ExecuteReader();
   C_TB_edition res = new C_TB_edition();
   while (dr.Read())
   {
    res.ID_edition = int.Parse(dr["ID_edition"].ToString());
    res.edi_nom = dr["edi_nom"].ToString();
    res.edi_dat = DateTime.Parse(dr["edi_dat"].ToString());
    res.edi_pdg_nom = dr["edi_pdg_nom"].ToString();
    res.edi_pdg_prenom = dr["edi_pdg_prenom"].ToString();
   }
			dr.Close();
			Commande.Connection.Close();
			return res;
		}
  public int Supprimer(int ID_edition)
  {
   CreerCommande("SupprimerTB_edition");
   int res = 0;
   Commande.Parameters.AddWithValue("@ID_edition", ID_edition);
   Commande.Connection.Open();
   res = Commande.ExecuteNonQuery();
			Commande.Connection.Close();
			return res;
		}
 }
}
