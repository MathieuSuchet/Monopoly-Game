using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MonopolyLib.Logique.Cards.CardSets;
using MonopolyLib.Logique.Cards.Keepables;
using MonopolyLib.Logique.Cases;
using MonopolyLib.Logique.Joueurs;
using MonopolyLib.Logique.Joueurs.AcheteursCase;
using MonopolyLib.Logique.Joueurs.AcheteursMaison;
using MonopolyLib.Logique.Joueurs.AjouteursArgent;
using MonopolyLib.Logique.Joueurs.GestionnairesFaillite;
using MonopolyLib.Logique.Joueurs.GestionnairesVente;
using MonopolyLib.Logique.Joueurs.LanceursDés;
using MonopolyLib.Logique.Joueurs.RetireursArgent;
using MonopolyLib.Logique.Joueurs.VendeursCase;
using MonopolyLib.Logique.Joueurs.VendeursMaison;
using MonopolyLib.Logique.PartieComponents.Parties;

namespace MonopolyLib.Logique.Joueurs
{
    public class JoueurIa : Joueur
    {
        private bool _prisonDouble;
        private bool _prisonCard;
        private bool _prisonMoney;

        public JoueurIa(string nom, bool silent) : base(nom, silent)
        {
            var random = new Random();
            Agressivite = random.Next(30, 100);
        }

        public JoueurIa(string nom, bool silent, float argent) : base(argent, nom, silent)
        {
            var random = new Random();
            Agressivite = random.Next(30, 100);
        }

        public JoueurIa(string nom, int agressivite, bool silent) : base(nom, silent)
        {
            Agressivite = agressivite;
        }
        
        public JoueurIa(float argent, string nom, bool silent, int nbProperties) : base(argent, nom, silent, nbProperties){}

        public override void AcheterUneCase(CaseAchetable c)
        {
            AcheteurCase.AcheterUneCase(c);
        }

        public override int FaireChoix(string intitule, List<string> choix, List<ConsoleKey> keys)
        {
            throw new NotImplementedException();
        }

        public override void FaitFaillite()
        {
            base.FaitFaillite();
        }

        public override void Jouer()
        {
            base.Jouer();
            OldPos = Position;
            List<int> values = LancerDés();
            if (values.Distinct().Count() != 1)
            {
                Partie.FinishedTurn = true;
            }

            int pos = Position + values.Sum();
            if (pos >= Partie.Board.Cases.Count)
            {
                pos %= Partie.Board.Cases.Count;
            }
            GoTo(Partie.Board.Cases[pos]);
            Partie.Roll = values.Sum();

            //Console.WriteLine("Il fait un " + Partie.Roll);
            //Position += values.Sum();
            ProfitCalculator.EstimateAverageProfit(3, false);
            GestionCaseIa(GetCaseAtCurrentPosition());

            //Console.WriteLine();

            //Console.WriteLine("Valeurs connues par l'IA : ");

            //Console.WriteLine("Nom : " + Nom);
            //Console.WriteLine("Argent : " + Argent);
            //Console.WriteLine("Cases : " + Cases);
            //Console.WriteLine("Position : " + Position);
            //Console.WriteLine("La partie : " + Partie);
            //Console.WriteLine("Danger potentiel devant : " + ProfitCalculator.EstimateDanger());
            //Console.WriteLine("Profit moyen : " + ProfitCalculator.EstimateAverageProfit());
            //Console.WriteLine("Profit potentiel : " + ProfitCalculator.EstimatePotentialProfit());
            //Console.WriteLine(Nom + " : Profit potentiel moyen sur 3 tours : " + ProfitCalculator.EstimateAverageProfit(3));
            //foreach (CaseAchetable c in Cases)
            //{
            //    Console.WriteLine($"\t{c.Nom} : {c.Position}");
            //}
            //foreach(Joueur j in Partie.joueurs)
            //{
            //    if(j == this)
            //    {
            //        continue;
            //    }
            //    Console.WriteLine($"{j.Nom} : {j.Position}");
            //}
            //Console.WriteLine();

        }

