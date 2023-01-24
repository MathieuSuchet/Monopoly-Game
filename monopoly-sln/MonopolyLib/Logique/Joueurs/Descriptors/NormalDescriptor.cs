using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using MonopolyLib.Logique.Cards;
using MonopolyLib.Logique.Joueurs;

namespace MonopolyLib.Logique.Joueurs.Descriptors
{
    public class NormalDescriptor : AbstractDescriptor
    {
        public NormalDescriptor(Joueur j) : base(j)
        {
        }

        public override void TurnStarted()
        {
            Console.WriteLine($"======================== {Joueur.Nom} started ===========================");
        }

        public override void ResuméJoueur()
        {
            string resume = $"------------------------ Résumé {Joueur.Nom} ----------------------------\n" +
                            $"Money : {Joueur.Argent}\n" +
                            $"Current position : {Joueur.Position} : ({Joueur.GetCaseAtCurrentPosition().Nom})\n" +
                            $"Number of properties : {Joueur.Cases.Count}\n" +
                            $"Estimated danger : {Joueur.ProfitCalculator.EstimateDanger()}\n" +
                            $"Estimated profit : {Joueur.ProfitCalculator.EstimateAverageProfit(3, true)}\n";
            Console.WriteLine(resume);

            resume += Joueur.Cards.Count == 0 ? "No card\n" : "Cards :\n";
            Console.WriteLine(Joueur.Cards.Count == 0 ? "No card" : "Cards :");
            if (Joueur.Cards.Count == 0)
            {
                foreach (var card in Joueur.Cards.Cast<Card>())
                {
                    resume += $"{card.GetType().Name} : {card.Intitule}\n";
                    Console.WriteLine($"{card.GetType().Name} : {card.Intitule}");
                }
            }

            resume += "\n";
            Console.WriteLine();

            resume += Joueur.Multiplicateurs.Any(e => Math.Abs(e.Value - 1) > 0.0001)
                ? "Multipliers : \n"
                : "No multipliers\n";
            Console.WriteLine(Joueur.Multiplicateurs.Any(e => Math.Abs(e.Value - 1) > 0.0001)
                ? "Multipliers : \n"
                : "No multipliers\n");

            if (Joueur.Multiplicateurs.Any(e => Math.Abs(e.Value - 1) > 0.0001))
            {
                foreach (var joueurMultiplicateur in Joueur.Multiplicateurs)
                {
                    if (Math.Abs(joueurMultiplicateur.Value - 1f) > 0.0001)
                    {
                        resume += $"{joueurMultiplicateur.Key.Nom} : x{joueurMultiplicateur.Value}\n";
                        Console.WriteLine($"{joueurMultiplicateur.Key.Nom} : x{joueurMultiplicateur.Value}");
                    }
                }
            }
            OnResume(Joueur, resume);
        }

        public override void DiceState(List<int> result)
        {
            string message = result[0].ToString();
            bool multiple = false;

            for(int i = 1; i<result.Count; i++)
            {
                var element = result[i];
                message += $" + {element}";
                if (result.Count(x => x == element) == result.Count)
                {
                    multiple = true;
                }
            }

            if (multiple)
            {
                message += " (double)";
            }
            
            Console.WriteLine($"{Joueur.Nom} rolled {message}");
        }
    }
}