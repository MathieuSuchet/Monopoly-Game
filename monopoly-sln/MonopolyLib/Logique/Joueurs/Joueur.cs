using System;
using System.Collections.Generic;
using MonopolyLib.Logique.Cards;
using MonopolyLib.Logique.Cards.Actionnables;
using MonopolyLib.Logique.Cards.CardSets;
using MonopolyLib.Logique.Cards.Keepables;
using MonopolyLib.Logique.Cases;
using MonopolyLib.Logique.Events;
using MonopolyLib.Logique.Joueurs.AcheteursCase;
using MonopolyLib.Logique.Joueurs.AcheteursMaison;
using MonopolyLib.Logique.Joueurs.AjouteursArgent;
using MonopolyLib.Logique.Joueurs.Descriptors;
using MonopolyLib.Logique.Joueurs.GestionnairesFaillite;
using MonopolyLib.Logique.Joueurs.GestionnairesVente;
using MonopolyLib.Logique.Joueurs.LanceursDés;
using MonopolyLib.Logique.Joueurs.RetireursArgent;
using MonopolyLib.Logique.Joueurs.VendeursCase;
using MonopolyLib.Logique.Joueurs.VendeursMaison;
using Newtonsoft.Json;
using MonopolyLib.Logique.PartieComponents.Parties;

