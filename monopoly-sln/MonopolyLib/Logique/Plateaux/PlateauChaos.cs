using MonopolyLib.Logique.Cases;
using MonopolyLib.Logique.PartieComponents.Parties;

namespace MonopolyLib.Logique.Plateaux
{
    public class PlateauChaos : Plateau
    {
        public PlateauChaos(Partie p, bool extended) : base(p)
        {
            #region Création des cases

            if (!extended) return;
            
            //Second layer
            AddCase(new CaseDépart("Départ 2ème étage", 200));
            AddCase(new CaseMaison("Marron", 600, "Marron1-2", 5, 20, 100, 300, 900, 1600, 2500));
            AddCase(new CaseCaisseCommunaute("Caisse de communauté"));
            AddCase(new CaseMaison("Marron", 800, "Marron2-2", 5, 40, 200, 600, 1800, 3200, 4500));
            AddCase(new CaseImpot("Impots sur le revenu-2", 200));
            AddCase(new CaseGare("Gare1-2", 500, 2000));
            AddCase(new CaseMaison("Bleu", 1000, "Bleu1-2", 5, 60, 300, 900, 2700, 4000, 5500));
            AddCase(new CaseChance("Chance"));
            AddCase(new CaseMaison("Bleu", 1000, "Bleu2-2", 5, 60, 300, 900, 2700, 4000, 5500));
            AddCase(new CaseMaison("Bleu", 1200, "Bleu3-2", 5, 80, 400, 1000, 3000, 4500, 6000));
            AddCase(new CaseVisite("Visite simple-2"));
            AddCase(new CaseMaison("Rose", 1400, "Rose1-2", 6, 100, 500, 1500, 4500, 6250, 7500));
            AddCase(new CaseEauElec("Eau-2"));
            AddCase(new CaseMaison("Rose", 1400, "Rose2-2", 6, 100, 500, 1500, 4500, 6250, 7500));
            AddCase(new CaseMaison("Rose", 1600, "Rose3-2", 6, 120, 600, 1800, 5000, 7000, 9000));
            AddCase(new CaseGare("Gare2-2", 500, 2000));
            AddCase(new CaseMaison("Orange", 1800, "Orange1-2", 6, 140, 700, 2000, 5500, 7500, 9500));
            AddCase(new CaseCaisseCommunaute("Caisse de communauté"));
            AddCase(new CaseMaison("Orange", 1800, "Orange2-2", 6, 140, 700, 2000, 5500, 7500, 9500));
            AddCase(new CaseMaison("Orange", 2000, "Orange3-2", 6, 160, 800, 2200, 6000, 8000, 10000));
            AddCase(new CaseParc("Parc gratuit"));
            AddCase(new CaseMaison("Rouge", 2200, "Rouge1-2", 7, 180, 900, 2500, 7000, 8750, 10500));
            AddCase(new CaseChance("Chance"));                   
            AddCase(new CaseMaison("0Rouge", 2200, "Rouge2-2", 7, 180, 900, 2500, 7000, 8750, 10500));
            AddCase(new CaseMaison("Rouge", 2400, "Rouge3-2", 7, 200, 1000, 3000, 7500, 9250, 11000));
            AddCase(new CaseGare("Gare3-2", 500, 2000));
            AddCase(new CaseMaison("Jaune", 2600, "Jaune1-2", 7, 220, 1100, 3300, 8000, 9750, 11500));
            AddCase(new CaseMaison("Jaune", 2600, "Jaune2-2", 7, 220, 1100, 3300, 8000, 9750, 11500));
            AddCase(new CaseEauElec("Electricité"));
            AddCase(new CaseMaison("Jaune", 2800, "Jaune3-2", 7, 240, 1200, 3600, 8500, 10250, 12000));
            AddCase(new CasePrison("Prison"));
            AddCase(new CaseMaison("Vert", 3000, "Vert1-2", 80, 260, 1300, 3900, 9000, 11000, 12750));
            AddCase(new CaseMaison("Vert", 3000, "Vert2-2", 80, 260, 1300, 3900, 9000, 11000, 12750));
            AddCase(new CaseCaisseCommunaute("Caisse de communauté"));
            AddCase(new CaseMaison("Vert", 3200, "Vert3-2", 8, 280, 1500, 4500, 10000, 12000, 14000));
            AddCase(new CaseGare("Gare4-2", 500, 2000));
            AddCase(new CaseChance("Chance"));
            AddCase(new CaseMaison("BleuFoncé", 3500, "BleuFoncé1-2", 8, 350, 1750, 5000, 11000, 13000, 15000));
            AddCase(new CaseImpot("Taxe-2", 1000));
            AddCase(new CaseMaison("BleuFoncé", 5000, "BleuFoncé2-2", 8, 500, 2000, 6000, 14000, 17000, 20000));

            #endregion
        }

        public PlateauChaos(int numberOfCardSets, Partie p) : base(numberOfCardSets, p)
        {
        }
    }
}