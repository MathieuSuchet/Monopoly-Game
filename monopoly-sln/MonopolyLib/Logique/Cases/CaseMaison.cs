namespace MonopolyLib.Logique.Cases
{
    public class CaseMaison : CaseAchetable
    {

        public int CotePlateau { get; private set; }


        private int NbMaisonsAttr;
        public int NbMaisons
        {
            get => NbMaisonsAttr;
            set
            {
                if (value > 5)
                {
                    NbMaisonsAttr = 5;
                }
                else
                {
                    NbMaisonsAttr = value;
                }
            }
        }
        public float PrixUnitMaison { get; set; }

        private float Prix1Maison { get; }
        private float Prix2Maison { get; }
        private float Prix3Maison { get; }
        private float Prix4Maison { get; }
        private float PrixHotel { get; }

        public string Couleur { get; }
        public CaseMaison(string couleur, float prixAchat, string nom, int cote, float prix, float prix1, float prix2, float prix3, float prix4, float prixHotel) : base(nom, prix, prixAchat)
        {

            CotePlateau = cote;
            Couleur = couleur;

            Prix1Maison = prix1;
            Prix2Maison = prix2;
            Prix3Maison = prix3;
            Prix4Maison = prix4;
            PrixHotel = prixHotel;
            Achetée = false;

            NbMaisons = 0;
            PrixUnitMaison = cote * 50;
        }
        public override string ResumeCarte()
        {
            return base.ResumeCarte() +
                "Prix Nu : " + Prix + "\n" +
                "\n////////////////////////\n\n" +
                "Prix avec 1 maison : " + Prix1Maison + "\n" +
                "Prix avec 2 maisons : " + Prix2Maison + "\n" +
                "Prix avec 3 maisons : " + Prix3Maison + "\n" +
                "Prix avec 4 maisons : " + Prix4Maison + "\n" +
                "Prix avec Hotel : " + PrixHotel + "\n" +
                "\n//////////////////////\n\n" +
                "Prix d'une maison : " + PrixUnitMaison + "\n";
        }

        protected override float GetPrixFinal()
        {
            return NbMaisonsAttr switch
            {
                0 => Prix,
                1 => Prix1Maison,
                2 => Prix2Maison,
                3 => Prix3Maison,
                4 => Prix4Maison,
                5 => PrixHotel,
                _ => 0
            };
        }

        protected override void SetPrixFinal(float value)
        {
            PrixFinalAttr = value;
        }
    }
}
