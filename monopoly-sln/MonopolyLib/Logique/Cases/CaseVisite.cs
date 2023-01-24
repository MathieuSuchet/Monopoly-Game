namespace MonopolyLib.Logique.Cases
{
    public class CaseVisite : CaseVisitable
    {
        public override string ResumeCarte()
        {
            return base.ResumeCarte() + "Case Visite simple";
        }

        public CaseVisite(string nom) : base(nom) { }
    }
}
