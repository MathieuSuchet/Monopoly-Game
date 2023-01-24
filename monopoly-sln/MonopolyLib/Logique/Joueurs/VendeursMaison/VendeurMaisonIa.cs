using System;
using MonopolyLib.Logique.Cases;

namespace MonopolyLib.Logique.Joueurs.VendeursMaison
{
    public class VendeurMaisonIa : VendeurMaison
    {
        public VendeurMaisonIa(Joueur j) : base(j) { }

        public override void VendreMaison(CaseMaison c)
        {
            throw new NotImplementedException();
        }

        public override void VendreMaison(CaseMaison c, int nbMaisons)
        {
            if(nbMaisons > c.NbMaisons)
            {
                Console.WriteLine("Demande de vente de trop de maisons");
                return;
            }
            c.NbMaisons -= nbMaisons;
            Player.AjouterArgent(nbMaisons * c.PrixUnitMaison);
            Player.OnHouseSell(nbMaisons, c);
        }
    }
}
