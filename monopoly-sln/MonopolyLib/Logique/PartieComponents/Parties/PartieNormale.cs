using System.Collections.Generic;
using MonopolyLib.Logique.Joueurs;
using MonopolyLib.Logique.Plateaux;

namespace MonopolyLib.Logique.PartieComponents.Parties
{
    public class PartieNormale : Partie
    {
        public PartieNormale(List<Joueur> joueurs, bool silent) : base(joueurs, silent)
        {
            Board = new PlateauNormal(this);
            
            for (int i = 0; i < Joueurs.Count; i++)
            {
                joueurs[i].Position = Board.GetPosDépart();
            }
        }
    }
}