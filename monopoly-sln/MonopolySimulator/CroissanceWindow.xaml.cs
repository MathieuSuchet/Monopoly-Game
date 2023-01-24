using System.Collections.Generic;
using System.Windows.Controls;
using MonopolyLib.Logique.Joueurs;

namespace MonopolySimulator;

public partial class CroissanceWindow : ControlMonopoly
{
    public CroissanceWindow()
    {
        InitializeComponent();
    }

    public CroissanceWindow(Tour tour)
    {
        InitializeComponent();
        Tour = tour;
    }
    
    public CroissanceWindow(Tour? tour, Joueur? j)
    {
        InitializeComponent();
        Tour = tour;
        Player = j;
    }

    protected override void CheckTour()
    {
        TourStatus.Text = Tour is null ? "Veuillez sélectionner un tour" : "Tour " + Tour.NumTour;
        if (Player is null || Tour is null || !Tour.Croissances.ContainsKey(Player)) return;
        SetText(Tour.Croissances[Player]);
    }

    protected override void CheckJoueur()
    {
        PlayerStatus.Text = Player is null ? "Veuillez sélectionner un joueur" : "Croissance pour " + Player.Nom;
        if (Player is null || Tour is null || !Tour.Croissances.ContainsKey(Player)) return;
        SetText(Tour.Croissances[Player]);
        
    }

    private void SetText(string croissance)
    {
        Croissances.Text += croissance + "\n";
    }

    public override void ClearContent()
    {
        Croissances.Text = "";
    }
}