using System.Collections.Generic;
using MonopolyLib.Logique.Joueurs;

namespace MonopolyLib.Logique.Cards.Keepables
{
    public class CardAllHouses : Card, IKeepable
    {
        public CardAllHouses()
        {
            Intitule = "Utilisez cette carte pour construire des maisons sur les autres propriétés de la couleur sur laquelle vous vous situez (Si celles-ci vous appartiennent)";
            Usage = Usability.Unique;
        }

        public void UseCard(Joueur? j)
        {
            NumberOfUse--;
            j.Historique.Add(new KeyValuePair<string, float>($"Tour {j.Partie.NbTours} : Utilisation de la carte \"{Intitule}\"",0));

            if (NumberOfUse == 0)
            {
                j.RemoveCard(this);
            }
        }
    }
}