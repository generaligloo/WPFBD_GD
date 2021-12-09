namespace WPFBD_GD_1ER.Model
{
    public class AffichLivre
    {
        C_TB_livre _livreAff;
        string _nomCat;
        string _nomEdi;

        public AffichLivre()
        {

        }

        public C_TB_livre livreAff
        {
            get { return _livreAff; }
            set { _livreAff = value; }
        }

        public string nomCat
        {
            get { return _nomCat; }
            set { _nomCat = value; }
        }

        public string nomEdi
        {
            get { return _nomEdi; }
            set { _nomEdi = value; }
        }
    }
}