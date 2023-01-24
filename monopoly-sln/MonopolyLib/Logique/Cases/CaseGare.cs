using System;

namespace MonopolyLib.Logique.Cases
{
    public class CaseGare : CaseAchetable
    {
        public float PrixParGare { get; private set; }

        public CaseGare(string nom, float prix, float prixAchat) : base(nom, prix, prixAchat)
        {
            PrixParGare = prix;
        }

        public override string ResumeCarte()
        {
            return base.ResumeCarte() +
                "Prix avec une gare : " + Prix + "\n" +
                "Prix avec deux gares : " + PrixParGare * 2 + "\n" +
                "Prix avec trois gares : " + PrixParGare * 3 + "\n" +
                "Prix avec quatre gares : " + PrixParGare * 4 + "\n" +
                "Prix d'achat : " + PrixAchat + "\n";
        }

        protected override float GetPrixFinal()
        {
            int cpt = 0;
            foreach (CaseAchetable c in Proprio.Cases)
            {
                if (c is CaseGare caseGare)
                {
                    cpt++;
                }
            }
            float value = cpt * PrixParGare;
            return value;
        }

        protected override void SetPrixFinal(float value)
        {
            throw new NotImplementedException();
        }
    }
}
