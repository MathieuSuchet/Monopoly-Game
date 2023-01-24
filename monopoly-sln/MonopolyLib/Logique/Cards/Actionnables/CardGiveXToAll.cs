using System.Linq;
using MonopolyLib.Logique.Joueurs;

namespace MonopolyLib.Logique.Cards.Actionnables
{
    public class CardGiveXToAll : Card, IActionnable
    {
        public int Value;

        public CardGiveXToAll(int value)
        {
            Value = value;
        }

        public void ActOn(Joueur j)
        {
            var list = j.Partie.Joueurs.Where(joueur => joueur != j).ToList();

            if (j.RetirerArgent(Value * list.Count)) return;
            j.GestionnaireVente.VendreJusquaRemboursement(Value);
            if (j.Faillite) return;
            foreach (var joueur in list)
            {
                joueur.AjouterArgent(Value);
                j.RetirerArgent(Value);
            }
        }
    }
}