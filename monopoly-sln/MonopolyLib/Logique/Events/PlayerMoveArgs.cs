using System;

namespace MonopolyLib.Logique.Events
{
    public class PlayerMoveArgs : EventArgs
    {
        public int OldPosition;
        public int NewPosition;

        public PlayerMoveArgs(int oldpos, int newpos)
        {
            OldPosition = oldpos;
            NewPosition = newpos;
        }
    }
}