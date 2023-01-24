using System;
using System.Collections.Generic;
using MonopolyLib.Logique.Cases;
using MonopolyLib.Logique.Joueurs.VendeursCase;

namespace MonopolyLib.Logique.Joueurs.GestionnairesVente
{
    internal class GestionnaireVenteBasique : GestionnaireVente
    {
        internal GestionnaireVenteBasique(Joueur? j, VendeurCase vendeurCase) : base(j, vendeurCase) { }

        internal override void VendreJusquaRemboursement(float value)
        {
            throw new NotImplementedException();
        }

        internal override void VendreOpti()
        {
            CaseAchetable caseAchetable = GetBestCaseToSell();
            if (caseAchetable == null)
            {
                return;
            }
            VendeurCase.VendreUneCase(caseAchetable);
        }

        internal override void VendreParChoix(float value)
        {

            List<string> choix = new List<string>
            {
                "Vendre la case la moins lucrative",
                "Vendre la case de votre choix",
                "Faire faillite"
            };


            while (Player.Argent < value && Player.Cases.Count != 0)
            {
                int choice = Player.FaireChoix("Que voulez-vous faire ? (A rembourser : " + Math.Abs(value) + ") (Votre argent : " + Player.Argent + ")", choix, new List<ConsoleKey>
                {
                    ConsoleKey.NumPad0,
                    ConsoleKey.NumPad1,
                    ConsoleKey.NumPad2
                });
                if (choice == 0)
                {
                    if (Player.Cases.Count == 0)
                    {
                        Player.FaitFaillite();
                        return;
                    }
                    VendreOpti();
                }
                else if (choice == 1)
                {
                    if (Player.Cases.Count == 0)
                    {
                        Player.FaitFaillite();
                        return;
                    }
                }
                else
                {
                    Player.FaitFaillite();
                    return;
                }
            }
        }

        private void VendreCarteChoix()
        {
            List<string> choix = new List<string>();
            foreach (CaseAchetable caseAchetable in Player.Cases)
            {
                choix.Add($"{caseAchetable.Nom} : (Prix de vente : {caseAchetable.PrixAchat})");
            }

            Player.FaireChoix("Quelles cases vendre ?", choix, null);
            string t = Console.ReadLine();

            List<CaseAchetable> choices = new List<CaseAchetable>();

            string[] results = t.Split(' ');
            foreach (string result in results)
            {
                choices.Add(Player.Cases[int.Parse(result)]);
            }

            foreach (CaseAchetable choice in choices)
            {
                VendeurCase.VendreUneCase(choice);
            }
        }


    }
}
