using System.Collections.Generic;
using System.Linq;
using MonopolyLib.Logique.Cases;
using MonopolyLib.Logique.Joueurs;

namespace MonopolyLib.Logique.Joueurs.GestionnairesFaillite
{
    public class GestionnaireFailliteIa : GestionnaireFaillite
    {
        public GestionnaireFailliteIa(Joueur? j) : base(j) { }
        public override void FaireFaillite()
        {
            foreach (var joueur in Player.Partie.Joueurs.Where(joueur => joueur != Player))
            {
                joueur.Multiplicateurs.Remove(Player);
            }
            
            foreach (CaseAchetable c in Player.Cases)
            {
                c.Proprio = null;
                c.Achetée = false;
            }
            Player.Partie.Faillites.Add(Player);
            Player.Partie.Joueurs.Remove(Player);
            Player.Cases.Clear();
            Player.Partie.FinishedTurn = true;
            Player.Faillite = true;
            Player.Historique.Add(new KeyValuePair<string, float>("Tour " + Player.Partie.NbTours + " : Faillite ", 0));
        }
    }
}
