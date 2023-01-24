using System;

namespace MonopolyLib.Logique.Cases
{
    public class CaseInteract : CaseVisitable
    {
        public string Intitule { get; }

        public int Value { get; private set; }

        public CaseInteract(string nom) : base(nom)
        {
            
        }
        

        public override string ResumeCarte()
        {
            return base.ResumeCarte() + "Case chance/Caisse de communauté";
        }
    }
}
