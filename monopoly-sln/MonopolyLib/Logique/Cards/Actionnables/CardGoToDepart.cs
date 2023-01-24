using MonopolyLib.Logique.Joueurs;

namespace MonopolyLib.Logique.Cards.Actionnables
{
    public class CardGoToDepart : Card, IActionnable
    {
        public void ActOn(Joueur j)
        {
            j.GoTo(j.Partie.Board.Cases[j.Partie.Board.GetPosDépart()]);
        }
    }
}