using MonopolyLib.Logique.Cases;
using MonopolyLib.Logique.Joueurs;

namespace MonopolyLib.Logique.Joueurs.VendeursMaison
{
    public abstract class VendeurMaison : PlayerComponentsMother
    {
        public VendeurMaison(Joueur? j) : base(j) { }

        public abstract void VendreMaison(CaseMaison c);

        public virtual void VendreMaison(CaseMaison c, int nbMaisons) { }
    }
}
