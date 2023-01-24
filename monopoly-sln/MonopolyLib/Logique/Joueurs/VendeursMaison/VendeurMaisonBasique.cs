using System;
using System.Collections.Generic;
using MonopolyLib.Logique.Cases;
using MonopolyLib.Logique.Joueurs;

namespace MonopolyLib.Logique.Joueurs.VendeursMaison
{
    public class VendeurMaisonBasique : VendeurMaison
    {
        public VendeurMaisonBasique(Joueur? j) : base(j) { }

        public override void VendreMaison(CaseMaison c)
        {
            if (c.NbMaisons == 0)
            {
                Console.WriteLine("Il n'y a pas de maisons sur la case " + c.Nom);
                return;
            }
            List<string> choix = new List<string>();
            switch (c.NbMaisons)
            {
                case 1:
                    choix.Add("Vendre une maison (Prix de vente : " + c.PrixUnitMaison + ")");

                    break;

                case 2:
                    choix.Add("Vendre une maison (Prix de vente : " + c.PrixUnitMaison + ")");
                    choix.Add("Vendre deux maisons (Prix de vente : " + c.PrixUnitMaison * 2 + ")");

                    break;
                case 3:
                    choix.Add("Vendre une maison (Prix de vente : " + c
                    .PrixUnitMaison + ")");
                    choix.Add("Vendre deux maisons (Prix de vente : " + c
                    .PrixUnitMaison * 2 + ")");
                    choix.Add("Vendre trois maisons (Prix de vente : " + c
                    .PrixUnitMaison * 3 + ")");

                    break;
                case 4:
                    choix.Add("Vendre une maison (Prix de vente : " + c
                    .PrixUnitMaison + ")");
                    choix.Add("Vendre deux maisons (Prix de vente : " + c
                    .PrixUnitMaison * 2 + ")");
                    choix.Add("Vendre trois maisons (Prix de vente : " + c
                    .PrixUnitMaison * 3 + ")");
                    choix.Add("Vendre quatre maisons (Prix de vente : " + c
                    .PrixUnitMaison * 4 + ")");

                    break;
                case 5:
                    choix.Add("Vendre une maison (Prix de vente : " + c
                    .PrixUnitMaison + ")");
                    choix.Add("Vendre deux maisons (Prix de vente : " + c
                    .PrixUnitMaison * 2 + ")");
                    choix.Add("Vendre trois maisons (Prix de vente : " + c
                    .PrixUnitMaison * 3 + ")");
                    choix.Add("Vendre quatre maisons (Prix de vente : " + c
                    .PrixUnitMaison * 4 + ")");
                    choix.Add("Vendre l'hotel (Prix de vente : " + c
                    .PrixUnitMaison * 5 + ")");

                    break;

            }
            int choice = Player.FaireChoix("Combien de maisons voulez-vous vendre ?", choix, new List<ConsoleKey>
            {
                ConsoleKey.NumPad0,
                ConsoleKey.NumPad1,
                ConsoleKey.NumPad2,
                ConsoleKey.NumPad3,
                ConsoleKey.NumPad4
            });
            c.NbMaisons -= choice;
            Player.AjouterArgent(choice * c.PrixUnitMaison);
            Player.OnHouseSell(choice, c);
        }
    }
}
