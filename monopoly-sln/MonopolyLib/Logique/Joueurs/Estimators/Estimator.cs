using System.Collections.Generic;
using MonopolyLib.Logique.Cases;

namespace MonopolyLib.Logique.Joueurs.Estimators
{
    public class Estimator
    {
        public virtual float EstimateNecessaryMoney(float tobepaid, float money)
        {
            return tobepaid - money;
        }
        public virtual float EstimateValueAllProperties(List<CaseAchetable> caseAchetables)
        {
            float worth = 0;
            foreach (CaseAchetable c in caseAchetables)
            {
                worth += c.PrixAchat;
                if (c is CaseMaison caseMaison)
                {
                    worth += caseMaison.PrixUnitMaison * caseMaison.NbMaisons;
                }
            }
            return worth;
        }

        public virtual float EstimateValueOf(CaseAchetable caseAchetable)
        {
            float worth = caseAchetable.PrixAchat;

            if (caseAchetable is CaseMaison caseMaison)
            {
                worth += caseMaison.NbMaisons * caseMaison.PrixUnitMaison;
            }

            return worth;
        }

        public virtual EstimatorResult EstimateImpactOfSelling(List<CaseAchetable> soldCases, List<CaseAchetable> cases)
        {
            EstimatorResult result = new EstimatorResult();

            float argentFinal = 0;

            foreach (CaseAchetable c in soldCases)
            {
                cases.Remove(c);
                argentFinal += c.PrixAchat;

                #region Cas où il y a des maisons sur les autres cases de la meme couleur
                if (c is CaseMaison caseMaison)
                {
                    foreach (CaseAchetable c2 in cases)
                    {
                        if (c2 is CaseMaison caseMaison1)
                        {
                            if (caseMaison1.Couleur == caseMaison.Couleur)
                            {
                                argentFinal += caseMaison1.NbMaisons * caseMaison1.PrixUnitMaison;
                                caseMaison1.NbMaisons = 0;
                            }
                        }
                    }
                }
                #endregion
            }


            result.Argent = argentFinal;
            result.Cases = cases;
            return result;
        }
    }

}
