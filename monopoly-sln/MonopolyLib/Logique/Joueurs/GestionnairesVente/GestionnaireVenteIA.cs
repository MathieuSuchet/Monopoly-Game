using MonopolyLib.Logique.Cases;
using MonopolyLib.Logique.Joueurs.VendeursCase;

namespace MonopolyLib.Logique.Joueurs.GestionnairesVente
{
    internal class GestionnaireVenteIa : GestionnaireVente
    {

        internal GestionnaireVenteIa(Joueur? j, VendeurCase vendeurCase) : base(j, vendeurCase) { }
        internal override void VendreJusquaRemboursement(float value)
        {
            while (value > Player.Argent)
            {
                if (Player.Cases.Count == 0)
                {
                    Player.FaitFaillite();
                    return;
                }
                //Console.WriteLine("Argent : " + Player.Argent);
                VendreOpti();
            }
        }

        internal override void VendreOpti()
        {
            CaseAchetable caseAchetable = GetBestCaseToSell();
            VendeurCase.VendreUneCase(caseAchetable);
        }
    }
}
