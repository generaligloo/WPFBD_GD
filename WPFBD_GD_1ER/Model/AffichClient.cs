namespace WPFBD_GD_1ER.Model
{
    public class AffichClient
    {
        C_TB_client _clientAff;
        bool _retardCota;

        public AffichClient()
        {

        }

        public C_TB_client clientAff
        {
            get { return _clientAff; }
            set { _clientAff = value; }
        }

        public bool retardCota
        {
            get { return _retardCota; }
            set { _retardCota = value; }
        }
    }
}