using MonopolyLib.Logique.Cases;
using MonopolyLib.Logique.Joueurs;

namespace MonopolyLib.Logique.Joueurs.AcheteursCase
{
    public abstract class AcheteurCase : PlayerComponentsMother
    {
        public AcheteurCase(Joueur? joueur) : base(joueur) { }

        public abstract void AcheterUneCase(CaseAchetable c);

        public abstract void RacheterUneCase(CaseAchetable c);
    }
}
