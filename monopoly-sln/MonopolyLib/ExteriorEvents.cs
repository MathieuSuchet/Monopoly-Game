using System;
using MonopolyLib.Logique.Events;
using MonopolyLib.Logique.Joueurs.Descriptors;
using MonopolyLib.Logique.PartieComponents.PartieDescriptors;

namespace MonopolyLib
{
    public static class ExteriorEvents
    {
        public static EventHandler<PlayerResumeArgs> PlayerResume
        {
            get => null;
            set => AbstractDescriptor.OnPlayerResume += value;
        }

        public static EventHandler<PartieStartArgs> PartieStarted
        {
            get => null;
            set => AbstractPartieDescriptor.OnPartieStarted += value;
        }
        
        public static EventHandler<PartieResumeArgs> PartieEnded
        
        {
            get => null;
            set => AbstractPartieDescriptor.OnPartieEnded += value;
        }
        
        public static EventHandler<PartieResumeArgs> PartieResumé
        {
            get => null;
            set => AbstractPartieDescriptor.OnPartieResumé += value;
        }
        
        public static EventHandler<PartieResumeArgs> PartieNextTurn
        {
            get => null;
            set => AbstractPartieDescriptor.OnPartieNextTurn += value;
        }
    }
}