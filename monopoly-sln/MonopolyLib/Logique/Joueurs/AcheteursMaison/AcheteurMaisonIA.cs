using System;
using System.Collections.Generic;
using MonopolyLib.Logique.Cases;
using MonopolyLib.Logique.Joueurs;

namespace MonopolyLib.Logique.Joueurs.AcheteursMaison
{
    public class AcheteurMaisonIa : AcheteurMaison
    {
        public AcheteurMaisonIa(Joueur? j) : base(j) { }
        public override void AcheterMaison(CaseMaison c)
        {
            int nbMaisonsMax = (int)Math.Floor(Player.Argent / c.PrixUnitMaison);
            
            if (nbMaisonsMax > (5 - c.NbMaisons))
            {
                nbMaisonsMax = 5 - c.NbMaisons;
            }

            if (nbMaisonsMax == 0)
            {
                if (c.NbMaisons == 5)
                {
                    //Console.WriteLine(Player.Nom + " can't buy houses on " + c.Nom + " (" + c.Nom + " already have the maximum amount of houses)");
                }
                else
                {
                    //Console.WriteLine(Player.Nom + " can't buy houses on " + c.Nom + " (Not enough money)");
                }
                
                return;
            }

            int origin = nbMaisonsMax;
            
            //Tant que le danger est trop haut, on baisse le nombre de maisons constructibles
            while (Player.Argent - nbMaisonsMax * c.PrixUnitMaison < Player.ProfitCalculator.EstimateDanger() && nbMaisonsMax > 0)
            {
                //Cependant, si le joueur est agressif, on outrepasse cette barrière
                if (Player.Agressivite / 100f + new Random().NextDouble() > 1)
                {
                    //Console.WriteLine(Player.Nom + " is agressive, he chooses to build " + NbMaisonsMax + " house(s) on " + c.Nom);
                    Player.Historique.Add(new KeyValuePair<string, float>("Tour " + Player.Partie.NbTours + " : Bypassed the barrier ",0));
                    break;
                }
                nbMaisonsMax--;
            }

            if (nbMaisonsMax == 0)
            {
                Player.Historique.Add(new KeyValuePair<string, float>("Tour " + Player.Partie.NbTours + " : refused the construction of " + origin + " house(s) on " + c.Nom + ", danger too high (Risk = " +
                                                                      Player.ProfitCalculator.EstimateDanger() + ", Money after buying " + origin +
                                                                      " house(s) : " + (Player.Argent - c.PrixUnitMaison * origin) + ")",0));
                //Console.WriteLine(Player.Nom + " refused the construction of " + origin + " house(s), danger too high (Risk = " + Player.ProfitCalculator.EstimateDanger() + ", Money after buying " + origin + " house(s) : " + (Player.Argent - c.PrixUnitMaison * origin) + ")");
                return;
            }

            if (origin != nbMaisonsMax)
            {
                Player.Historique.Add(new KeyValuePair<string, float>("Tour " + Player.Partie.NbTours + " : refused the construction of " + origin + " house(s) and built " + nbMaisonsMax + " house(s) instead on " + c.Nom + ", danger too high (Risk = " +
                                                                      Player.ProfitCalculator.EstimateDanger() + ", Money after buying " + origin +
                                                                      " house(s) : " + (Player.Argent - c.PrixUnitMaison * origin) + ")",0));
            }
            if (HasAllSameColor(c))
            {
                c.NbMaisons += nbMaisonsMax;
                Player.RetirerArgent(nbMaisonsMax * c.PrixUnitMaison);
                Player.OnHouseBuy(nbMaisonsMax, c);
                Player.Historique.Add(new KeyValuePair<string, float>($"Achat de {nbMaisonsMax} maisons sur {c.Nom}", nbMaisonsMax* c.PrixUnitMaison));
                //Console.WriteLine(Player.Nom + " built " + NbMaisonsMax + " house(s) on " + c.Nom);
            }
            else
            {
                //Console.WriteLine(Player.Nom + " doesn't have all the properties of the same color (" + c.Couleur + ")");
            }

        }

        public bool HasAllSameColor(CaseMaison caseMaison)
        {
            int cpt = 0;
            foreach (CaseAchetable caseAchetable in Player.Cases)
            {
                if (!(caseAchetable is CaseMaison caseMaison1)) continue;
                if (caseMaison1.Couleur == caseMaison.Couleur)
                {
                    cpt++;
                }
            }
            return cpt == GetNumberSameColor(caseMaison);
        }

        private int GetNumberSameColor(CaseMaison caseMaison)
        {
            int cpt = 0;
            foreach (Case caseP in Player.Partie.Board.Cases)
            {
                if (!(caseP is CaseMaison caseMaison1)) continue;
                if (caseMaison1.Couleur == caseMaison.Couleur)
                {
                    cpt++;
                }
            }
            return cpt;
        }
    }
}
