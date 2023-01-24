using System;
using System.Collections.Generic;
using System.Linq;
using MonopolyLib.Logique.Joueurs;

namespace MonopolyLib.Logique.Cards.Keepables
{
    public class CardXTimes : Card, IKeepable
    {
        private int _factor;
        public int Factor
        {
            get => _factor;
            set
            {
                _factor = value;
                Intitule = "Faire payer " + Factor + " fois le prochain loyer à quelqu'un";
            }
        }

        public CardXTimes()
        {
            Factor = 2;
            Usage = Usability.Unique;
            Intitule = "Faire payer " + Factor + " fois le prochain loyer à quelqu'un";
        }

        public CardXTimes(int next)
        {
            Factor = next;
            Usage = Usability.Unique;
            NumberOfUse = 1;
            Intitule = "Faire payer " + Factor + " fois le prochain loyer à quelqu'un";
        }

        public void UseCard(Joueur? j)
        {
            List<Joueur?> joueurs = j.Partie.Joueurs;
            
            Joueur? cible = joueurs.Find(x =>
                Math.Abs(x.ProfitCalculator.EstimateAverageProfit(3, true) - joueurs.Max(y => y.ProfitCalculator.EstimateAverageProfit(3, true))) < 0.0001);

            if (cible is null)
            {
                return;
            }

            cible.Multiplicateurs[j] = _factor;
            
            /*
            var mults = cible?.Multiplicateurs.ToArray();
            cible?.Multiplicateurs.CopyTo(mults);
            foreach (KeyValuePair<Joueur?, float> entry in mults)
            {
                if (entry.Key != j) continue;
                mults.SetValue(entry.Key,Factor);
            }*/


            NumberOfUse--;
            j.Historique.Add(new KeyValuePair<string, float>($"Tour {j.Partie.NbTours} : Utilisation de la carte \"{Intitule}\" à {cible.Nom}",0));
            if (NumberOfUse == 0)
            {
                j.RemoveCard(this);
            }
        }
    }
}