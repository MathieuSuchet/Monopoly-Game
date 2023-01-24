using System.Collections.Generic;
using MonopolyLib.Logique.Cases;
using MonopolyLib.Logique.Joueurs;

namespace MonopolyLib.Logique.Joueurs.GestionnairesFaillite
{
    public class GestionnaireFailliteBasique : GestionnaireFaillite
    {
        public GestionnaireFailliteBasique(Joueur? j) : base(j) { }

        public override void FaireFaillite()
        {
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
            Player.OnFaillite();
        }
    }
}
