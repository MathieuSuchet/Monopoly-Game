using System;
using MonopolyLib.Logique.Joueurs;

namespace MonopolyLib.Logique.Events
{
    public class PlayerResumeArgs : EventArgs
    {
        public Joueur Joueur;

        public string Resume;

        public PlayerResumeArgs(Joueur j, string resume)
        {
            Joueur = j;
            Resume = resume;
        }
    }
}