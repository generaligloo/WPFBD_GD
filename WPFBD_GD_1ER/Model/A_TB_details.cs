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
 public class A_TB_details : ADBase
 {
  #region Constructeurs
  public A_TB_details(string sChaineConnexion)
  	: base(sChaineConnexion)
  { }
  #endregion
  public int Ajouter(int ID_location, int? ID_livre, DateTime dat_emprunt, DateTime? dat_limite, DateTime? dat_rentre, int? amende)
  {
   CreerCommande("AjouterTB_details");
   int res = 0;
   Commande.Parameters.Add("ID_details", SqlDbType.Int);
   Direction("ID_details", ParameterDirection.Output);
   Commande.Parameters.AddWithValue("@ID_location", ID_location);
   if(ID_livre == null) Commande.Parameters.AddWithValue("@ID_livre", Convert.DBNull);
   else Commande.Parameters.AddWithValue("@ID_livre", ID_livre);
   Commande.Parameters.AddWithValue("@dat_emprunt", dat_emprunt);
   if(dat_limite == null) Commande.Parameters.AddWithValue("@dat_limite", Convert.DBNull);
   else Commande.Parameters.AddWithValue("@dat_limite", dat_limite);
   if(dat_rentre == null) Commande.Parameters.AddWithValue("@dat_rentre", Convert.DBNull);
   else Commande.Parameters.AddWithValue("@dat_rentre", dat_rentre);
   if(amende == null) Commande.Parameters.AddWithValue("@amende", Convert.DBNull);
   else Commande.Parameters.AddWithValue("@amende", amende);
   Commande.Connection.Open();
   Commande.ExecuteNonQuery();
   res = int.Parse(LireParametre("ID_details"));
   Commande.Connection.Close();
   return res;
  }
  public int Modifier(int ID_details, int ID_location, int? ID_livre, DateTime dat_emprunt, DateTime? dat_limite, DateTime? dat_rentre, int? amende)
  {
   CreerCommande("ModifierTB_details");
   int res = 0;
   Commande.Parameters.AddWithValue("@ID_details", ID_details);
   Commande.Parameters.AddWithValue("@ID_location", ID_location);
   if(ID_livre == null) Commande.Parameters.AddWithValue("@ID_livre", Convert.DBNull);
   else Commande.Parameters.AddWithValue("@ID_livre", ID_livre);
   Commande.Parameters.AddWithValue("@dat_emprunt", dat_emprunt);
   if(dat_limite == null) Commande.Parameters.AddWithValue("@dat_limite", Convert.DBNull);
   else Commande.Parameters.AddWithValue("@dat_limite", dat_limite);
   if(dat_rentre == null) Commande.Parameters.AddWithValue("@dat_rentre", Convert.DBNull);
   else Commande.Parameters.AddWithValue("@dat_rentre", dat_rentre);
   if(amende == null) Commande.Parameters.AddWithValue("@amende", Convert.DBNull);
   else Commande.Parameters.AddWithValue("@amende", amende);
   Commande.Connection.Open();
   Commande.ExecuteNonQuery();
   Commande.Connection.Close();
   return res;
  }
  public List<C_TB_details> Lire(string Index)
  {
   CreerCommande("SelectionnerTB_details");
   Commande.Parameters.AddWithValue("@Index", Index);
   Commande.Connection.Open();
   SqlDataReader dr = Commande.ExecuteReader();
   List<C_TB_details> res = new List<C_TB_details>();
   while (dr.Read())
   {
    C_TB_details tmp = new C_TB_details();
    tmp.ID_details = int.Parse(dr["ID_details"].ToString());
    tmp.ID_location = int.Parse(dr["ID_location"].ToString());
   if(dr["ID_livre"] != DBNull.Value) tmp.ID_livre = int.Parse(dr["ID_livre"].ToString());
    tmp.dat_emprunt = DateTime.Parse(dr["dat_emprunt"].ToString());
   if(dr["dat_limite"] != DBNull.Value) tmp.dat_limite = DateTime.Parse(dr["dat_limite"].ToString());
   if(dr["dat_rentre"] != DBNull.Value) tmp.dat_rentre = DateTime.Parse(dr["dat_rentre"].ToString());
   if(dr["amende"] != DBNull.Value) tmp.amende = int.Parse(dr["amende"].ToString());
    res.Add(tmp);
			}
			dr.Close();
			Commande.Connection.Close();
			return res;
		}
  public C_TB_details Lire_ID(int ID_details)
  {
   CreerCommande("SelectionnerTB_details_ID");
   Commande.Parameters.AddWithValue("@ID_details", ID_details);
   Commande.Connection.Open();
   SqlDataReader dr = Commande.ExecuteReader();
   C_TB_details res = new C_TB_details();
   while (dr.Read())
   {
    res.ID_details = int.Parse(dr["ID_details"].ToString());
    res.ID_location = int.Parse(dr["ID_location"].ToString());
   if(dr["ID_livre"] != DBNull.Value) res.ID_livre = int.Parse(dr["ID_livre"].ToString());
    res.dat_emprunt = DateTime.Parse(dr["dat_emprunt"].ToString());
   if(dr["dat_limite"] != DBNull.Value) res.dat_limite = DateTime.Parse(dr["dat_limite"].ToString());
   if(dr["dat_rentre"] != DBNull.Value) res.dat_rentre = DateTime.Parse(dr["dat_rentre"].ToString());
   if(dr["amende"] != DBNull.Value) res.amende = int.Parse(dr["amende"].ToString());
   }
			dr.Close();
			Commande.Connection.Close();
			return res;
		}
  public int Supprimer(int ID_details)
  {
   CreerCommande("SupprimerTB_details");
   int res = 0;
   Commande.Parameters.AddWithValue("@ID_details", ID_details);
   Commande.Connection.Open();
   res = Commande.ExecuteNonQuery();
			Commande.Connection.Close();
			return res;
		}
 }
}
