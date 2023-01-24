using System;

namespace MonopolyLib.Logique.Events
{
    public class PlayerPrisonArgs : EventArgs
    {
        public bool InPrison;

        public PlayerPrisonArgs(bool inPrison)
        {
            InPrison = inPrison;
        }
    }
}