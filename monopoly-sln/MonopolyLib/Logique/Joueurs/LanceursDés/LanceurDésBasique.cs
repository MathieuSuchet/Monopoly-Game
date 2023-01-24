using System;
using System.Collections.Generic;

namespace MonopolyLib.Logique.Joueurs.LanceursDés
{
    public class LanceurDésBasique : LanceurDés
    {

        public LanceurDésBasique(Joueur? j) : base(j) { }
        public override List<int> LancerDés()
        {
            List<int> val = new List<int>();
            Random rd = new Random();
            for (int i = 0; i < 2; i++)
            {
                int value = rd.Next(1, 7);
                val.Add(value);
            }

            //Console.Write("[");
            //foreach(int v in val)
            //{
            //    Console.Write(v + ",");
            //}
            //Console.Write("]\n");

            return val;
        }
    }
}
