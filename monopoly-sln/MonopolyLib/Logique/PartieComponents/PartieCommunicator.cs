using System;
using MonopolyLib.Logique.PartieComponents.PartieDescriptors;
using MonopolyLib.Logique.PartieComponents.Parties;

namespace MonopolyLib.Logique.PartieComponents
{
    public class PartieCommunicator
    {
        private readonly AbstractPartieDescriptor _descriptor;

        public PartieCommunicator(Partie partie)
        {
            _descriptor = new EmptyPartieDescriptor();
            _descriptor.Partie = partie;
        }

        public PartieCommunicator(Partie partie, AbstractPartieDescriptor descriptor)
        {
            _descriptor = descriptor;
            _descriptor.Partie = partie;
        }

        private bool IsDescriptorNull()
        {
            if (!(_descriptor is null)) return true;
            Console.WriteLine("Descriptor is null");
            return false;

        }

        public void ResuméPartie()
        {
            if (!IsDescriptorNull())
            {
                return;
            }
            _descriptor.ResuméPartie();
        }

        public void NextTurnPartie()
        {
            if (!IsDescriptorNull())
            {
                return;
            }
            _descriptor.NextTurnPartie();
        }

        public void StartPartie()
        {
            if (!IsDescriptorNull())
            {
                return;
            }
            _descriptor.StartPartie();
        }

        public void PartieEnded()
        {
            if (!IsDescriptorNull())
            {
                return;
            }
            _descriptor.PartieEnded();
        }
    }
}