using System;

namespace MonopolyLib.Logique.Events
{
    public class PlayerMoneyMovementArgs : EventArgs
    {
        public float Argent;
        public bool Lost;

        public PlayerMoneyMovementArgs(float argent, bool lost)
        {
            Argent = argent;
            Lost = lost;
        }
    }
}