        public void GestionCaseIa(Case c)
        {
            float argent = Argent;
            if (EnPrison)
            {
                #region Si le joueur peut être libéré de prison via une carte

                for (int i = 0; i < Cards.Count; i++)
                {
                    if (Cards[i].GetType() != typeof(CardOutOfPrison)) continue;
                    if (!(ProfitCalculator.EstimateDanger() < Argent)) continue;
                    Cards[i].UseCard(this);
                    _prisonCard = true;
                }
                
                #endregion

                #region Si il peut payer sans danger

                if (ProfitCalculator.EstimateDanger() < Argent - 50)
                {
                    if (RetirerArgent(50))
                    {
                        OnTransaction(50, this, null, "Sortie prison");
                        _prisonMoney = true;
                        return;
                    }

                    EnPrison = false;
                }

                #endregion

                if (!_prisonCard && !_prisonDouble && !_prisonMoney) return;
            }
            

            switch (c)
            {
                #region La case est achetable
                case CaseAchetable caseAchetable:
                {
                    #region La case est une case où des maisons peuvent etre construites
                    if (caseAchetable is CaseMaison caseMaison)
                    {
                        #region La case appartient au joueur
                        if (caseMaison.Proprio == this)
                        {
                            AcheterMaison(caseMaison);
                        }
                        #endregion
                    }
                    #endregion
                    #region La case appartient à quelqu'un, mais pas au joueur
                    if (caseAchetable.Proprio != null && caseAchetable.Proprio != this)
                    {
                        float multiplicateur = Multiplicateurs[caseAchetable.Proprio];
                        float prix = caseAchetable.PrixFinal * multiplicateur;

                        if (caseAchetable is CaseMaison)
                        {
                            int nbCases = caseAchetable.Proprio.Cases.Count(x => x.Proprio == caseAchetable.Proprio);
                            int nbTotal = Partie.Board.Cases.Where(x => x is CaseMaison).Cast<CaseMaison>()
                                .Count(x => x.Couleur == ((CaseMaison)caseAchetable).Couleur);

                            if (nbCases == nbTotal) prix *= 2;
                        }
                        
                        if (!RetirerArgent(prix))
                        {
                            GestionnaireVente.VendreJusquaRemboursement(prix);
                            if (!Faillite)
                            {
                                RetirerArgent(prix);
                                Historique.Add(new KeyValuePair<string, float>("Tour " + Partie.NbTours + " : Paiement sur la case " + c.Nom + " appartenant à " + caseAchetable.Proprio.Nom + " (Multiplicateur : " + multiplicateur + ")", -prix));
                            }
                            else
                            {
                                return;
                            }
                        }
                        OnTransaction(prix, this, caseAchetable.Proprio, $"Paiement sur {caseAchetable.Nom}");
                        caseAchetable.Profit += prix;
                        caseAchetable.Proprio.AjouterArgent(prix);

                        Multiplicateurs[caseAchetable.Proprio] = 1;

                        if (Argent > 3 * (caseAchetable.PrixFinal + caseAchetable.PrixAchat))
                        {
                            if (c is CaseMaison maison)
                            {
                                if(!(maison.Proprio.AcheteurMaison as AcheteurMaisonIa).HasAllSameColor(maison))
                                    AcheteurCase.RacheterUneCase(maison);
                            }

                        }
                    }
                    #endregion

                    #region La case n'appartient à personne
                    else
                    {
                        AcheterUneCase(caseAchetable);
                    }
                    #endregion
                    break;
                }
                #endregion

                #region La case est visitable
                case CaseVisitable caseVisitable:
                    switch (caseVisitable)
                    {
                        #region Case prison
                        case CasePrison _:
                            EnPrison = true;
                            break;
                        #endregion

                        #region Case parc gratuit
                        case CaseParc _:
                            //Console.WriteLine("Gestion du parc gratuit à implémenter");
                            break;
                        #endregion

                        #region Case chance/Caisse communauté

                        case CaseInteract caseInteract:
                            TirerUneCarte(caseInteract is CaseChance
                                ? Partie.Board.Cards.Find(x => x is CardSetChance)
                                : Partie.Board.Cards.Find(x => x is CardSetCaisseCommunauté));
                            break;

                        #endregion

                        #region Case impot
                        case CaseImpot caseImpot:
                        {
                            #region Le joueur n'a pas l'argent
                            if (!RetirerArgent(caseImpot.PrixAPayer))
                            {
                                GestionnaireVente.VendreParChoix(caseImpot.PrixAPayer);
                                if (!Faillite)
                                {
                                    RetirerArgent(caseImpot.PrixAPayer);
                                }
                            }

                            OnTransaction(caseImpot.PrixAPayer, this, null, "Impots/Taxes");
                            #endregion
                            
                            break;
                        }
                            

                        #endregion

                        #region Case départ
                        case CaseDépart caseDépart:
                            AjouterArgent(caseDépart.RécompensePassage);
                            OnTransaction(caseDépart.RécompensePassage, null, this, "Arret sur la case départ");
                            break;
                        #endregion
                    }
                    break;
                #endregion
            }

            // if(Nom == "Test 1")
            //     Agent.PrintEverything();
        }

