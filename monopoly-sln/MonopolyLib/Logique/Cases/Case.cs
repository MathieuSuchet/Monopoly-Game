namespace MonopolyLib.Logique.Cases
{
    public abstract class Case : System.IComparable
    {
        public string Nom { get; protected set; }
        public int Position { get; internal set; }

        public virtual string ResumeCarte()
        {
            return "\n////////" + Nom + "////////\n\n";
        }

        public Case(string nom)
        {
            this.Nom = nom;
        }

        public int CompareTo(object other)
        {
            return string.CompareOrdinal(Nom, ((Case)other).Nom);
        }
    }
}
