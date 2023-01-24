using System.Collections.Generic;
using MonopolyLib.Logique.Joueurs;

namespace MonopolyLib.Logique.Cards.Keepables
{
    public class CardOutOfPrison : Card, IKeepable
    {
        public CardOutOfPrison()
        {
            Intitule = "Sortez de prison ! Utilisation Unique";
            Usage = Usability.Unique;
        }

        public void UseCard(Joueur? j)
        {
            if (j.EnPrison)
            {
                j.EnPrison = false;
            }
            else
            {
                //Console.WriteLine($"{j.Nom} n'est pas en prison !");
                return;
            }
            
            NumberOfUse--;
            j.Historique.Add(new KeyValuePair<string, float>($"Tour {j.Partie.NbTours} : Utilisation de la carte \"{Intitule}\"",0));
            if (NumberOfUse == 0)
            {
                j.RemoveCard(this);
            }
        }
    }
}