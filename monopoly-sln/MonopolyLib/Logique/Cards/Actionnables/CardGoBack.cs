using MonopolyLib.Logique.Joueurs;

namespace MonopolyLib.Logique.Cards.Actionnables
{
    public class CardGoBack : Card, IActionnable
    {
        public readonly int NbCases;

        public CardGoBack()
        {
            NbCases = 3;
            Intitule = $"Reculez de {NbCases} cases";
        }

        public CardGoBack(int nbCases)
        {
            NbCases = nbCases;
            Intitule = $"Reculez de {NbCases} cases";
        }
        
        public void ActOn(Joueur j)
        {
            int pos = j.Position - NbCases;
            if (pos < 0)
            {
                pos += j.Partie.Board.Cases.Count;
            }

            while (j.Position != pos)
            {
                if (j.Position <= 0)
                {
                    j.Position += j.Partie.Board.Cases.Count;
                }

                j.Position--;
            }
        }
    }
}