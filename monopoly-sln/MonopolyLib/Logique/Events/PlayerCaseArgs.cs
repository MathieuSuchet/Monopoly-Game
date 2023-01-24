using System;
using MonopolyLib.Logique.Cases;

namespace MonopolyLib.Logique.Events
{
    public class PlayerCaseArgs : EventArgs
    {
        public float Argent;
        public Case CaseAchetee;

        public PlayerCaseArgs(float argent, Case @case)
        {
            Argent = argent;
            CaseAchetee = @case;
        }
    }
}