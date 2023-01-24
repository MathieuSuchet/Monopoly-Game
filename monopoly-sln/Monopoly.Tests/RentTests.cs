using MonopolyLib.Logique.Cases;
using MonopolyLib.Logique.Joueurs;
using MonopolyLib.Logique.PartieComponents.Parties;
using MonopolyLib.Logique.Plateaux;

namespace Monopoly.Tests;

public class RentTests
{

    [Fact]
    public void RentTest1Service()
    {
        
        JoueurIa victim = new JoueurIa("Victim", true);

        JoueurIa bully = new JoueurIa("Bully", true);

        List<Joueur> joueurs = new List<Joueur>
        {
            victim,
            bully
        };
        Partie p = new PartieNormale(joueurs, true);

        victim.Position = 12;
        Partie.Roll = 12;

        CaseAchetable c1 = (CaseAchetable)p.Board.Cases[12];
        c1.Achetée = true;
        c1.Proprio = bully;
        bully.Cases.Add(c1);

        float oldmoney = victim.Argent;
        victim.GestionCaseIa(p.Board.Cases[victim.Position]);
        float newMoney = victim.Argent;

        Assert.True(Math.Abs(oldmoney - newMoney - 48) < 0.001, $"Should have paid 48, paid {oldmoney - newMoney}");
    }
    
    [Fact]
    public void RentTest2Services()
    {
        
        JoueurIa victim = new JoueurIa("Victim", true);

        JoueurIa bully = new JoueurIa("Bully", true);

        List<Joueur> joueurs = new List<Joueur>
        {
            victim,
            bully
        };
        Partie p = new PartieNormale(joueurs, true);

        victim.Position = 12;
        Partie.Roll = 12;

        CaseAchetable c1 = (CaseAchetable)p.Board.Cases[12];
        c1.Achetée = true;
        c1.Proprio = bully;
        bully.Cases.Add(c1);
        
        CaseAchetable c2 = (CaseAchetable)p.Board.Cases[28];
        c2.Achetée = true;
        c2.Proprio = bully;
        bully.Cases.Add(c2);

        float oldmoney = victim.Argent;
        victim.GestionCaseIa(p.Board.Cases[victim.Position]);
        float newMoney = victim.Argent;

        Assert.True(Math.Abs(oldmoney - newMoney - 120) < 0.001, $"Should have paid 120, paid {oldmoney - newMoney}");
    }
    

    [Fact]
    public void RentTestNotAllHouses()
    {
        JoueurIa victim = new JoueurIa("Victim", true, 100);

        JoueurIa bully = new JoueurIa("Bully", true);

        List<Joueur> joueurs = new List<Joueur>
        {
            victim,
            bully
        };
        Partie p = new PartieNormale(joueurs, true);

        victim.Position = 8;

        CaseAchetable c1 = (CaseAchetable)p.Board.Cases[6];
        c1.Achetée = true;
        c1.Proprio = bully;
        bully.Cases.Add(c1);
        
        CaseAchetable c2 = (CaseAchetable)p.Board.Cases[8];
        c2.Achetée = true;
        c2.Proprio = bully;
        bully.Cases.Add(c2);

        float oldmoney = victim.Argent;
        victim.GestionCaseIa(p.Board.Cases[victim.Position]);
        float newMoney = victim.Argent;

        Assert.True(Math.Abs(oldmoney - newMoney - 6) < 0.001, $"Should have paid 6, paid {oldmoney - newMoney}");
    }
    
    [Fact]
    public void RentTestAll2Houses()
    {
        JoueurIa victim = new JoueurIa("Victim", true);

        JoueurIa bully = new JoueurIa("Bully", true);

        List<Joueur> joueurs = new List<Joueur>
        {
            victim,
            bully
        };
        Partie p = new PartieNormale(joueurs, true);

        victim.Position = 39;

        CaseAchetable c1 = (CaseAchetable)p.Board.Cases[^1];
        c1.Achetée = true;
        c1.Proprio = bully;
        bully.Cases.Add(c1);
        
        CaseAchetable c2 = (CaseAchetable)p.Board.Cases[^3];
        c2.Achetée = true;
        c2.Proprio = bully;
        bully.Cases.Add(c2);

        float oldmoney = victim.Argent;
        victim.GestionCaseIa(p.Board.Cases[victim.Position]);
        float newMoney = victim.Argent;

        Assert.True(Math.Abs(oldmoney - newMoney - 100) < 0.001, $"Should have paid 100, paid {oldmoney - newMoney}");
    }
    
    
    [Fact]
    public void RentTestAll3Houses()
    {

        JoueurIa victim = new JoueurIa("Victim", true);

        JoueurIa bully = new JoueurIa("Bully", true);

        List<Joueur> joueurs = new List<Joueur>
        {
            victim,
            bully
        };
        Partie p = new PartieNormale(joueurs, true);

        victim.Position = 8;

        CaseAchetable c1 = (CaseAchetable)p.Board.Cases[6];
        c1.Achetée = true;
        c1.Proprio = bully;
        bully.Cases.Add(c1);
        
        CaseAchetable c2 = (CaseAchetable)p.Board.Cases[8];
        c2.Achetée = true;
        c2.Proprio = bully;
        bully.Cases.Add(c2);
        
        CaseAchetable c3 = (CaseAchetable)p.Board.Cases[9];
        c3.Achetée = true;
        c3.Proprio = bully;
        bully.Cases.Add(c3);

        float oldmoney = victim.Argent;
        victim.GestionCaseIa(p.Board.Cases[victim.Position]);
        float newMoney = victim.Argent;

        Assert.True(Math.Abs(oldmoney - newMoney - 12) < 0.001, $"Should have paid 12, paid {oldmoney - newMoney}");
    }

    [Fact]
    public void RentMultiplierTest()
    {
        JoueurIa victim = new JoueurIa("Victim", true);

        JoueurIa bully = new JoueurIa("Bully", true);

        List<Joueur> joueurs = new List<Joueur>
        {
            victim,
            bully
        };
        Partie p = new PartieNormale(joueurs, true);

        victim.Position = 8;
        victim.Multiplicateurs[bully] = 4;

        CaseAchetable c1 = (CaseAchetable)p.Board.Cases[6];
        c1.Achetée = true;
        c1.Proprio = bully;
        bully.Cases.Add(c1);
        
        CaseAchetable c2 = (CaseAchetable)p.Board.Cases[8];
        c2.Achetée = true;
        c2.Proprio = bully;
        bully.Cases.Add(c2);
        
        CaseAchetable c3 = (CaseAchetable)p.Board.Cases[9];
        c3.Achetée = true;
        c3.Proprio = bully;
        bully.Cases.Add(c3);

        float oldmoney = victim.Argent;
        victim.GestionCaseIa(p.Board.Cases[victim.Position]);
        float newMoney = victim.Argent;

        Assert.True(Math.Abs(oldmoney - newMoney - 48) < 0.001, $"Should have paid 48, paid {oldmoney - newMoney}");
    }
}