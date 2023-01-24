using System;
using System.Collections.Generic;
using MonopolyLib.Logique.Events;
using MonopolyLib.Logique.Joueurs;

namespace MonopolyLib.Logique.Joueurs.Descriptors
{
    public abstract class AbstractDescriptor
    {
        public Joueur Joueur;

        public static event EventHandler<PlayerResumeArgs> OnPlayerResume;
        

        public AbstractDescriptor(Joueur j)
        {
            Joueur = j;
        }

        public abstract void TurnStarted();

        public abstract void ResuméJoueur();
        protected void OnResume(Joueur r, string s)
        {
            OnPlayerResume?.Invoke(this, new PlayerResumeArgs(r,s));
        }

        public abstract void DiceState(List<int> result);
    }
}