using System.Collections.Generic;
using System.Diagnostics;
using MonopolyLib.Logique.Joueurs;
using MonopolyLib.Logique.Plateaux;

namespace MonopolyLib.Logique.PartieComponents.Parties
{
    public class PartieChaos : Partie
    {

        public bool ExtendedBoard;
        public PartieChaos(List<Joueur> joueurs, bool silent, bool extendedBoard) : base(joueurs, silent)
        {

            ExtendedBoard = extendedBoard;
            //Initialize board
            Board = new PlateauChaos(this, extendedBoard);
            Trace.WriteLine(Board.Cases.Count);
        }
    }
}