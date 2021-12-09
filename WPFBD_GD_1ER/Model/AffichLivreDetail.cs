namespace WPFBD_GD_1ER.Model
{
    public class AffichLivreDetail
    {
        C_TB_details _detailAff;
        string _titre_auteur_livre;

        public AffichLivreDetail()
        {

        }

        public C_TB_details detailAff
        {
            get { return _detailAff; }
            set { _detailAff = value; }
        }

        public string titre_auteur_livre
        {
            get { return _titre_auteur_livre; }
            set { _titre_auteur_livre = value; }
        }
    }
}