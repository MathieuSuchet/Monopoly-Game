using MonopolyLib.Logique.Cases;
using MonopolyLib.Logique.Joueurs;
using MonopolyLib.Logique.PartieComponents.Parties;

namespace Monopoly.Tests;

public class PlayerActionsTests
{
    [Fact]
    public void BuyPlaceTest()
    {
        JoueurIa buyer = new JoueurIa("Buyer", true)
        {
            Agressivite = 100
        };

        List<Joueur> players = new List<Joueur>
        {
            buyer
        };

        Partie p = new PartieNormale(players, true);

        var caseToBuy = (CaseAchetable)p.Board.Cases[8];

        float oldArgent = buyer.Argent;
        
        buyer.AcheterUneCase(caseToBuy);

        float newArgent = buyer.Argent;
        
        Assert.True(caseToBuy.Achetée, "The place is not considered bought");
        Assert.True(caseToBuy.Proprio == buyer, "The owner is not the buyer");
        Assert.Contains(caseToBuy, buyer.Cases);

        Assert.True(Math.Abs(oldArgent - newArgent - caseToBuy.PrixAchat) < 0.0001,
            $"The buyer bought the place for {oldArgent - newArgent}, should have bought it for {caseToBuy.PrixAchat}");
    }

    [Fact]
    public void SellPlaceTest()
    {
        JoueurIa seller = new JoueurIa("Buyer", true)
        {
            Agressivite = 100
        };

        List<Joueur> players = new List<Joueur>
        {
            seller
        };

        Partie p = new PartieNormale(players, true);

        var caseToSell = (CaseAchetable)p.Board.Cases[8];
        caseToSell.Achetée = true;
        caseToSell.Proprio = seller;
        seller.Cases.Add(caseToSell);
        

        float oldArgent = seller.Argent;
        
        seller.VendreUneCase(caseToSell);

        float newArgent = seller.Argent;
        
        Assert.False(caseToSell.Achetée, "The place is still considered bought");
        Assert.True(caseToSell.Proprio == null, "The owner is not null");
        Assert.DoesNotContain(caseToSell, seller.Cases);

        Assert.True(Math.Abs(Math.Abs(oldArgent - newArgent) - caseToSell.PrixAchat) < 0.001,
            $"The buyer sold the place for {Math.Abs(oldArgent - newArgent)}, should have sold it for {caseToSell.PrixAchat}");
    }

    [Fact]
    public void BuyHouseWhenNotAllPlaces()
    {
        JoueurIa buyer = new JoueurIa("Buyer", true)
        {
            Agressivite = 100
        };

        List<Joueur> joueurs = new List<Joueur>
        {
            buyer
        };

        Partie p = new PartieNormale(joueurs, true);
        
        CaseMaison c1 = (CaseMaison)p.Board.Cases[^1];
        buyer.AcheterUneCase(c1);

        buyer.Position = 39;

        float oldArgent = buyer.Argent;
        buyer.AcheterMaison(c1);
        float newArgent = buyer.Argent;
        
        Assert.True(c1.NbMaisons == 0, $"The place shouldn't have any house, currently has {c1.NbMaisons}");
        Assert.True(Math.Abs(c1.PrixFinal - 50) < 0.0001, $"The place should cost 50, currently cost {c1.PrixFinal}");
        Assert.True(oldArgent - newArgent == 0, $"The user didn't buy any house, difference should be 0, currently is {oldArgent - newArgent}");
    }

    [Fact]
    public void BuyHouse()
    {
        JoueurIa buyer = new JoueurIa("Buyer", true, 10000)
        {
            Agressivite = 100
        };

        List<Joueur> joueurs = new List<Joueur>
        {
            buyer
        };

        Partie p = new PartieNormale(joueurs, true);
        
        CaseMaison c1 = (CaseMaison)p.Board.Cases[^1];
        buyer.AcheterUneCase(c1);
        
        CaseMaison c2 = (CaseMaison)p.Board.Cases[^3];
        buyer.AcheterUneCase(c2);

        buyer.Position = 39;

        float oldArgent = buyer.Argent;
        buyer.AcheterMaison(c1);
        float newArgent = buyer.Argent;
        
        Assert.True(c1.NbMaisons == 5, $"This place should now have 5 houses (1 hotel), currently has {c1.NbMaisons}");
        Assert.True(Math.Abs(c1.PrixFinal - 2000) < 0.0001, $"This place should now cost 2000, costs {c1.PrixFinal}");
        Assert.True(Math.Abs(oldArgent - newArgent - 1000) < 0.0001, $"The player should have paid 1000, it paid {oldArgent - newArgent}");
    }

    [Fact]
    public void SellHouse()
    {
        JoueurIa buyer = new JoueurIa("Buyer", true, 10000)
        {
            Agressivite = 100
        };

        List<Joueur> joueurs = new List<Joueur>
        {
            buyer
        };

        Partie p = new PartieNormale(joueurs, true);
        
        CaseMaison c1 = (CaseMaison)p.Board.Cases[^1];
        buyer.AcheterUneCase(c1);
        
        CaseMaison c2 = (CaseMaison)p.Board.Cases[^3];
        buyer.AcheterUneCase(c2);
        
        buyer.AcheterMaison(c1);
        
        buyer.Position = 39;

        float oldArgent = buyer.Argent;
        buyer.VendreMaison(c1, 5);
        float newArgent = buyer.Argent;
        
        Assert.True(c1.NbMaisons == 0, $"The place shouldn't have any house, currently has {c1.NbMaisons}");
        Assert.True(Math.Abs(c1.PrixFinal - 50) < 0.001, $"This place should cost 50, currently costs {c1.PrixFinal}");
        Assert.True(Math.Abs(Math.Abs(oldArgent - newArgent) - 1000) < 0.0001, $"The user should have gained 1000, gained {Math.Abs(oldArgent - newArgent)}");
    }

    [Fact]
    public void SellPlaceWhenHouse()
    {
        JoueurIa seller = new JoueurIa("Seller", true, 10000)
        {
            Agressivite = 100
        };

        List<Joueur> joueurs = new List<Joueur>
        {
            seller
        };

        Partie p = new PartieNormale(joueurs, true);
        
        CaseMaison c1 = (CaseMaison)p.Board.Cases[^1];
        seller.AcheterUneCase(c1);
        
        CaseMaison c2 = (CaseMaison)p.Board.Cases[^3];
        seller.AcheterUneCase(c2);
        
        seller.AcheterMaison(c1);
        
        

        seller.Position = 39;

        float oldArgent = seller.Argent;
        seller.VendreUneCase(c1);
        float newArgent = seller.Argent;
        
        //Place is sold properly
        Assert.False(c1.Achetée, "The place is still considered bought");
        Assert.True(c1.Proprio == null, "The owner is not null");
        Assert.DoesNotContain(c1, seller.Cases);
        
        //Houses disappeared
        Assert.True(c1.NbMaisons == 0, $"This place shouldn't have any house, currently has {c1.NbMaisons}");
        
        //Seller has been refunded
        Assert.True(Math.Abs(Math.Abs(oldArgent - newArgent) - 1500) < 0.0001, $"Player should have been refunded 1400, got refunded {Math.Abs(oldArgent - newArgent)}");
    }
}