
using MonopolyLib.Logique.Joueurs;

namespace MonopolySimulator;

public class Change
{
    public float ChangeAmount;
    public Joueur Source;
    public Joueur Dest;
    public string Reason;

    public Change(float change, Joueur s, Joueur d, string reason)
    {
        ChangeAmount = change;
        Source = s;
        Dest = d;
        Reason = reason;
    }
}