namespace MonopolyLib.Logique.Joueurs
{
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class Joueur
    {
        private int _position;

        #region Variables Non Wrapper
        internal Partie Partie { get; private set; }
        
        
        /// <summary>
        /// Money of the player (Read-only)
        /// </summary>
        [JsonProperty]
        public float Argent { get; internal set; }
        
        /// <summary>
        /// Position of the player relative to the board (Read-only)
        /// </summary>
        [JsonProperty]
        public int Position
        {
            get => _position;
            set
            {
                int oldPos = _position;
                _position = value;
                OnMovement(oldPos, value);
            }
        }
        
        
        /// <summary>
        /// Name of the player (Read-only)
        /// </summary>
        [JsonProperty]
        public string Nom { get; }

        /// <summary>
        /// Current number of properties (Read-only)
        /// </summary>
        [JsonProperty]
        public int NbProperties { get; internal set; }

        internal int OldPos { get; set; }

        public float OldArgent { get; set; }
        
        public List<CaseAchetable> Cases { get; } = new List<CaseAchetable>();

        internal List<KeyValuePair<string, float>> Historique { get; } = new List<KeyValuePair<string, float>>();

        internal bool Faillite { get; set; }

        protected bool EnPrisonAttr { get; set; }
        public bool EnPrison
        {
            get => GetEnPrison();
            set
            {
                OnPrisonChanged(value);
                SetEnPrison(value);
            } 
        }

        private void OnPrisonChanged(bool value)
        {
            OnPlayerPrisonChanged?.Invoke(this, new PlayerPrisonArgs(value));
        }

        public int NbToursPrisons;

        public int Agressivite;
        internal List<IKeepable> Cards = new List<IKeepable>();

        public Dictionary<Joueur, float> Multiplicateurs = new Dictionary<Joueur, float>();

        #endregion

        #region Variables Wrapper (Qui dérivent de PlayerComponentMother)

        protected AjouteurArgent AjouteurArgent { get; set; }
        protected RetireurArgent RetireurArgent { get; set; }


        protected internal AcheteurMaison AcheteurMaison { get; set; }
        protected VendeurMaison VendeurMaison { get; set; }

        protected AcheteurCase AcheteurCase { get; set; }
        protected VendeurCase VendeurCase { get; set; }

        protected LanceurDés LanceurDés { get; set; }
        protected GestionnaireFaillite GestionnaireFaillite { get; set; }

        internal GestionnaireVente GestionnaireVente { get; set; }

        internal ProfitCalculator ProfitCalculator { get; set; }
        
        internal AbstractDescriptor Descriptor { get; set; }

        #endregion

        #region Events
        
        /// <summary>
        /// Event that occurs when the player turn starts
        /// </summary>
        public event EventHandler<PlayerStartTurnArgs> OnPlayerTurnStarted;
        
        /// <summary>
        /// Event that occurs when the player turn ends
        /// </summary>
        public event EventHandler<PlayerTurnArgs> OnPlayerTurnEnded;

        /// <summary>
        /// Event that occurs when the players wins money
        /// </summary>
        public event EventHandler<PlayerMoneyMovementArgs> OnPlayerMoneyChanged;
        
        /// <summary>
        /// Event that occurs on player movement
        /// </summary>
        public event EventHandler<PlayerMoveArgs> OnPlayerMovement;
        
        /// <summary>
        /// Event that occurs on player buying a new card
        /// </summary>
        public event EventHandler<PlayerCaseArgs> OnPlayerBuyCase;
        
        /// <summary>
        /// Event that occurs on player selling a card
        /// </summary>
        public event EventHandler<PlayerCaseArgs> OnPlayerSellCase;
        
        /// <summary>
        /// Event that occurs when a player buys a new house/property
        /// </summary>
        public event EventHandler<PlayerHouseArgs> OnPlayerBuyHouse;
        
        /// <summary>
        /// Event that occurs when a player sells a new house/property
        /// </summary>
        public event EventHandler<PlayerHouseArgs> OnPlayerSellHouse;
        
        /// <summary>
        /// Event that occurs when a player reaches the "GO" frame
        /// </summary>
        public event EventHandler<EventArgs> OnPlayerCaseDepart;
        
        /// <summary>
        /// Event that occurs when one player pays another player
        /// </summary>
        public event EventHandler<PlayerTransactionArgs> OnPlayerTransaction;
        
        /// <summary>
        /// Event that occurs when a player loses
        /// </summary>
        public event EventHandler<EventArgs> OnPlayerFaillite;
        
        /// <summary>
        /// Event that occurs when a player goes in or out of prison 
        /// </summary>
        public event EventHandler<PlayerPrisonArgs> OnPlayerPrisonChanged;
        
        /// <summary>
        /// Event that occurs when a players draws a card
        /// </summary>
        public event EventHandler<PlayerCardArgs> OnPlayerDrawCard;

        public event EventHandler<EventArgs> OnAmountCaseChanged;

        #endregion

        protected virtual bool GetEnPrison() { return EnPrisonAttr; }
        protected abstract void SetEnPrison(bool value);

        protected Joueur(string nom, bool silent)
        {
            Argent = 1000;
            Nom = nom;
            
            if (silent)
            {
                Descriptor = new EmptyDescriptor(this);
            }
            else
            {
                Descriptor = new NormalDescriptor(this);
            }

            //Random random = new Random();
            //Agressivite = random.Next(30, 101);
        }

        protected Joueur(float argent, string nom, bool silent)
        {
            Argent = argent;
            Nom = nom;
            if (silent)
            {
                Descriptor = new EmptyDescriptor(this);
            }
            else
            {
                Descriptor = new NormalDescriptor(this);
            }
        }

        protected Joueur(float argent, string nom, bool silent, int nbProperties)
        {
            Argent = argent;
            Nom = nom;
            if (silent)
            {
                Descriptor = new EmptyDescriptor(this);
            }
            else
            {
                Descriptor = new NormalDescriptor(this);
            }
            NbProperties = nbProperties;
        }

        /// <summary>
        /// Main function to play a turn
        /// </summary>
        public virtual void Jouer() {
            Descriptor.TurnStarted();
        }
        public abstract void AcheterUneCase(CaseAchetable c);
        public abstract void VendreUneCase(CaseAchetable c);

        public virtual void AcheterMaison(CaseMaison c) { AcheteurMaison.AcheterMaison(c); }
        public virtual void VendreMaison(CaseMaison c) { VendeurMaison.VendreMaison(c); }

        public abstract List<int> LancerDés();
        internal virtual Case GetCaseAtCurrentPosition() { return Partie.Board.Cases[Position]; }

        internal virtual bool RetirerArgent(float value) { return RetireurArgent.RetirerArgent(value); }

        internal virtual void AjouterArgent(float value) { AjouteurArgent.AjouterArgent(value); }

        public virtual void FaitFaillite() { GestionnaireFaillite.FaireFaillite(); }

        public abstract int FaireChoix(string intitule, List<string> choix, List<ConsoleKey> keys);

        public virtual void SetPartie(Partie p)
        {
            Partie = p;
        }
        public virtual void RemoveCard(IKeepable card)
        {
            if (Cards.Contains(card))
            {
                Cards.Remove(card);
            }
        }

        protected void TirerUneCarte(CardSet set)
        {
            Card card = set.Get(0);
            set.Remove(card);
            OnDrawnCard(card);
            switch (card)
            {
                case IKeepable ckeep:
                    Cards.Add(ckeep);
                    break;
                case IActionnable cact:
                    cact.ActOn(this);
                    set.Add(card);
                    break;
            }
        }

        public void GoTo(Case c)
        {
            while (Position != c.Position)
            {
                //Console.WriteLine("Changement " + Position);
                Position++;
                if (Position >= Partie.Board.Cases.Count)
                {
                    Position %= Partie.Board.Cases.Count;
                }

                if (!(GetCaseAtCurrentPosition() is CaseDépart cd)) continue;
                AjouterArgent(cd.RécompensePassage);
                OnTransaction(cd.RécompensePassage, null, this, "Passage case départ");
                OnCaseDepart();
            }
        }

        internal void OnTurnEnded(float oldArg, float newArg, int oldPos, int newPos, string nom)
        {
            OnPlayerTurnEnded?.Invoke(this, new PlayerTurnArgs(oldArg, newArg, oldPos, newPos, nom));
        }

        internal void OnMoneyChanged(float change, bool lost)
        {
            OnPlayerMoneyChanged?.Invoke(this, new PlayerMoneyMovementArgs(change, lost));
        }

        protected void OnMovement(int oldpos, int newpos)
        {
            OnPlayerMovement?.Invoke(this, new PlayerMoveArgs(oldpos, newpos));
        }

        protected void OnTurnStarted(int pos, float argent)
        {
            OnPlayerTurnStarted?.Invoke(this, new PlayerStartTurnArgs(pos, argent));
        }

        internal void OnHouseBuy(int nbMaison, CaseMaison caseMaison)
        {
            OnPlayerBuyHouse?.Invoke(this, new PlayerHouseArgs(caseMaison, nbMaison));
        }

        internal void OnHouseSell(int nbMaison, CaseMaison caseMaison)
        {
            OnPlayerSellHouse?.Invoke(this, new PlayerHouseArgs(caseMaison, nbMaison));
        }

        internal void OnCaseBuy(float argent, Case @case)
        {
            OnPlayerBuyCase?.Invoke(this, new PlayerCaseArgs(argent, @case));
            OnAmountCaseChanged?.Invoke(this, EventArgs.Empty);
        }

        internal void OnCaseSell(float argent, Case @case)
        {
            OnPlayerSellCase?.Invoke(this, new PlayerCaseArgs(argent, @case));
            OnAmountCaseChanged?.Invoke(this, EventArgs.Empty);
        }

        protected void OnCaseDepart()
        {
            OnPlayerCaseDepart?.Invoke(this, EventArgs.Empty);
        }

        internal void OnTransaction(float change, Joueur source, Joueur? dest, string reason)
        {
            OnPlayerTransaction?.Invoke(this, new PlayerTransactionArgs(change, source, dest, reason));
        }

        internal void OnFaillite()
        {
            OnPlayerFaillite?.Invoke(this, EventArgs.Empty);
        }

        protected void OnDrawnCard(Card card)
        {
            OnPlayerDrawCard?.Invoke(this, new PlayerCardArgs(card));
        }
    }
}
