using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFBD_GD_1ER.Model
{
    public class A_Client: A_Base
    {
        public A_Client()
        { }

        public A_Client(string chConn)
         : base(chConn)
        { }

        public int AjouterClient(string Nom_, string Pre_, DateTime Nai_, DateTime Crea_, DateTime Coti_)
        {
            _Commande.CommandText = "AjouterTB_client";
            int res = -1;
            _Commande.Parameters.Add("@ID_client", SqlDbType.Int);
            _Commande.Parameters["@ID_client"].Direction = ParameterDirection.Output;
            _Commande.Parameters.AddWithValue("@client_prenom", Pre_);
            _Commande.Parameters.AddWithValue("@client_nom", Nom_);
            _Commande.Parameters.AddWithValue("@client_nai", Nai_);
            _Commande.Parameters.AddWithValue("@client_crea", Crea_);
            _Commande.Parameters.AddWithValue("@client_cotisation", Coti_);
            _Commande.Connection.Open();
            _Commande.ExecuteNonQuery();
            res = (int)_Commande.Parameters["@ID_client"].Value;
            _Commande.Connection.Close();
            return res;
        }

        public int ModifierClient(int ID_, string Nom_, string Pre_, DateTime Nai_, DateTime Crea_, DateTime Coti_)
        {
            _Commande.CommandText = "ModifierTB_client";
            int res = -1;
            _Commande.Parameters.AddWithValue("@ID_client", ID_);
            _Commande.Parameters.AddWithValue("@client_prenom", Pre_);
            _Commande.Parameters.AddWithValue("@client_nom", Nom_);
            _Commande.Parameters.AddWithValue("@client_nai", Nai_);
            _Commande.Parameters.AddWithValue("@client_crea", Crea_);
            _Commande.Parameters.AddWithValue("@client_cotisation", Coti_);
            _Commande.Connection.Open();
            res = _Commande.ExecuteNonQuery();
            _Commande.Connection.Close();
            return res;
        }

        public int SupprimerClient(int ID_)
        {
            _Commande.CommandText = "SupprimerTB_client";
            int res = -1;
            _Commande.Parameters.AddWithValue("@ID_client", ID_);
            _Commande.Connection.Open();
            res = _Commande.ExecuteNonQuery();
            _Commande.Connection.Close();
            return res;
        }

        public List<C_Client> LireClient()
        {
            List<C_Client> res = new List<C_Client>();
            _Commande.CommandText = "SelectionnerTB_client";
            _Commande.Parameters.AddWithValue("@Index", "ID");
            _Commande.Connection.Open();
            SqlDataReader dr = _Commande.ExecuteReader();
            while (dr.Read())
                res.Add(new C_Client(int.Parse(dr["ID_client"].ToString())
                 , dr["client_nom"].ToString(), dr["client_prenom"].ToString()
                 , DateTime.Parse(dr["client_nai"].ToString())
                 , DateTime.Parse(dr["client_crea"].ToString())
                 , DateTime.Parse(dr["client_cotisation"].ToString())));
            dr.Close();
            _Commande.Connection.Close();
            return res;
        }

        public C_Client LireClient_ID(int ID_)
        {
            _Commande.CommandText = "SelectionnerTB_client_ID";
            _Commande.Parameters.AddWithValue("@ID_client", ID_);
            _Commande.Connection.Open();
            SqlDataReader dr = _Commande.ExecuteReader();
            dr.Read();
            C_Client res = new C_Client(int.Parse(dr["ID_client"].ToString())
              , dr["client_nom"].ToString(), dr["client_prenom"].ToString()
              , DateTime.Parse(dr["client_nai"].ToString())
              , DateTime.Parse(dr["client_crea"].ToString())
              , DateTime.Parse(dr["client_cotisation"].ToString()));
            dr.Close();
            _Commande.Connection.Close();
            return res;
        }
    }
}
