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
 public class A_TB_categorie : ADBase
 {
  #region Constructeurs
  public A_TB_categorie(string sChaineConnexion)
  	: base(sChaineConnexion)
  { }
  #endregion
  public int Ajouter(string Nom, Int16 Pegi)
  {
   CreerCommande("AjouterTB_categorie");
   int res = 0;
   Commande.Parameters.Add("ID_categorie", SqlDbType.Int);
   Direction("ID_categorie", ParameterDirection.Output);
   Commande.Parameters.AddWithValue("@Nom", Nom);
   Commande.Parameters.AddWithValue("@Pegi", Pegi);
   Commande.Connection.Open();
   Commande.ExecuteNonQuery();
   res = int.Parse(LireParametre("ID_categorie"));
   Commande.Connection.Close();
   return res;
  }
  public int Modifier(int ID_categorie, string Nom, Int16 Pegi)
  {
   CreerCommande("ModifierTB_categorie");
   int res = 0;
   Commande.Parameters.AddWithValue("@ID_categorie", ID_categorie);
   Commande.Parameters.AddWithValue("@Nom", Nom);
   Commande.Parameters.AddWithValue("@Pegi", Pegi);
   Commande.Connection.Open();
   Commande.ExecuteNonQuery();
   Commande.Connection.Close();
   return res;
  }
  public List<C_TB_categorie> Lire(string Index)
  {
   CreerCommande("SelectionnerTB_categorie");
   Commande.Parameters.AddWithValue("@Index", Index);
   Commande.Connection.Open();
   SqlDataReader dr = Commande.ExecuteReader();
   List<C_TB_categorie> res = new List<C_TB_categorie>();
   while (dr.Read())
   {
    C_TB_categorie tmp = new C_TB_categorie();
    tmp.ID_categorie = int.Parse(dr["ID_categorie"].ToString());
    tmp.Nom = dr["Nom"].ToString();
    tmp.Pegi = Int16.Parse(dr["Pegi"].ToString());
    res.Add(tmp);
			}
			dr.Close();
			Commande.Connection.Close();
			return res;
		}
  public C_TB_categorie Lire_ID(int ID_categorie)
  {
   CreerCommande("SelectionnerTB_categorie_ID");
   Commande.Parameters.AddWithValue("@ID_categorie", ID_categorie);
   Commande.Connection.Open();
   SqlDataReader dr = Commande.ExecuteReader();
   C_TB_categorie res = new C_TB_categorie();
   while (dr.Read())
   {
    res.ID_categorie = int.Parse(dr["ID_categorie"].ToString());
    res.Nom = dr["Nom"].ToString();
    res.Pegi = Int16.Parse(dr["Pegi"].ToString());
   }
			dr.Close();
			Commande.Connection.Close();
			return res;
		}
  public int Supprimer(int ID_categorie)
  {
   CreerCommande("SupprimerTB_categorie");
   int res = 0;
   Commande.Parameters.AddWithValue("@ID_categorie", ID_categorie);
   Commande.Connection.Open();
   res = Commande.ExecuteNonQuery();
			Commande.Connection.Close();
			return res;
		}
 }
}
