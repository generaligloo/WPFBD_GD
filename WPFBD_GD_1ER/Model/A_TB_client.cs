#region Ressources extérieures

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#endregion Ressources extérieures

namespace WPFBD_GD_1ER.Model
{
    /// <summary>
    /// Couche d'accès aux données (Data Access Layer)
    /// </summary>
    public class A_TB_client : ADBase
    {
        #region Constructeurs

        public A_TB_client(string sChaineConnexion)
            : base(sChaineConnexion)
        { }

        #endregion Constructeurs

        public int Ajouter(string client_prenom, string client_nom, DateTime client_nai, DateTime client_crea, DateTime client_cotisation, string client_mail)
        {
            CreerCommande("AjouterTB_client");
            int res = 0;
            Commande.Parameters.Add("ID_client", SqlDbType.Int);
            Direction("ID_client", ParameterDirection.Output);
            Commande.Parameters.AddWithValue("@client_prenom", client_prenom);
            Commande.Parameters.AddWithValue("@client_nom", client_nom);
            Commande.Parameters.AddWithValue("@client_nai", client_nai);
            Commande.Parameters.AddWithValue("@client_crea", client_crea);
            Commande.Parameters.AddWithValue("@client_cotisation", client_cotisation);
            Commande.Parameters.AddWithValue("@client_mail", client_mail);
            Commande.Connection.Open();
            Commande.ExecuteNonQuery();
            res = int.Parse(LireParametre("ID_client"));
            Commande.Connection.Close();
            return res;
        }

        public int Modifier(int ID_client, string client_prenom, string client_nom, DateTime client_nai, DateTime client_crea, DateTime client_cotisation, string client_mail)
        {
            CreerCommande("ModifierTB_client");
            int res = 0;
            Commande.Parameters.AddWithValue("@ID_client", ID_client);
            Commande.Parameters.AddWithValue("@client_prenom", client_prenom);
            Commande.Parameters.AddWithValue("@client_nom", client_nom);
            Commande.Parameters.AddWithValue("@client_nai", client_nai);
            Commande.Parameters.AddWithValue("@client_crea", client_crea);
            Commande.Parameters.AddWithValue("@client_cotisation", client_cotisation);
            Commande.Parameters.AddWithValue("@client_mail", client_mail);
            Commande.Connection.Open();
            Commande.ExecuteNonQuery();
            Commande.Connection.Close();
            return res;
        }

        public List<C_TB_client> Lire(string Index)
        {
            CreerCommande("SelectionnerTB_client");
            Commande.Parameters.AddWithValue("@Index", Index);
            Commande.Connection.Open();
            SqlDataReader dr = Commande.ExecuteReader();
            List<C_TB_client> res = new List<C_TB_client>();
            while (dr.Read())
            {
                C_TB_client tmp = new C_TB_client();
                tmp.ID_client = int.Parse(dr["ID_client"].ToString());
                tmp.client_prenom = dr["client_prenom"].ToString();
                tmp.client_nom = dr["client_nom"].ToString();
                tmp.client_nai = DateTime.Parse(dr["client_nai"].ToString());
                tmp.client_crea = DateTime.Parse(dr["client_crea"].ToString());
                tmp.client_cotisation = DateTime.Parse(dr["client_cotisation"].ToString());
                tmp.client_mail = dr["client_mail"].ToString();
                res.Add(tmp);
            }
            dr.Close();
            Commande.Connection.Close();
            return res;
        }

        public C_TB_client Lire_ID(int ID_client)
        {
            CreerCommande("SelectionnerTB_client_ID");
            Commande.Parameters.AddWithValue("@ID_client", ID_client);
            Commande.Connection.Open();
            SqlDataReader dr = Commande.ExecuteReader();
            C_TB_client res = new C_TB_client();
            while (dr.Read())
            {
                res.ID_client = int.Parse(dr["ID_client"].ToString());
                res.client_prenom = dr["client_prenom"].ToString();
                res.client_nom = dr["client_nom"].ToString();
                res.client_nai = DateTime.Parse(dr["client_nai"].ToString());
                res.client_crea = DateTime.Parse(dr["client_crea"].ToString());
                res.client_cotisation = DateTime.Parse(dr["client_cotisation"].ToString());
                res.client_mail = dr["client_mail"].ToString();
            }
            dr.Close();
            Commande.Connection.Close();
            return res;
        }

        public int Supprimer(int ID_client)
        {
            CreerCommande("SupprimerTB_client");
            int res = 0;
            Commande.Parameters.AddWithValue("@ID_client", ID_client);
            Commande.Connection.Open();
            res = Commande.ExecuteNonQuery();
            Commande.Connection.Close();
            return res;
        }
    }
}