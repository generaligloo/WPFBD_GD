using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFBD_GD_1ER.Model
{
    public class G_Client : G_Base
    {
        public G_Client()
         : base()
        { }

        public G_Client(string chConn)
         : base(chConn)
        { }

        public int AjouterClient(string Nom_, string Pre_, DateTime Nai_, DateTime Crea_ , DateTime Coti_)
        { return new A_Client(ChaineConnexion).AjouterClient(Nom_, Pre_, Nai_, Crea_, Coti_); }

        public int ModifierClient(int NumCli_, string Nom_, string Pre_, DateTime Nai_, DateTime Crea_, DateTime Coti_)
        { return new A_Client(ChaineConnexion).ModifierClient(NumCli_, Nom_, Pre_, Nai_, Crea_, Coti_); }

        public int SupprimerClient(int NumCli_)
        { return new A_Client(ChaineConnexion).SupprimerClient(NumCli_); }

        public List<C_Client> LireClient()
        { return new A_Client(ChaineConnexion).LireClient(); }

        public C_Client LireClient_ID(int NumCli_)
        { return new A_Client(ChaineConnexion).LireClient_ID(NumCli_); }
    }
}
