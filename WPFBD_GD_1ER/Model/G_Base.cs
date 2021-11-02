namespace WPFBD_GD_1ER.Model
{
    public class G_Base
    {
        #region Données membres

        private string _ChaineConnexion;

        #endregion Données membres

        #region Constructeurs

        public G_Base()
        { ChaineConnexion = ""; }

        public G_Base(string sChaineConnexion)
        { ChaineConnexion = sChaineConnexion; }

        #endregion Constructeurs

        #region Accesseur

        public string ChaineConnexion
        {
            get { return _ChaineConnexion; }
            set { _ChaineConnexion = value; }
        }

        #endregion Accesseur
    }
}