        public override List<int> LancerDés()
        {
            var result = LanceurDés.LancerDés();
            Descriptor.DiceState(result);
            return result;
        }

        protected override void SetEnPrison(bool value)
        {
            if (value)
            {
                NbToursPrisons = 3;
                Position = Partie.Board.GetPosVisite();
            }
            else
            {
                if (!_prisonDouble && !_prisonCard && !_prisonMoney)
                {
                    while (!RetirerArgent(50))
                    {
                        if (Cases.Count == 0)
                        {
                            return;
                        }
                        GestionnaireVente.VendreJusquaRemboursement(50);
                        OnTransaction(50, this, null, "Sortie forcée de prison");
                    } 
                    Historique.Add(new KeyValuePair<string, float>("Tour " + Partie.NbTours + " : Sortie de prison", 50));

                }
                else
                {
                    _prisonCard = false;
                    _prisonDouble = false;
                    _prisonDouble = false;
                    Historique.Add(new KeyValuePair<string, float>("Tour " + Partie.NbTours + " : Sortie de prison", 0));
                }
                
                NbToursPrisons = 0;
            }
            EnPrisonAttr = value;
        }

        public override void SetPartie(Partie p)
        {
            base.SetPartie(p);
            AcheteurCase = new AcheteurCaseIA(this);
            VendeurCase = new VendeurCaseIa(this);

            AjouteurArgent = new AjouteurArgentIa(this);
            RetireurArgent = new RetireurArgentIa(this);

            AcheteurMaison = new AcheteurMaisonIa(this);
            VendeurMaison = new VendeurMaisonIa(this);

            LanceurDés = new LanceurDésIa(this);
            GestionnaireFaillite = new GestionnaireFailliteIa(this);

            GestionnaireVente = new GestionnaireVenteIa(this, VendeurCase);
            ProfitCalculator = new ProfitCalculator(this);

            for (int i = 0; i < Partie.Joueurs.Count; i++)
            {
                if (Partie.Joueurs[i] == this)
                {
                    continue;
                }
                Multiplicateurs.Add(Partie.Joueurs[i], 1);
            }
        }

        public override void VendreUneCase(CaseAchetable c)
        {
            VendeurCase.VendreUneCase(c);
        }

        public void VendreMaison(CaseMaison c, int nbMaisons)
        {
            VendeurMaison.VendreMaison(c, nbMaisons);
        }
    }
}
