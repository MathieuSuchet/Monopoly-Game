using MonopolyLib.Logique.Cases;

namespace MonopolyLib.Logique.Joueurs.VendeursCase
{
    public abstract class VendeurCase : PlayerComponentsMother
    {
        public VendeurCase(Joueur? j) : base(j) { }
        public abstract void VendreUneCase(CaseAchetable c);
    }
}
