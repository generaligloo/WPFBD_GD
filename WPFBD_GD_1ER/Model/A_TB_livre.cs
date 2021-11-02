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
 public class A_TB_livre : ADBase
 {
  #region Constructeurs
  public A_TB_livre(string sChaineConnexion)
  	: base(sChaineConnexion)
  { }
  #endregion
  public int Ajouter(string titre, string auteur, int? ID_categorie, Int16 statut, int? Ann_pub, int ID_edition)
  {
   CreerCommande("AjouterTB_livre");
   int res = 0;
   Commande.Parameters.Add("ID_livre", SqlDbType.Int);
   Direction("ID_livre", ParameterDirection.Output);
   Commande.Parameters.AddWithValue("@titre", titre);
   Commande.Parameters.AddWithValue("@auteur", auteur);
   if(ID_categorie == null) Commande.Parameters.AddWithValue("@ID_categorie", Convert.DBNull);
   else Commande.Parameters.AddWithValue("@ID_categorie", ID_categorie);
   Commande.Parameters.AddWithValue("@statut", statut);
   if(Ann_pub == null) Commande.Parameters.AddWithValue("@Ann_pub", Convert.DBNull);
   else Commande.Parameters.AddWithValue("@Ann_pub", Ann_pub);
   Commande.Parameters.AddWithValue("@ID_edition", ID_edition);
   Commande.Connection.Open();
   Commande.ExecuteNonQuery();
   res = int.Parse(LireParametre("ID_livre"));
   Commande.Connection.Close();
   return res;
  }
  public int Modifier(int ID_livre, string titre, string auteur, int? ID_categorie, Int16 statut, int? Ann_pub, int ID_edition)
  {
   CreerCommande("ModifierTB_livre");
   int res = 0;
   Commande.Parameters.AddWithValue("@ID_livre", ID_livre);
   Commande.Parameters.AddWithValue("@titre", titre);
   Commande.Parameters.AddWithValue("@auteur", auteur);
   if(ID_categorie == null) Commande.Parameters.AddWithValue("@ID_categorie", Convert.DBNull);
   else Commande.Parameters.AddWithValue("@ID_categorie", ID_categorie);
   Commande.Parameters.AddWithValue("@statut", statut);
   if(Ann_pub == null) Commande.Parameters.AddWithValue("@Ann_pub", Convert.DBNull);
   else Commande.Parameters.AddWithValue("@Ann_pub", Ann_pub);
   Commande.Parameters.AddWithValue("@ID_edition", ID_edition);
   Commande.Connection.Open();
   Commande.ExecuteNonQuery();
   Commande.Connection.Close();
   return res;
  }
  public List<C_TB_livre> Lire(string Index)
  {
   CreerCommande("SelectionnerTB_livre");
   Commande.Parameters.AddWithValue("@Index", Index);
   Commande.Connection.Open();
   SqlDataReader dr = Commande.ExecuteReader();
   List<C_TB_livre> res = new List<C_TB_livre>();
   while (dr.Read())
   {
    C_TB_livre tmp = new C_TB_livre();
    tmp.ID_livre = int.Parse(dr["ID_livre"].ToString());
    tmp.titre = dr["titre"].ToString();
    tmp.auteur = dr["auteur"].ToString();
   if(dr["ID_categorie"] != DBNull.Value) tmp.ID_categorie = int.Parse(dr["ID_categorie"].ToString());
    tmp.statut = Int16.Parse(dr["statut"].ToString());
   if(dr["Ann_pub"] != DBNull.Value) tmp.Ann_pub = int.Parse(dr["Ann_pub"].ToString());
    tmp.ID_edition = int.Parse(dr["ID_edition"].ToString());
    res.Add(tmp);
			}
			dr.Close();
			Commande.Connection.Close();
			return res;
		}
  public C_TB_livre Lire_ID(int ID_livre)
  {
   CreerCommande("SelectionnerTB_livre_ID");
   Commande.Parameters.AddWithValue("@ID_livre", ID_livre);
   Commande.Connection.Open();
   SqlDataReader dr = Commande.ExecuteReader();
   C_TB_livre res = new C_TB_livre();
   while (dr.Read())
   {
    res.ID_livre = int.Parse(dr["ID_livre"].ToString());
    res.titre = dr["titre"].ToString();
    res.auteur = dr["auteur"].ToString();
   if(dr["ID_categorie"] != DBNull.Value) res.ID_categorie = int.Parse(dr["ID_categorie"].ToString());
    res.statut = Int16.Parse(dr["statut"].ToString());
   if(dr["Ann_pub"] != DBNull.Value) res.Ann_pub = int.Parse(dr["Ann_pub"].ToString());
    res.ID_edition = int.Parse(dr["ID_edition"].ToString());
   }
			dr.Close();
			Commande.Connection.Close();
			return res;
		}
  public int Supprimer(int ID_livre)
  {
   CreerCommande("SupprimerTB_livre");
   int res = 0;
   Commande.Parameters.AddWithValue("@ID_livre", ID_livre);
   Commande.Connection.Open();
   res = Commande.ExecuteNonQuery();
			Commande.Connection.Close();
			return res;
		}
 }
}
