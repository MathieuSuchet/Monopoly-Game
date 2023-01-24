using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MonopolyLib.Logique.Cases;
using MonopolyLib.Logique.Joueurs;

namespace MonopolyLib.Logique.Cards.Actionnables
{
    public class CardHouseHotels : Card, IActionnable
    {
        private readonly int _maison = 45;
        private readonly int _hotel = 115;
        public CardHouseHotels()
        {
            Intitule = $"Payer {_maison} par maisons et {_hotel} par hotels";
            Usage = Usability.Always;
        }

        public CardHouseHotels(int maison, int hotels)
        {
            _maison = maison;
            _hotel = hotels;
            
            Intitule = $"Payer {_maison} par maisons et {_hotel} par hotels";
            Usage = Usability.Always;
        }
        
        public void ActOn(Joueur j)
        {
            float profit = 0;
            
            IEnumerable<CaseMaison> casesMaison = j.Cases.FindAll(x => x.GetType() == typeof(CaseMaison)).Cast<CaseMaison>();
            List<CaseMaison> hotels = casesMaison.ToList().FindAll(x => x.NbMaisons == 5);

            profit += _hotel * hotels.Count;

            var maisons = casesMaison.ToList().FindAll(x => x.NbMaisons != 5);

            profit += _maison * maisons.Sum(x => x.NbMaisons);

            if (!j.RetirerArgent(profit))
            {
                j.GestionnaireVente.VendreJusquaRemboursement(profit);
            }
            
            j.OnTransaction(profit, j, null, "Paiement maison/hotel");

            Trace.WriteLine($"{j.Nom} doit payer {profit} pour ses maisons et hotels");
            
            j.Historique.Add(new KeyValuePair<string, float>($"Tour {j.Partie.NbTours} : Utilisation de la carte \"{Intitule}\"",-profit));

            NumberOfUse--;
        }
    }
}