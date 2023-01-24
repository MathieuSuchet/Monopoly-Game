using MonopolyLib.Logique.Joueurs;

namespace MonopolyLib.Logique.Joueurs.AjouteursArgent
{
    public class AjouteurArgentIa : AjouteurArgent
    {
        public AjouteurArgentIa(Joueur j) : base(j) { }

        public override void AjouterArgent(float value)
        {
            Player.Argent += value;
            base.AjouterArgent(value);
        }
    }
}
