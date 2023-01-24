using System;
using System.Collections.Generic;
using MonopolyLib.Logique.Events;
using MonopolyLib.Logique.Joueurs;
using MonopolyLib.Logique.PartieComponents.PartieDescriptors;
using MonopolyLib.Logique.Plateaux;

namespace MonopolyLib.Logique.PartieComponents.Parties
{
    public class Partie
    {
        /// <summary>
        /// Player list
        /// </summary>
        public readonly List<Joueur?> Joueurs;

        /// <summary>
        /// List of players that have lost
        /// </summary>
        public readonly List<Joueur?> Faillites = new List<Joueur?>();

        private Joueur? _joueurActuelAttr;

        /// <summary>
        /// Number of turns up until now
        /// </summary>
        public int NbTours;

        /// <summary>
        /// Current roll of the game
        /// </summary>
        public static int Roll = 0;

        private int _position;
        
        public Joueur? JoueurActuel
        {
            get => _joueurActuelAttr;
            set
            {
                if (_finished) return;
                _joueurActuelAttr = value;
                PlayerChanged();
            }
        }


        private PartieCommunicator Communicator { get; }

        /// <summary>
        /// Event that occurs when the board is loaded
        /// </summary>
        public event EventHandler<PlateauArgs> OnPlateauLoaded;

        public event EventHandler<EventArgs> OnPlayerChanged; 

        private bool _finished;
        
        /// <summary>
        /// True if the player finished his turn
        /// </summary>
        public bool FinishedTurn;

        /// <summary>
        /// Board of the game
        /// </summary>
        public Plateau Board { get; protected set; }

        protected Partie(List<Joueur> joueurs, bool silent)
        {
            Joueurs = joueurs;
            Communicator = silent
                ? new PartieCommunicator(this, new EmptyPartieDescriptor())
                : new PartieCommunicator(this, new PartieDescriptor());

            Random rng = new Random();
            int n = Joueurs.Count;
            while (n > 1)
            {
                n--;
                
                int k = rng.Next(n + 1);
                Joueurs[k].Position = n;
                (Joueurs[k], Joueurs[n]) = (Joueurs[n], Joueurs[k]);
            }
            
            foreach (Joueur? jo in Joueurs)
            {
                jo.SetPartie(this);
            }

            JoueurActuel = Joueurs[0];
        }


        /// <summary>
        /// Starts the game and guide through the whole game
        /// </summary>
        public virtual void Gestionnaire()
        {
            Communicator.StartPartie();

            #region Partie non finie

            while (!_finished)
            {
                NextPlayer();
                FinishedTurn = false;

                #region Tour non fini

                while (!FinishedTurn)
                {
                    Jouer();
                }
                #endregion
                _position++;
               
            }

            #endregion
        }

        protected virtual void NextPlayer()
        {
            if (_position >= Joueurs.Count)
            {
                NbTours++;
                Communicator.NextTurnPartie();

                foreach (var joueur in Joueurs)
                {
                    joueur.OldArgent = joueur.Argent;
                }
                
                if (NbTours % 1 == 0)
                {
                    Communicator.ResuméPartie();
                }

                _position = 0;
                if (Joueurs.Count <= 1)
                {
                    _finished = true;
                    Communicator.PartieEnded();
                    return;
                }

                if (NbTours > 2000)
                {
                    Communicator.PartieEnded();
                    _finished = true;
                    return;
                }
            }

            JoueurActuel = Joueurs[_position];
        }

        private void Jouer()
        {
            #region Gestion de la prison

            if (JoueurActuel.EnPrison)
            {
                if (JoueurActuel.NbToursPrisons > 0)
                {
                    JoueurActuel.NbToursPrisons--;
                }
                else
                {
                    JoueurActuel.EnPrison = false;
                }
            }
            JoueurActuel.Jouer();

            #endregion
        }

        public override string ToString()
        {
            return $"Tour : {NbTours}\n" +
                   $"Roll actuel : {Roll}\n" +
                   $"Joueurs : {Joueurs}\n" +
                   $"Joueur actuel : {JoueurActuel}\n" +
                   $"Plateau : {Board}\n" +
                   $"Faillites : {Faillites}";
        }

        internal void OnPLoaded(Plateau p)
        {
            OnPlateauLoaded?.Invoke(this, new PlateauArgs(p));
        }


        public void PlayerChanged()
        {
            OnPlayerChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}