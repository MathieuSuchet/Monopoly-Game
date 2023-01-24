using System;
using MonopolyLib.Logique.Cards;

namespace MonopolyLib.Logique.Events
{
    public class PlayerCardArgs : EventArgs
    {
        public Card DrawnCard;

        public PlayerCardArgs(Card card)
        {
            DrawnCard = card;
        }
    }
}