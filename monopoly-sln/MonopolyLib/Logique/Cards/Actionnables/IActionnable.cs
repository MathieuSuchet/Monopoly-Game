using MonopolyLib.Logique.Joueurs;

namespace MonopolyLib.Logique.Cards.Actionnables
{
    public interface IActionnable
    {
        public void ActOn(Joueur j);
    }
}