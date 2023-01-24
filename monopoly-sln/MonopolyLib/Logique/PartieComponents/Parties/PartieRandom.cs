using System.Collections.Generic;
using MonopolyLib.Logique.Joueurs;
using MonopolyLib.Logique.Plateaux;

namespace MonopolyLib.Logique.PartieComponents.Parties
{
    public class PartieRandom : Partie
    {
        public PartieRandom(List<Joueur> joueurs, bool silent) : base(joueurs, silent)
        {
            Board = new PlateauRandom(this);
            
            for (int i = 0; i < Joueurs.Count; i++)
            {
                joueurs[i].Position = Board.GetPosDépart();
            }
        }
    }
}