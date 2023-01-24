namespace MonopolyLib.Logique.Cases
{
    public class CaseDépart : CaseVisitable
    {

        public float RécompensePassage { get; private set; }
        public override string ResumeCarte()
        {
            return base.ResumeCarte() + "Case départ\n" +
                "Récompense à chaque passage : " + RécompensePassage;
        }

        public CaseDépart(string nom, float récompense) : base(nom)
        {
            RécompensePassage = récompense;
        }
    }
}
