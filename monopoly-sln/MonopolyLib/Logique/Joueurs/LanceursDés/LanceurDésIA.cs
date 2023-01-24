using System;
using System.Collections.Generic;
using MonopolyLib.Logique.Joueurs;

namespace MonopolyLib.Logique.Joueurs.LanceursDés
{
    internal class LanceurDésIa : LanceurDés
    {

        public LanceurDésIa(Joueur? j) : base(j) { }
        public override List<int> LancerDés()
        {
            List<int> val = new List<int>();
            var rd = new Random();
            for (int i = 0; i < 2; i++)
            {
                val.Add(rd.Next(1, 7));
            }

            return val;
        }
    }
}
