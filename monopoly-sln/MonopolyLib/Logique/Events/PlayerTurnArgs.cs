using System;
using System.Collections.Generic;
using MonopolyLib.Logique.Cases;

namespace MonopolyLib.Logique.Events
{
    public class PlayerTurnArgs : EventArgs
    {
        public float OldArg { get; internal set; }
        public float NewArg { get; internal set; }
        
        public int OldPos { get; internal set; }
        public int NewPos { get; internal set; }
        
        public string NomJoueur { get; internal set; }

        public List<CaseAchetable> CasesAchetees { get; internal set; } = new List<CaseAchetable>();

        public PlayerTurnArgs(float oldArg, float newArg, int oldPos, int newPos, string nom)
        {
            OldArg = oldArg;
            NewArg = newArg;

            OldPos = oldPos;
            NewPos = newPos;

            NomJoueur = nom;
        }
    }
}