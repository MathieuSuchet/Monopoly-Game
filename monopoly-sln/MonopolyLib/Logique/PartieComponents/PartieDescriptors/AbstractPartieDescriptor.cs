using System;
using MonopolyLib.Logique.Events;

namespace MonopolyLib.Logique.PartieComponents.PartieDescriptors
{
    public abstract class AbstractPartieDescriptor : PartieMothersComponent
    {
        public static event EventHandler<PartieResumeArgs> OnPartieEnded;
        public static event EventHandler<PartieStartArgs> OnPartieStarted;
        public static event EventHandler<PartieResumeArgs> OnPartieResumé;
        public static event EventHandler<PartieResumeArgs> OnPartieNextTurn;

        public void OnEnd(string end)
        {
            OnPartieEnded?.Invoke(this,new PartieResumeArgs(end));
        }

        public void OnStart(string start)
        {
            OnPartieStarted?.Invoke(this, new PartieStartArgs(Partie, start));
        }

        public void OnResumé(string s)
        {
            OnPartieResumé?.Invoke(this, new PartieResumeArgs(s));
        }

        public void OnNextTurn(string s)
        {
            foreach (var joueur in Partie.Joueurs)
            {
                joueur.OnTurnEnded(joueur.OldArgent, joueur.Argent, joueur.OldPos, joueur.Position, joueur.Nom);
            }
            OnPartieNextTurn?.Invoke(this, new PartieResumeArgs(s));
            
        }
        
        public abstract void ResuméPartie();

        public abstract void StartPartie();

        public abstract void NextTurnPartie();

        public abstract void PartieEnded();


    }
}