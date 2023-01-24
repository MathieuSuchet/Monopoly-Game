using System;
using MonopolyLib.Logique.Plateaux;

namespace MonopolyLib.Logique.Events
{
    public class PlateauArgs : EventArgs
    {
        public Plateau Plateau;

        public PlateauArgs(Plateau p)
        {
            Plateau = p;
        }
    }
}