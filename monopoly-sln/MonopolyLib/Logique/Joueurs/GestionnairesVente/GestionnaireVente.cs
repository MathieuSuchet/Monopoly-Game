using System.Collections.Generic;
using MonopolyLib.Logique.Cases;
using MonopolyLib.Logique.Joueurs.VendeursCase;

namespace MonopolyLib.Logique.Joueurs.GestionnairesVente
{
    internal abstract class GestionnaireVente : PlayerComponentsMother
    {
        protected VendeurCase VendeurCase { get; set; }
        protected GestionnaireVente(Joueur? j, VendeurCase vendeurCase) : base(j)
        {
            VendeurCase = vendeurCase;
        }

        private int Comparison(KeyValuePair<CaseAchetable, float> a, KeyValuePair<CaseAchetable, float> b)
        {
            return a.Value.CompareTo(b.Value);
        }

        internal abstract void VendreJusquaRemboursement(float value);

        internal abstract void VendreOpti();

        protected CaseAchetable GetBestCaseToSell()
        {
            List<KeyValuePair<CaseAchetable, float>> profits = new List<KeyValuePair<CaseAchetable, float>>();

            foreach (CaseAchetable caseAchetable in Player.Cases)
            {
                profits.Add(new KeyValuePair<CaseAchetable, float>(caseAchetable, caseAchetable.Profit));
            }

            if (profits.Count == 0)
            {
                Player.FaitFaillite();
                return null;
            }

            profits.Sort(Comparison);
            return profits[0].Key;
        }

        internal virtual void VendreParChoix(float value)
        {
        }

    }
}
