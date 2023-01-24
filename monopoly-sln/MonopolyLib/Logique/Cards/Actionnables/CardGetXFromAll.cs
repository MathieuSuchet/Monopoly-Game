using System.Linq;
using MonopolyLib.Logique.Joueurs;

namespace MonopolyLib.Logique.Cards.Actionnables
{
    public class CardGetXFromAll : Card, IActionnable
    {
        public int Value;

        public CardGetXFromAll(int value)
        {
            Value = value;
        }
        public void ActOn(Joueur j)
        {
            foreach (var joueur in j.Partie.Joueurs.Where(joueur => joueur != j))
            {
                if (!joueur.RetirerArgent(Value))
                {
                    joueur.GestionnaireVente.VendreJusquaRemboursement(Value);
                    if (!joueur.Faillite)
                    {
                        joueur.RetirerArgent(Value);
                    }
                    else
                    {
                        return;
                    }
                }
                j.AjouterArgent(Value);
            }
        }
    }
}