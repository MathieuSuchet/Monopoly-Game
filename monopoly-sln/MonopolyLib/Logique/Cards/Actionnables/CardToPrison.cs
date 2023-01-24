using System.Linq;
using MonopolyLib.Logique.Cases;
using MonopolyLib.Logique.Joueurs;

namespace MonopolyLib.Logique.Cards.Actionnables
{
    public class CardToPrison : Card, IActionnable
    {
        public void ActOn(Joueur j)
        {
            j.Position = j.Partie.Board.Cases.First(x => x is CasePrison).Position;
            j.EnPrison = true;
        }
    }
}