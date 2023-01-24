namespace MonopolyLib.Logique.Cases
{
    public class CasePrison : CaseVisitable
    {
        public override string ResumeCarte()
        {
            return base.ResumeCarte() + "Case prison : Allez en prison";
        }

        public CasePrison(string nom) : base(nom) { }
    }
}
