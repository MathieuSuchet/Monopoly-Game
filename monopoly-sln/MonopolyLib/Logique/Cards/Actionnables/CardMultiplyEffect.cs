using System.Collections.Generic;
using MonopolyLib.Logique.Cards.Keepables;
using MonopolyLib.Logique.Joueurs;

namespace MonopolyLib.Logique.Cards.Actionnables
{
    public class CardMultiplyEffect : Card, IActionnable
    {
        public CardMultiplyEffect()
        {
            Intitule = "Vos cartes passives ont leurs effets mutlipliés par 2";
            Usage = Usability.Always;
        }
        public void ActOn(Joueur j)
        {
            for (int i = 0; i < j.Cards.Count; i++)
            {
                IKeepable card = j.Cards[i];
                if (card is CardXTimes cardXTimes)
                {
                    cardXTimes.Factor *= 2;
                }
            }

            j.Historique.Add(new KeyValuePair<string, float>($"Tour {j.Partie.NbTours} : Multiplication des effets",0));
            NumberOfUse--;
        }
    }
}