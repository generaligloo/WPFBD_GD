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
    public class A_TB_location : ADBase
    {
        #region Constructeurs

        public A_TB_location(string sChaineConnexion)
            : base(sChaineConnexion)
        { }

        #endregion Constructeurs

        public int Ajouter(int ID_client, DateTime? dat_location)
        {
            CreerCommande("AjouterTB_location");
            int res = 0;
            Commande.Parameters.Add("ID_location", SqlDbType.Int);
            Direction("ID_location", ParameterDirection.Output);
            Commande.Parameters.AddWithValue("@ID_client", ID_client);
            if (dat_location == null) Commande.Parameters.AddWithValue("@dat_location", Convert.DBNull);
            else Commande.Parameters.AddWithValue("@dat_location", dat_location);
            Commande.Connection.Open();
            Commande.ExecuteNonQuery();
            res = int.Parse(LireParametre("ID_location"));
            Commande.Connection.Close();
            return res;
        }

        public int Modifier(int ID_location, int ID_client, DateTime? dat_location)
        {
            CreerCommande("ModifierTB_location");
            int res = 0;
            Commande.Parameters.AddWithValue("@ID_location", ID_location);
            Commande.Parameters.AddWithValue("@ID_client", ID_client);
            if (dat_location == null) Commande.Parameters.AddWithValue("@dat_location", Convert.DBNull);
            else Commande.Parameters.AddWithValue("@dat_location", dat_location);
            Commande.Connection.Open();
            Commande.ExecuteNonQuery();
            Commande.Connection.Close();
            return res;
        }

        public List<C_TB_location> Lire(string Index)
        {
            CreerCommande("SelectionnerTB_location");
            Commande.Parameters.AddWithValue("@Index", Index);
            Commande.Connection.Open();
            SqlDataReader dr = Commande.ExecuteReader();
            List<C_TB_location> res = new List<C_TB_location>();
            while (dr.Read())
            {
                C_TB_location tmp = new C_TB_location();
                tmp.ID_location = int.Parse(dr["ID_location"].ToString());
                tmp.ID_client = int.Parse(dr["ID_client"].ToString());
                if (dr["dat_location"] != DBNull.Value) tmp.dat_location = DateTime.Parse(dr["dat_location"].ToString());
                res.Add(tmp);
            }
            dr.Close();
            Commande.Connection.Close();
            return res;
        }

        public C_TB_location Lire_ID(int ID_location)
        {
            CreerCommande("SelectionnerTB_location_ID");
            Commande.Parameters.AddWithValue("@ID_location", ID_location);
            Commande.Connection.Open();
            SqlDataReader dr = Commande.ExecuteReader();
            C_TB_location res = new C_TB_location();
            while (dr.Read())
            {
                res.ID_location = int.Parse(dr["ID_location"].ToString());
                res.ID_client = int.Parse(dr["ID_client"].ToString());
                if (dr["dat_location"] != DBNull.Value) res.dat_location = DateTime.Parse(dr["dat_location"].ToString());
            }
            dr.Close();
            Commande.Connection.Close();
            return res;
        }

        public int Supprimer(int ID_location)
        {
            CreerCommande("SupprimerTB_location");
            int res = 0;
            Commande.Parameters.AddWithValue("@ID_location", ID_location);
            Commande.Connection.Open();
            res = Commande.ExecuteNonQuery();
            Commande.Connection.Close();
            return res;
        }
    }
}