using System;
using System.Collections.Generic;
using System.Linq;
using MonopolyLib.Logique.Cards.Keepables;
using MonopolyLib.Logique.Cases;
using MonopolyLib.Logique.Joueurs;

namespace MonopolyLib.Logique.Joueurs
{
    public class ProfitCalculator : PlayerComponentsMother
    {
        public ProfitCalculator(Joueur j) : base(j) { }

        public float EstimateAverageProfit(int nbTurn, bool informative)
        {
            float profit = 0;
            Joueur[] joueurs = new Joueur[4];
            Player.Partie.Joueurs.CopyTo(joueurs);
            List<Joueur> list = joueurs.ToList();
            list.RemoveAll(x => x is null);
            list.Remove(Player);
            //Console.WriteLine("Appel de méthode avec message long");
            for (int i = 0; i < nbTurn; i++)
            {
                for (var index = 0; index < list.Count; index++)
                {
                    Joueur j = list[index];
                    if (!PlayerIsInRiskZone(j)) continue;
                    //Console.WriteLine("Ca passe");
                    List<CaseAchetable> dangers = EstimateCasesInRiskZoneFor(j);
                    profit += dangers.Sum(c => c.PrixFinal * 1 / 11);
                }
            }

            if (informative)
            {
                return profit;
            }

            if (profit < 100f / Player.Agressivite) return profit;
            {
                var cards = Player.Cards.ToArray();
                foreach (IKeepable card in cards)
                {
                    if (card is CardXTimes x)
                    {
                        x.UseCard(Player);
                    }
                }
            }


            return profit;
        }

        // public float EstimatePotentialProfit()
        // {
        //     float profit = 0;
        //     Joueur[] joueurs = { null, null, null, null };
        //     Player.Partie.joueurs.CopyTo(joueurs);
        //     List<Joueur> list = joueurs.ToList();
        //     list.RemoveAll(x => x is null);
        //     list.Remove(Player);
        //     foreach (Joueur j in list)
        //     {
        //         if (PlayerIsInRiskZone(j))
        //         {
        //             List<CaseAchetable> dangers = EstimateCasesInRiskZoneFor(j);
        //             profit += dangers.Sum(c => c.PrixFinal);
        //         }
        //     }
        //     return profit;
        // }

        public float EstimateProfitForXTurn(int depth)
        {
            float profit = 0;
            List<Joueur> list = new List<Joueur>();
            list.AddRange(Player.Partie.Joueurs);

            list.RemoveAll(x => x is null);
            list.Remove(Player);

            foreach (Joueur j in list)
            {
                if (j == Player)
                {
                    profit += 0;
                    continue;
                }

                var tmp = EstimateAverageProfit(3, false);
                

                profit += tmp;
            }
            return profit;
        }

        private float EstimateProfitFor(Joueur j, int depth)
        {
            int originalPos = j.Position;
            float profit = 0;
            List<float> list = new List<float>();
            if (depth == 0)
            {
                return profit;
            }

            int pos;
            for (int i = 1; i <= depth; i++)
            {
                for (int k = 2; k <= 12; k++)
                {
                    j.Position = originalPos;
                    pos = j.Position + k;
                    if (pos >= j.Partie.Board.Cases.Count)
                    {
                        pos -= j.Partie.Board.Cases.Count;
                    }
                    if (j.Partie.Board.Cases[pos] is CaseAchetable ca && ca.Proprio == Player)
                    {
                        Console.WriteLine($"({Player.Nom} => {j.Nom}, Tour n°{i}) : Case {ca.Nom}, profit possible : {ca.PrixFinal}");
                        list.Add(((CaseAchetable)Player.Partie.Board.Cases[pos]).PrixFinal);
                    }
                }

                originalPos += 12;
                originalPos %= j.Partie.Board.Cards.Count;
                profit += list.Count == 0 ? 0 : list.Average();
                list.Clear();
            }

            return profit;
        }

        private bool PlayerIsInRiskZone(Joueur j)
        {
            foreach (CaseAchetable c in Player.Cases)
            {
                if (j.Position <= c.Position - 2 && j.Position >= c.Position - 12)
                {
                    return true;
                }
            }
            return false;
        }

        private CaseAchetable EstimateClosestCaseTo(Joueur j)
        {
            int min = Player.Partie.Board.Cases.Count;
            CaseAchetable currentClosest = null;
            foreach (CaseAchetable c in Player.Cases)
            {
                int pos = j.Position;
                //On repart du début du plateau
                if (j.Position - c.Position < 0)
                {
                    pos += Player.Partie.Board.Cases.Count;
                }

                if (pos - c.Position < min)
                {
                    min = pos - c.Position;
                    currentClosest = c;
                }
            }
            return currentClosest;
        }

        private List<CaseAchetable> EstimateCasesInRiskZoneFor(Joueur j)
        {
            return Player.Cases.Where(c => j.Position <= c.Position - 2 && j.Position >= c.Position - 12).ToList();
        }

        public float EstimateDanger()
        {
            float originalArgent = Player.Argent;
            List<float> dangers = new List<float>();
            for(int i = 2; i<=12; i++)
            {
                int pos = Player.Position + i;
                if (pos >= Player.Partie.Board.Cases.Count)
                {
                    pos -= Player.Partie.Board.Cases.Count;
                }
                if(Player.Partie.Board.Cases[pos] is CaseDépart cd)
                {
                    originalArgent += cd.RécompensePassage;
                    //Player.AjouterArgent(cd.RécompensePassage);
                }
                if(Player.Partie.Board.Cases[pos] is CaseAchetable ca && ca.Proprio != null && ca.Proprio != Player)
                {          
                    dangers.Add(ca.PrixFinal);
                }
            }
            if (dangers.Count == 0)
                return 0;
            return dangers.Max();
        }
    }
}
