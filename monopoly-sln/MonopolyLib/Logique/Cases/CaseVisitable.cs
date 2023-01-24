namespace MonopolyLib.Logique.Cases
{
    public abstract class CaseVisitable : Case
    {
        public override string ResumeCarte()
        {
            return base.ResumeCarte();
        }

        public CaseVisitable(string nom) : base(nom) { }
    }
}
