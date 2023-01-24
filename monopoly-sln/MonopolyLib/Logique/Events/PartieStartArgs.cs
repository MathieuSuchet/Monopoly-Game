using System;
using MonopolyLib.Logique.PartieComponents.Parties;

namespace MonopolyLib.Logique.Events
{
    public class PartieStartArgs : EventArgs
    {
        public Partie Partie { get; set; }
        
        public string Resume { get; set; }

        public PartieStartArgs(Partie p, string r)
        {
            Partie = p;
            Resume = r;
        }
    }
}