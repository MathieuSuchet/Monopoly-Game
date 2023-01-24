using MonopolyLib.Logique.PartieComponents.Parties;

namespace MonopolyLib.Logique.Plateaux
{
    public class PlateauNormal : Plateau
    {
        public PlateauNormal(Partie p) : base(p)
        {
        }

        public PlateauNormal(int numberOfCardSets, Partie p) : base(numberOfCardSets, p)
        {
        }
    }
}