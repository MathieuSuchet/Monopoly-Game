using System;
using MonopolyLib.Logique.Cases;
using MonopolyLib.Logique.Joueurs;
using MonopolyLib.Logique.Plateaux;

namespace MonopolyLib.Logique.Cards.Actionnables
{
    public class CardGoTo : Card, IActionnable
    {
        public Case Dest;

        public CardGoTo(Plateau p)
        {
            Random rd = new Random();
            int x = rd.Next(0, p.Cases.Count);
            Dest = p.Cases[x];
            Intitule = "Aller à la case " + Dest.Nom + ", si vous passez par la case départ, recevez " + ((CaseDépart)p.Cases[p.GetPosDépart()]).RécompensePassage;
        }

        public CardGoTo(Plateau p, Case c)
        {
            if (!p.Cases.Contains(c))
                throw new ArgumentOutOfRangeException();
            Dest = c;
            Intitule = "Aller à la case " + Dest.Nom + ", si vous passez par la case départ, recevez " + ((CaseDépart)p.Cases[p.GetPosDépart()]).RécompensePassage;
        }
        
        public void ActOn(Joueur j)
        {
            j.GoTo(Dest);
        }
    }
}