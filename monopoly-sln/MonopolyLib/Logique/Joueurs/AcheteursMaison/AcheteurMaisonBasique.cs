using System;
using System.Collections.Generic;
using MonopolyLib.Logique.Cases;
using MonopolyLib.Logique.Joueurs;

namespace MonopolyLib.Logique.Joueurs.AcheteursMaison
{
    public class AcheteurMaisonBasique : AcheteurMaison
    {
        public AcheteurMaisonBasique(Joueur? j) : base(j) { }
        public override void AcheterMaison(CaseMaison c)
        {
            bool peutConstruire = true;
            int NbMaisonsMax = (int)Math.Floor(Player.Argent / c.PrixUnitMaison);
            if (NbMaisonsMax > (5 - c.NbMaisons))
            {
                NbMaisonsMax = 5 - c.NbMaisons;
            }
            if (NbMaisonsMax == 0)
            {
                peutConstruire = false;
            }
            List<string> choix = new List<string>();
            switch (NbMaisonsMax)
            {
                case 1:
                    choix.Add("Construire 1 maison");

                    break;
                case 2:
                    choix.Add("Construire 1 maison");
                    choix.Add("Construire 2 maisons");

                    break;
                case 3:
                    choix.Add("Construire 1 maison");
                    choix.Add("Construire 2 maisons");
                    choix.Add("Construire 3 maisons");

                    break;
                case 4:
                    choix.Add("Construire 1 maison");
                    choix.Add("Construire 2 maisons");
                    choix.Add("Construire 3 maisons");
                    choix.Add("Construire 4 maisons");

                    break;
                case 5:
                    choix.Add("Construire 1 maison");
                    choix.Add("Construire 2 maisons");
                    choix.Add("Construire 3 maisons");
                    choix.Add("Construire 4 maisons");
                    choix.Add("Construire un hotel");

                    break;
                default: break;
            }

            if (peutConstruire && HasAllSameColor(c))
            {
                int choice = Player.FaireChoix($"Vous posséder la case {c.Nom}", choix, new List<ConsoleKey>
                {
                    ConsoleKey.NumPad0,
                    ConsoleKey.NumPad1,
                    ConsoleKey.NumPad2,
                    ConsoleKey.NumPad3,
                    ConsoleKey.NumPad4
                });



                c.NbMaisons += (choice + 1);
                Player.RetirerArgent((choice + 1) * c.PrixUnitMaison);
            }
            if (!HasAllSameColor(c))
            {
                Console.WriteLine("Le joueur " + Player.Nom + " n'a pas toutes les cases de la meme couleur");
            }
        }

        private bool HasAllSameColor(CaseMaison caseMaison)
        {
            int cpt = 0;
            foreach (CaseAchetable caseAchetable in Player.Cases)
            {
                if (caseAchetable is CaseMaison caseMaison1)
                {
                    if (caseMaison1.Couleur == caseMaison.Couleur)
                    {
                        cpt++;
                    }
                }
            }
            return cpt == GetNumberSameColor(caseMaison);
        }

        private int GetNumberSameColor(CaseMaison caseMaison)
        {
            int cpt = 0;
            foreach (Case caseP in Player.Partie.Board.Cases)
            {
                if (caseP is CaseMaison caseMaison1)
                {
                    if (caseMaison1.Couleur == caseMaison.Couleur)
                    {
                        cpt++;
                    }
                }
            }
            return cpt;
        }
    }
}
