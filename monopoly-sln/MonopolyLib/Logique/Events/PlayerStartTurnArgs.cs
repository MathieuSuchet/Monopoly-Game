using System;

namespace MonopolyLib.Logique.Events
{
    public class PlayerStartTurnArgs : EventArgs
    {
        public int Position;
        public float Argent;

        public PlayerStartTurnArgs(int position, float argent)
        {
            Position = position;
            Argent = argent;
        }
    }
}