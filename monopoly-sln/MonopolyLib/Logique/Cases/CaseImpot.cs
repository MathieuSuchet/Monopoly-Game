namespace MonopolyLib.Logique.Cases
{
    public class CaseImpot : CaseVisitable
    {
        public float PrixAPayer { get; }
        public override string ResumeCarte()
        {
            return base.ResumeCarte() + "Case Impot\n" +
                "Montant à payer : " + PrixAPayer;
        }

        public CaseImpot(string nom, float prix) : base(nom)
        {
            PrixAPayer = prix;
        }
    }
}
