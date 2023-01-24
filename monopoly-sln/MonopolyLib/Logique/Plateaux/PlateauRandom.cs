using System;
using MonopolyLib.Logique.PartieComponents.Parties;

namespace MonopolyLib.Logique.Plateaux
{
    public class PlateauRandom : Plateau
    {
        public PlateauRandom(Partie p) : base(p)
        {
            #region Création du random

            Random rng = new Random();
            int n = Cases.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Cases[k].Position = n;
                (Cases[k], Cases[n]) = (Cases[n], Cases[k]);
            }

            #endregion
        }

        public PlateauRandom(int numberOfCardSets, Partie p) : base(numberOfCardSets, p)
        {
            #region Création du random

            Random rng = new Random();
            int n = Cases.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Cases[k].Position = n;
                (Cases[k], Cases[n]) = (Cases[n], Cases[k]);
            }

            #endregion
        }
    }
}