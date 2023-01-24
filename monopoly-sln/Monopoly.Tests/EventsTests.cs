using System.Diagnostics;
using MonopolyLib.Logique.Joueurs;
using MonopolyLib.Logique.PartieComponents.Parties;
using Xunit.Abstractions;

namespace Monopoly.Tests;

public class EventsTests
{

    private ITestOutputHelper _testOutput;

    public EventsTests(ITestOutputHelper helper)
    {
        _testOutput = helper;
    }
    
    [Fact]
    public void Taxes()
    {
        JoueurIa victim = new JoueurIa("Victim", true);

        List<Joueur> joueurs = new List<Joueur>
        {
            victim
        };
        Partie p = new PartieNormale(joueurs, true);

        victim.Position = 4;
        float oldArgent = victim.Argent;
        victim.GestionCaseIa(p.Board.Cases[victim.Position]);
        float newArgent = victim.Argent;

        float diff = 200;
        
        Assert.True(Math.Abs(oldArgent - newArgent - diff) < 0.001, $"Taxes are at {diff}, player paid {oldArgent - newArgent}");
        _testOutput.WriteLine("Player money before : " + oldArgent);
        _testOutput.WriteLine("Player money after  : " + newArgent);

        _testOutput.WriteLine("");
        
        _testOutput.WriteLine("Target difference : " + diff);
        _testOutput.WriteLine("Observed difference : " + (oldArgent - newArgent));
    }
    
    [Fact]
    public void LuxeTaxe()
    {
        JoueurIa victim = new JoueurIa("Victim", true);

        List<Joueur> joueurs = new List<Joueur>
        {
            victim
        };
        Partie p = new PartieNormale(joueurs, true);

        victim.Position = 38;
        float oldArgent = victim.Argent;
        victim.GestionCaseIa(p.Board.Cases[victim.Position]);
        float newArgent = victim.Argent;
        
        Assert.True(Math.Abs(oldArgent - newArgent - 100) < 0.001, $"Luxe taxe is at 100, player paid {oldArgent - newArgent}");
    }
    
    [Fact]
    public void InPrisonTest()
    {
        JoueurIa victim = new JoueurIa("Victim", true);

        List<Joueur> joueurs = new List<Joueur>
        {
            victim
        };
        Partie p = new PartieNormale(joueurs, true);

        victim.EnPrison = true;
        Assert.True(victim.NbToursPrisons == 3, $"The player should have 3 turns left in prison, currently have {victim.NbToursPrisons}");
    }
    
    [Fact]
    public void OutPrisonTest()
    {
        JoueurIa victim = new JoueurIa("Victim", true);

        List<Joueur> joueurs = new List<Joueur>
        {
            victim
        };
        Partie p = new PartieNormale(joueurs, true);

        victim.EnPrison = true;
        float oldArgent = victim.Argent;
        victim.EnPrison = false;
        float newArgent = victim.Argent;
        
        Assert.True(Math.Abs(oldArgent - newArgent - 50) < 0.0001, $"Should have paid 50 to exit to pay prison, paid {oldArgent - newArgent}");
        Assert.True(victim.NbToursPrisons == 0, $"Player should no longer have any turn in prison, currently have {victim.NbToursPrisons}");
        Assert.True(victim.Position == p.Board.GetPosVisite(), $"Player is not on the jail position, it is at {victim.Position} instead of {p.Board.GetPosVisite()}");
    }
}