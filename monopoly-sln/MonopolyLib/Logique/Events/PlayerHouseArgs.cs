using System;
using MonopolyLib.Logique.Cases;

namespace MonopolyLib.Logique.Events
{
    public class PlayerHouseArgs : EventArgs
    {
        public int NbMaisons;
        public CaseMaison Target;

        public PlayerHouseArgs(CaseMaison target, int nbMaisons)
        {
            NbMaisons = nbMaisons;
            Target = target;
        }
    }
}