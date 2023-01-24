using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using MonopolyLib.Logique.Joueurs;

namespace MonopolySimulator;

public partial class AvancementWindow : ControlMonopoly
{
    public AvancementWindow()
    {
        InitializeComponent();
    }

    public AvancementWindow(Tour tour) : base(tour)
    {
        InitializeComponent();
    }

    public AvancementWindow(Tour? tour, Joueur? j)
    {
        InitializeComponent();
        Tour = tour;
        Player = j;
    }
    
    protected override void CheckTour()
    {
        TourStatus.Text = Tour is null ? "Veuillez sélectionner un tour" : "Tour " + Tour.NumTour;
        if (Tour is null || Player is null) return;
        if(Tour.Avancements.ContainsKey(Player))
            SetText(Tour.Avancements[Player]);
        else
        {
            SetText(new List<string>
            {
                "Pas d'avancements pour " + Player.Nom,
            });
        }
    }

    protected override void CheckJoueur()
    {
        PlayerStatus.Text = Player is null ? "Veuillez sélectionner un joueur" : "Avancement pour " + Player.Nom;
        if (Tour is null || Player is null) return;
        if(Tour.Avancements.ContainsKey(Player))
            SetText(Tour.Avancements[Player]);
        else
        {
            SetText(new List<string>
            {
                "Pas d'avancements pour " + Player.Nom,
            });
        }
    }

    private void SetText(List<string> text)
    {
        Avancement.Text = string.Empty;
        for (int i = 0; i < text.Count; i++)
        {
            Avancement.Text += text[i] + "\n";
        }
    }

    public override void ClearContent()
    {
        Avancement.Text = "";
    }
}