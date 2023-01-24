using System;
using MonopolyLib.Logique.Events;
using MonopolyLib.Logique.Joueurs;

namespace MonopolyLib.Logique.Cards.Actionnables
{
    public class CardMoney : Card, IActionnable
    {
        private readonly int _value;
        private readonly bool _win;
        public CardMoney() : this(20, 100) { }

        public CardMoney(int minLimit, int maxLimit)
        {
            Random rd = new Random();
            _win = rd.Next(0, 3) == 1;
            _value = 20 * (rd.Next(minLimit, maxLimit) % 20);
            Intitule = _win ? $"Félicitations, vous avez gagné {_value}" : $"Dommage, vous avez perdu {_value}";
            Usage = Usability.Always;
        }

        public void ActOn(Joueur j)
        {
            if(!_win)
            {
                if (j.RetirerArgent(_value)) return;
                
                j.GestionnaireVente.VendreJusquaRemboursement(_value);
                if(!j.Faillite)
                {
                    j.RetirerArgent(_value);
                }
                j.OnTransaction(_value, j, null, "Paiement carte money");
            }
            else
            {
                j.AjouterArgent(_value);
                j.OnTransaction(_value, null, j, "Paiement carte money");
            }
                
        }
    }
}