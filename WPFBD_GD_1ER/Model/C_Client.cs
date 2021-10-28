using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFBD_GD_1ER.Model
{
    public class C_Client
    {
        private int _ID;
        private string _Nom, _Pre;
        private DateTime _Nai, _Coti, _Crea;

        public C_Client()
        { }

        public C_Client(string Nom_, string Pre_, DateTime Nai_, DateTime Crea_, DateTime Coti_)
        {
            Nom = Nom_;
            Pre = Pre_;
            Nai = Nai_;
            Coti = Coti_;
            Crea = Crea_;
        }

        public C_Client(int ID_, string Nom_, string Pre_, DateTime Nai_, DateTime Crea_, DateTime Coti_)
          : this(Nom_, Pre_, Nai_, Crea_, Coti_)
        { ID = ID_; }

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string Nom
        {
            get { return _Nom; }
            set { _Nom = value; }
        }

        public string Pre
        {
            get { return _Pre; }
            set { _Pre = value; }
        }

        public DateTime Nai
        {
            get { return _Nai; }
            set { _Nai = value; }
        }

        public DateTime Coti
        {
            get { return _Coti; }
            set { _Coti = value; }
        }

        public DateTime Crea
        {
            get { return _Crea; }
            set { _Crea = value; }
        }
    }

}
