using MonopolyLib.Logique.Joueurs;

namespace MonopolyLib.Logique.Joueurs.AjouteursArgent
{
    public abstract class AjouteurArgent : PlayerComponentsMother
    {
        public AjouteurArgent(Joueur j) : base(j) { }

        public virtual void AjouterArgent(float value)
        {
            Player.OnMoneyChanged(value, false);
        }
    }
}
