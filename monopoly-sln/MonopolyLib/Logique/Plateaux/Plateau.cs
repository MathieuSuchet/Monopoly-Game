using System.Collections.Generic;
using MonopolyLib.Logique.Cards.CardSets;
using MonopolyLib.Logique.Cases;
using MonopolyLib.Logique.PartieComponents.Parties;

namespace MonopolyLib.Logique.Plateaux
{
    public class Plateau
    {
        public readonly List<Case> Cases = new List<Case>();

        public readonly List<CardSet> Cards = new List<CardSet>();

        protected Plateau(Partie p)
        {
            #region Création des cases
            
            AddCase(new CaseDépart("Départ", 200));
            AddCase(new CaseMaison("Marron", 60, "Marron1", 1, 2, 10, 30, 90, 160, 250));
            AddCase(new CaseCaisseCommunaute("Caisse de communauté"));
            AddCase(new CaseMaison("Marron", 80, "Marron2", 1, 4, 20, 60, 180, 320, 450));
            AddCase(new CaseImpot("Impots sur le revenu", 200));
            AddCase(new CaseGare("Gare1", 50, 200));
            AddCase(new CaseMaison("Bleu", 100, "Bleu1", 1, 6, 30, 90, 270, 400, 550));
            AddCase(new CaseChance("Chance"));
            AddCase(new CaseMaison("Bleu", 100, "Bleu2", 1, 6, 30, 90, 270, 400, 550));
            AddCase(new CaseMaison("Bleu", 120, "Bleu3", 1, 8, 40, 100, 300, 450, 600));
            AddCase(new CaseVisite("Visite simple"));
            AddCase(new CaseMaison("Rose", 140, "Rose1", 2, 10, 50, 150, 450, 625, 750));
            AddCase(new CaseEauElec("Eau"));
            AddCase(new CaseMaison("Rose", 140, "Rose2", 2, 10, 50, 150, 450, 625, 750));
            AddCase(new CaseMaison("Rose", 160, "Rose3", 2, 12, 60, 180, 500, 700, 900));
            AddCase(new CaseGare("Gare2", 50, 200));
            AddCase(new CaseMaison("Orange", 180, "Orange1", 2, 14, 70, 200, 550, 750, 950));
            AddCase(new CaseCaisseCommunaute("Caisse de communauté"));
            AddCase(new CaseMaison("Orange", 180, "Orange2", 2, 14, 70, 200, 550, 750, 950));
            AddCase(new CaseMaison("Orange", 200, "Orange3", 2, 16, 80, 220, 600, 800, 1000));
            AddCase(new CaseParc("Parc gratuit"));
            AddCase(new CaseMaison("Rouge", 220, "Rouge1", 3, 18, 90, 250, 700, 875, 1050));
            AddCase(new CaseChance("Chance"));
            AddCase(new CaseMaison("Rouge", 220, "Rouge2", 3, 18, 90, 250, 700, 875, 1050));
            AddCase(new CaseMaison("Rouge", 240, "Rouge3", 3, 20, 100, 300, 750, 925, 1100));
            AddCase(new CaseGare("Gare3", 50, 200));
            AddCase(new CaseMaison("Jaune", 260, "Jaune1", 3, 22, 110, 330, 800, 975, 1150));
            AddCase(new CaseMaison("Jaune", 260, "Jaune2", 3, 22, 110, 330, 800, 975, 1150));
            AddCase(new CaseEauElec("Electricité"));
            AddCase(new CaseMaison("Jaune", 280, "Jaune3", 3, 24, 120, 360, 850, 1025, 1200));
            AddCase(new CasePrison("Prison"));
            AddCase(new CaseMaison("Vert", 300, "Vert1", 4, 26, 130, 390, 900, 1100, 1275));
            AddCase(new CaseMaison("Vert", 300, "Vert2", 4, 26, 130, 390, 900, 1100, 1275));
            AddCase(new CaseCaisseCommunaute("Caisse de communauté"));
            AddCase(new CaseMaison("Vert", 320, "Vert3", 4, 28, 150, 450, 1000, 1200, 1400));
            AddCase(new CaseGare("Gare4", 50, 200));
            AddCase(new CaseChance("Chance"));
            AddCase(new CaseMaison("BleuFoncé", 350, "BleuFoncé1", 4, 35, 175, 500, 1100, 1300, 1500));
            AddCase(new CaseImpot("Taxe", 100));
            AddCase(new CaseMaison("BleuFoncé", 500, "BleuFoncé2", 4, 50, 200, 600, 1400, 1700, 2000));

            #endregion

            #region Génération des tas de cartes

            CardSet set = new CardSetCaisseCommunauté();
            set.Generate(16, this,p.GetType());
            Cards.Add(set);

            CardSet set2 = new CardSetChance();
            set2.Generate(16, this,p.GetType());
            Cards.Add(set2);
            
            p.OnPLoaded(this);

            #endregion

        }

        protected Plateau(int numberOfCardSets, Partie p) : this(p)
        {
            for (int i = 0; i < numberOfCardSets; i++)
            {
                CardSet set = new CardSet();
                set.Generate(52, this,p.GetType());
                Cards.Add(set);
            }
            
            p.OnPLoaded(this);
        }

        protected void AddCase(Case c)
        {
            Cases.Add(c);
            c.Position = Cases.Count - 1;
        }

        public int GetPosDépart()
        {
            int cpt = 0;
            foreach (Case c in Cases)
            {
                if (c is CaseDépart)
                {
                    return cpt;
                }
                cpt++;
            }
            return -1;
        }

        public int GetPosVisite()
        {
            int cpt = 0;
            foreach (Case c in Cases)
            {
                if (c is CaseVisite)
                {
                    return cpt;
                }
                cpt++;
            }
            return -1;
        }
    }


}
