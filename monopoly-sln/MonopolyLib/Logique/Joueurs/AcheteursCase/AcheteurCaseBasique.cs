using System;
using System.Collections.Generic;
using MonopolyLib.Logique.Cases;
using MonopolyLib.Logique.Joueurs;

namespace MonopolyLib.Logique.Joueurs.AcheteursCase
{
    class AcheteurCaseBasique : AcheteurCase
    {
        public AcheteurCaseBasique(Joueur joueur) : base(joueur) { }

        public override void AcheterUneCase(CaseAchetable c)
        {
            if (c.Achetée)
            {
                return;
            }
            if (Player.RetirerArgent(c.PrixAchat))
            {
                Console.WriteLine("Case " + c.Nom + " achetée avec succès");
                Player.Historique.Add(new KeyValuePair<string, float>("Tour " + Player.Partie.NbTours + " : Achat de " + c.Nom, -c.PrixAchat));
            }
            else
            {
                Console.WriteLine("Problème lors de l'achat de la case " + c.Nom);
                return;
            }
            c.Proprio = Player;
            c.Achetée = true;
            Player.Cases.Add(c);
            Player.OnCaseBuy(c.PrixAchat, c);
        }

        public override void RacheterUneCase(CaseAchetable c)
        {
            if(!Player.RetirerArgent(c.PrixFinal * 3))
            {
                Console.WriteLine("Problème lors du rachat de la case " + c.Nom);
                return;
            }
            c.Proprio.VendreUneCase(c);
            c.Proprio = Player;
            c.Achetée = true;
            Player.Cases.Add(c);
            Player.OnCaseBuy(c.PrixAchat * 3, c);
        }
    }
}
