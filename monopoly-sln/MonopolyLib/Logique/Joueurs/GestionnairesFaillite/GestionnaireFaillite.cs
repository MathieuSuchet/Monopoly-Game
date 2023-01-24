using MonopolyLib.Logique.Joueurs;

namespace MonopolyLib.Logique.Joueurs.GestionnairesFaillite
{
    public abstract class GestionnaireFaillite : PlayerComponentsMother
    {
        public GestionnaireFaillite(Joueur? j) : base(j) { }

        public abstract void FaireFaillite();
    }
}
