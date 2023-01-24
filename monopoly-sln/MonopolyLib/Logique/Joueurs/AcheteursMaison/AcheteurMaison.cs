using MonopolyLib.Logique.Cases;
using MonopolyLib.Logique.Joueurs;

namespace MonopolyLib.Logique.Joueurs.AcheteursMaison
{
    public abstract class AcheteurMaison : PlayerComponentsMother
    {
        public AcheteurMaison(Joueur? j) : base(j) { }

        public abstract void AcheterMaison(CaseMaison c);
    }
}
