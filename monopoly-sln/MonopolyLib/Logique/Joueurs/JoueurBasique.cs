using System;
using System.Collections.Generic;
using System.Linq;
using MonopolyLib.Logique.Cards.CardSets;
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
    class JoueurBasique : Joueur
    {
        public override void Jouer()
        {
            Waiter();
        }

        private void Waiter()
        {
            List<int> values = LancerDés();
            if (values.Distinct().Count() != 1)
            {
                Partie.FinishedTurn = true;
            }
            int pos = Position + values.Sum();
            Partie.Roll = values.Sum();
            Position = pos;
            GestionCase(GetCaseAtCurrentPosition());
        }

        protected override bool GetEnPrison()
        {
            return EnPrisonAttr;
        }

        protected override void SetEnPrison(bool value)
        {
            if (value)
            {
                NbToursPrisons = 3;
                Console.WriteLine("En prison " + Nom + "!");
                Position = Partie.Board.GetPosVisite();
            }
            else
            {
                if (!RetirerArgent(50))
                {
                    GestionnaireVente.VendreParChoix(50);
                }
                Historique.Add(new KeyValuePair<string, float>("Tour " + Partie.NbTours + " : Sortie de prison", -50));
                NbToursPrisons = 0;
            }
            EnPrisonAttr = value;
        }

        public JoueurBasique(string nom, bool silent) : base(nom, silent)
        {
        }

        public JoueurBasique(float argent, string nom, bool silent) : base(argent, nom, silent) { }

        public override void AcheterUneCase(CaseAchetable c)
        {
            AcheteurCase.AcheterUneCase(c);
        }

        public override void VendreUneCase(CaseAchetable c)
        {
            VendeurCase.VendreUneCase(c);
        }

        public override void AcheterMaison(CaseMaison c)
        {
            AcheteurMaison.AcheterMaison(c);
        }


        public override List<int> LancerDés()
        {
            var result = LanceurDés.LancerDés();
            Descriptor.DiceState(result);
            return result;
        }

        public void GestionCase(Case c)
        {
            #region La case est achetable
            if (c is CaseAchetable caseAchetable)
            {
                #region Il y a un propiétaire qui n'est pas le joueur
                if (caseAchetable.Proprio != null && caseAchetable.Proprio != this)
                {
                    GestionnaireVente.VendreParChoix(caseAchetable.PrixFinal);

                    if (!RetirerArgent(caseAchetable.PrixFinal))
                    {
                        FaitFaillite();
                        return;
                    }
                    Historique.Add(new KeyValuePair<string, float>("Tour " + Partie.NbTours + " : Paiement sur la case " + c.Nom + " appartenant à " + caseAchetable.Proprio.Nom, -caseAchetable.PrixFinal));
                    caseAchetable.Profit += caseAchetable.PrixFinal;
                    caseAchetable.Proprio.AjouterArgent(caseAchetable.PrixFinal);
                }
                #endregion
                #region Le joueur est proprio ou la case n'est pas achetée
                else
                {
                    #region La case est achetée
                    if (caseAchetable.Achetée)
                    {
                        #region La case est une CaseMaison
                        if (caseAchetable is CaseMaison caseMaison)
                        {
                            //Debug.Log("Fell on owned place");
                            #region La case appartient au joueur
                            if (caseMaison.Proprio == this)
                            {
                                AcheterMaison(caseMaison);
                            }
                            #endregion
                        }
                        #endregion
                        return;
                    }
                    #endregion
                    #region Le joueur peut acheter la case et il n'y a pas de proprio

                    if (!(caseAchetable.PrixAchat <= Argent) || caseAchetable.Proprio != null) return;
                    List<string> choix = new List<string>
                    {
                        "Acheter la case " + c.Nom + " (Prix : " + caseAchetable.PrixAchat + ") (Votre argent : " + Argent + ")",
                        "Ne rien faire"
                    };

                    int choice = FaireChoix("La case " + c.Nom + " n'est pas achetée", choix, new List<ConsoleKey>
                    {
                        ConsoleKey.NumPad0,
                        ConsoleKey.NumPad1
                    });


                    if (choice == 0)
                    {
                        AcheterUneCase(caseAchetable);
                    }
                    #endregion
                }
                #endregion
            }
            #endregion

            #region La case est visitable
            else if (c is CaseVisitable caseVisitable)
            {
                #region Case prison
                if (caseVisitable is CasePrison)
                {
                    EnPrison = true;
                }
                #endregion
                #region Case parc gratuit
                else if (caseVisitable is CaseParc)
                {
                    Console.WriteLine("Gestion du parc gratuit à implémenter");
                }
                #endregion
                #region Case interactive (Chance/CaisseCom)
                else if (caseVisitable is CaseInteract caseInteract)
                {
                    switch (caseInteract)
                    {
                        case CaseChance _:
                            TirerUneCarte(Partie.Board.Cards.Find(x => x is CardSetChance));
                            break;
                        case CaseCaisseCommunaute _:
                            TirerUneCarte(Partie.Board.Cards.Find(x => x is CardSetCaisseCommunauté));
                            break;
                    }
                }
                #endregion
                #region Case impot
                else if (caseVisitable is CaseImpot caseImpot)
                {
                    if (!RetirerArgent(caseImpot.PrixAPayer))
                    {
                        GestionnaireVente.VendreParChoix(caseImpot.PrixAPayer);
                    }
                }
                #endregion

                #region Case départ

                else if (caseVisitable is CaseDépart caseDépart)
                {
                    AjouterArgent(caseDépart.RécompensePassage);
                }

                #endregion
            }
            #endregion
        }

        public override void SetPartie(Partie p)
        {
            base.SetPartie(p);
            AcheteurCase = new AcheteurCaseBasique(this);
            VendeurCase = new VendeurCaseBasique(this);

            AjouteurArgent = new AjouteurArgentBasique(this);
            RetireurArgent = new RetireurArgentBasique(this);

            AcheteurMaison = new AcheteurMaisonBasique(this);
            VendeurMaison = new VendeurMaisonBasique(this);

            LanceurDés = new LanceurDésBasique(this);
            GestionnaireFaillite = new GestionnaireFailliteBasique(this);

            GestionnaireVente = new GestionnaireVenteBasique(this, VendeurCase);
            Position = Partie.Board.GetPosDépart();

            for (int i = 0; i < Partie.Joueurs.Count; i++)
            {
                if (Partie.Joueurs[i] == this)
                {
                    continue;
                }
                Multiplicateurs.Add(Partie.Joueurs[i], 1);
            }

        }
        public override int FaireChoix(string intitule, List<string> choix, List<ConsoleKey> keys)
        {
            Console.WriteLine($"-----{intitule}-----\n");

            for (int i = 0; i < choix.Count; i++)
            {
                Console.WriteLine($"{keys[i]} : {choix[i]}");
            }

            ConsoleKeyInfo key = Console.ReadKey();
            if (keys.Contains(key.Key))
            {
                for (int j = 0; j < keys.Count; j++)
                {
                    if (keys[j] == key.Key)
                    {
                        return j;
                    }
                }
            }
            return 0;


        }
    }
}
