// See https://aka.ms/new-console-template for more information

using MonopolyLib.Logique.Joueurs;
using MonopolyLib.Logique.PartieComponents.Parties;

class Program
{
    public static void Main(string[] args)
    {
        List<Joueur> joueurs = new List<Joueur>
        {
            new JoueurIa("Player 1", false),
            new JoueurIa("Player 2", false),
            new JoueurIa("Player 3", false),
            new JoueurIa("Player 4", false),
        };

        Partie p = new PartieNormale(joueurs, false);
        p.Gestionnaire();
    }
}