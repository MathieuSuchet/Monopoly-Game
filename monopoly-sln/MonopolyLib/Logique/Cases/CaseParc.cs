namespace MonopolyLib.Logique.Cases
{
    public class CaseParc : CaseVisitable
    {
        public override string ResumeCarte()
        {
            return base.ResumeCarte() + "\nCase parc gratuit : ";
        }

        public CaseParc(string nom) : base(nom) { }
    }
}
