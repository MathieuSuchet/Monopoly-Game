using System;
using System.Collections.Generic;
using MonopolyLib.Logique.Joueurs;

namespace MonopolyLib.Logique.Joueurs.Descriptors
{
    public class EmptyDescriptor : AbstractDescriptor
    {
        public EmptyDescriptor(Joueur j) : base(j)
        {
        }

        public override void TurnStarted()
        {
            
        }

        public override void ResuméJoueur()
        {
            OnResume(Joueur,string.Empty);
        }

        public override void DiceState(List<int> result)
        {
            
        }
    }
}