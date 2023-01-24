using System;
using System.ComponentModel;

namespace MonopolyLib.Logique.Cards
{
    public class Card
    {
        private Usability _usage;

        public string Intitule;

        protected Usability Usage
        {
            get => _usage;
            set
            {
                if (!Enum.IsDefined(typeof(Usability), value))
                    throw new InvalidEnumArgumentException(nameof(value), (int)value, typeof(Usability));
                _usage = value;
                NumberOfUse = Usage switch
                {
                    Usability.Unique => 1,
                    Usability.Always => -1,
                    _ => NumberOfUse
                };
            }
        }

        public int NumberOfUse;
    }
}