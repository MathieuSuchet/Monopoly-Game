using System;
using System.Threading;
using MonopolyLib.Logique.Joueurs;

namespace MonopolyLib.Logique.PartieComponents.PartieDescriptors
{
    public class PartieDescriptor : AbstractPartieDescriptor
    {
        public override void ResuméPartie()
        {
            string resume = $" ~~~~~~~ RESUME DE LA PARTIE (Tour {Partie.NbTours})~~~~~~~ ";
            Console.WriteLine(resume);
            OnResumé(resume);
            foreach (Joueur? joueur in Partie.Joueurs)
            {
                joueur.Descriptor.ResuméJoueur();
            }
            
            //Thread.Sleep(500);
            //Console.Clear();
        }

        public override void StartPartie()
        {
            string start = "--------------------------Début du jeu !--------------------------\n";
            OnStart(start);
            foreach (Joueur? j in Partie.Joueurs)
            {
                j.Descriptor.ResuméJoueur();
            }
            Console.WriteLine(start);
        }

        public override void NextTurnPartie()
        {
            string next = "\n================= Tour " + Partie.NbTours + " ==================\n";
            Console.WriteLine(next);
            OnNextTurn(next);
        }

        public override void PartieEnded()
        {
            string end = "---------------------------Partie terminée !-------------------------\n";
            Console.WriteLine(end);
            OnEnd(end);
        }
    }
}