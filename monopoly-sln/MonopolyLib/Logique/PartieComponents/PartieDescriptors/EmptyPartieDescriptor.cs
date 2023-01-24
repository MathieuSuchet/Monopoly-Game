namespace MonopolyLib.Logique.PartieComponents.PartieDescriptors
{
    public class EmptyPartieDescriptor : AbstractPartieDescriptor
    {
        public override void ResuméPartie()
        {
            OnResumé(string.Empty);
        }

        public override void StartPartie()
        {
            OnStart(string.Empty);
        }

        public override void NextTurnPartie()
        {
            OnNextTurn(string.Empty);
        }

        public override void PartieEnded()
        {
            OnEnd(string.Empty);
        }
    }
}