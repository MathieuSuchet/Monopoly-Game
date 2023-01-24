using MonopolyLib.Logique.Joueurs;

namespace MonopolyLib.Logique.Joueurs.AjouteursArgent
{
    public class AjouteurArgentBasique : AjouteurArgent
    {
        public AjouteurArgentBasique(Joueur j) : base(j) { }
        public override void AjouterArgent(float value)
        {
            base.AjouterArgent(value);
            Player.Argent += value;
        }
    }
}
