using System.Collections.Generic;

namespace MonopolyLib.Logique.Joueurs.LanceursDés
{
    public abstract class LanceurDés : PlayerComponentsMother
    {
        public LanceurDés(Joueur? j) : base(j) { }

        public abstract List<int> LancerDés();
    }
}
