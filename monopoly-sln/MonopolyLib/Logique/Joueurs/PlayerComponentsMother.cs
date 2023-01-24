using MonopolyLib.Logique.Joueurs;

namespace MonopolyLib.Logique.Joueurs
{
    public abstract class PlayerComponentsMother
    {
        protected Joueur Player { get; set; }

        public PlayerComponentsMother(Joueur j)
        {
            Player = j;
        }
    }
}
