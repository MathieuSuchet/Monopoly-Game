using System;
using System.Collections.Generic;
using MonopolyLib.Logique.Joueurs;
using MonopolyLib.Logique.PartieComponents.Parties;

namespace MonopolyLib
{
    internal class Program
    {
        public static void Main(params string[] args)
        {
            List<Joueur> joueurs = new List<Joueur>
            {
                new JoueurIa("IA1", false),
                new JoueurIa("IA2", false),
                new JoueurIa("IA3", false),
                new JoueurIa("IA4", false)
            };

            Partie p = new PartieNormale(joueurs, false);
            
            p.Gestionnaire();
        }
    }
}