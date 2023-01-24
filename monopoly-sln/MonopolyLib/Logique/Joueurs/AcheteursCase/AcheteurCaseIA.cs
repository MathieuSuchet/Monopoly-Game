using System;
using System.Collections.Generic;
using MonopolyLib.Logique.Cases;
using MonopolyLib.Logique.Joueurs;
using MonopolyLib.Logique.PartieComponents.Parties;

namespace MonopolyLib.Logique.Joueurs.AcheteursCase
{
    class AcheteurCaseIA : AcheteurCase
    {

        public AcheteurCaseIA(Joueur j) : base(j) { }
        public override void AcheterUneCase(CaseAchetable c)
        {
            if (c.Achetée)
            {
                return;
            }

            Random random = new Random();
            if (Player.Argent >= c.PrixAchat && Player.ProfitCalculator.EstimateDanger() < Player.Argent - c.PrixAchat && random.NextDouble() + (Player.Agressivite / 100f) > 1)
            {
                Player.RetirerArgent(c.PrixAchat);
                Player.Cases.Add(c);
                c.Achetée = true;
                c.Proprio = Player;
                Player.NbProperties++;
                //Console.WriteLine(Player.Nom + " bought " + c.Nom);
                Player.Historique.Add(new KeyValuePair<string, float>("Achat de la case " + c.Nom, -c.PrixAchat));
                Player.OnCaseBuy(c.PrixAchat, c);
            }
            else
            {
                // if (Player.Argent < c.PrixAchat)
                //     Console.WriteLine(Player.Nom + " refused to buy " + c.Nom + " (Not enough money)");
                // else if (Player.ProfitCalculator.EstimateDanger() > Player.Argent - c.PrixAchat)
                //     Console.WriteLine(Player.Nom + " refused to buy " + c.Nom + " (Danger too high (Risk = " + Player.ProfitCalculator.EstimateDanger() + ", Money after buying it = " + (Player.Argent - c.PrixAchat) + "))");
                // else if (random.NextDouble() + (Player.agressivite / 100f) < 1)
                //     Console.WriteLine(Player.Nom + " refused to buy " + c.Nom + " (Not agressive enough : " + Player.agressivite + ")");
                
            }
        }

        public override void RacheterUneCase(CaseAchetable c)
        {
            Random random = new Random();
            var danger = Player.ProfitCalculator.EstimateDanger();
            var rand = random.NextDouble();
            if (Player.Argent >= 3 * (c.PrixFinal + c.PrixAchat) && danger < Player.Argent - 3 * (c.PrixFinal + c.PrixAchat) && rand < Player.Agressivite / 100f)
            {
                Player.RetirerArgent(3 * (c.PrixFinal + c.PrixAchat));
                c.Proprio.AjouterArgent(3 * (c.PrixFinal + c.PrixAchat));
                Player.OnTransaction(3 * (c.PrixFinal + c.PrixAchat), Player, c.Proprio, $"Rachat de {c.Nom}");
                c.Proprio.Cases.Remove(c);
                c.Proprio.NbProperties--;
                Player.Cases.Add(c);
                c.Proprio = Player;
                Player.NbProperties++;
                //Console.WriteLine(Player.Nom + " bought " + c.Nom);
                Player.Historique.Add(new KeyValuePair<string, float>("Achat de la case " + c.Nom, -3 * (c.PrixFinal + c.PrixAchat)));
                
                Player.OnCaseBuy(3 * (c.PrixFinal + c.PrixAchat), c);
            }
        }
    }
}
