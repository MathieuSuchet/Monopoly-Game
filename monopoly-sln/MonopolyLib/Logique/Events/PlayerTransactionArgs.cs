using System;
using MonopolyLib.Logique.Joueurs;

namespace MonopolyLib.Logique.Events
{
    public class PlayerTransactionArgs : EventArgs
    {
        public float Change;
        public Joueur Source;
        public Joueur? Dest;
        public string Reason;

        public PlayerTransactionArgs(float change, Joueur? source, Joueur? dest, string reason)
        {
            Change = change;
            Source = source;
            Dest = dest;
            Reason = reason;
        }
    }
}