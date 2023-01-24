using System.Collections.Generic;
using System.Linq;
using MonopolyLib.Logique.PartieComponents.Parties;

namespace MonopolyLib.Logique.Cases
{
    public class CaseEauElec : CaseAchetable
    {
        private static List<CaseEauElec> All = new List<CaseEauElec>();

        public CaseEauElec(string nom) : base(nom, 0, 150)
        {
            All.Add(this);
        }

        public CaseEauElec(string nom, float prix) : base(nom, prix, 150)
        {
            All.Add(this);
        }

        public CaseEauElec(string nom, float prix, float prixAchat) : base(nom, prix, prixAchat)
        {
            All.Add(this);
        }

        protected override float GetPrixFinal()
        {
            int count = All.Where(x => x.Proprio == Proprio).ToArray().Length;
            return count switch
            {
                0 => 0,
                1 => 4 * Partie.Roll,
                2 => 10 * Partie.Roll,
                _ => 0
            };
        }

        protected override void SetPrixFinal(float value)
        {
            PrixFinalAttr = value;
        }
    }
}
