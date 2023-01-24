using System.Collections.Generic;
using MonopolyLib.Logique.Cases;

namespace MonopolyLib.Logique.Joueurs.VendeursCase
{

    class VendeurCaseBasique : VendeurCase
    {

        public VendeurCaseBasique(Joueur? j) : base(j) { }
        public override void VendreUneCase(CaseAchetable c)
        {
            float argentVente;
            if (c is CaseMaison caseMaison)
            {
                argentVente = caseMaison.PrixAchat + caseMaison.NbMaisons * caseMaison.PrixUnitMaison;
                caseMaison.NbMaisons = 0;
                foreach (CaseAchetable caseAchetable in Player.Cases)
                {
                    if (!(caseAchetable is CaseMaison caseMaison1)) continue;
                    if (caseMaison1.Couleur != caseMaison.Couleur) continue;
                    argentVente += (float)0.9 * (caseMaison1.NbMaisons * caseMaison1.PrixUnitMaison);
                    Player.OnHouseSell(caseMaison1.NbMaisons, caseMaison1);
                    caseMaison1.NbMaisons = 0;
                }
                Player.Historique.Add(new KeyValuePair<string, float>("Tour " + Player.Partie.NbTours + " : Vente de " + c.Nom, argentVente));
                
            }
            else
            {
                argentVente = c.PrixAchat;
            }
            Player.AjouterArgent(argentVente);
            c.Proprio = null;
            c.Achetée = false;
            Player.Cases.Remove(c);
            Player.OnCaseSell(argentVente, c);
        }
    }
}
