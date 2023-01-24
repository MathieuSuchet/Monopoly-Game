using MonopolyLib.Logique.Joueurs;

namespace MonopolyLib.Logique.Cards.Keepables
{
    public interface IKeepable
    {
        public void UseCard(Joueur? j);
    }
}