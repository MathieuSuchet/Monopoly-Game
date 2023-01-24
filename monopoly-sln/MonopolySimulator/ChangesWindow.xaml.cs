using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using MonopolyLib.Logique.Joueurs;

namespace MonopolySimulator;

public partial class ChangesWindow : ControlMonopoly
{
    public ChangesWindow()
    {
        InitializeComponent();
    }

    public ChangesWindow(Tour tour) : base(tour)
    {
        InitializeComponent();
    }
    
    public ChangesWindow(Tour? tour, Joueur? j)
    {
        InitializeComponent();
        Tour = tour;
        Player = j;
    }
    
    protected override void CheckTour()
    {
        TourStatus.Text = Tour is null ? "Veuillez sélectionner un tour" : "Tour " + Tour.NumTour;
        if (Tour is null || Player is null) return;
        SetText(Tour.Changes.Where(x => x.Dest == Player).ToList(), true);
        SetText(Tour.Changes.Where(x => x.Source == Player).ToList(), false);
    }
    
    

    protected override void CheckJoueur()
    {
        PlayerStatus.Text = Player is null ? "Veuillez sélectionner un joueur" : "Mouvements d'argent pour " + Player.Nom;
        if (Tour is null || Player is null) return;
        SetText(Tour.Changes.Where(x => x.Dest == Player).ToList(), true);
        SetText(Tour.Changes.Where(x => x.Source == Player).ToList(), false);

    }
    
    private void SetText(List<Change> text, bool win)
    {
        if (win)
        {
            ChangesWins.Text = string.Empty;
            if (text.Count == 0)
            {
                ChangesWins.Text = "Aucun mouvement d'argent";
            }

            for (int i = 0; i < text.Count; i++)
            {
                ChangesWins.Text += $"{text[i].Dest.Nom} <=  {text[i].Source.Nom} ({text[i].ChangeAmount}) [{text[i].Reason}]" + "\n";
            }
        }
        else
        {
            ChangesLosses.Text = string.Empty;
            if (text.Count == 0)
            {
                ChangesLosses.Text = "Aucun mouvement d'argent";
            }
            for (int i = 0; i < text.Count; i++)
            {
                ChangesLosses.Text += $"{text[i].Source.Nom} =>  {text[i].Dest.Nom} ({text[i].ChangeAmount})" + "\n";
            }
        }
    }

    public override void ClearContent()
    {
        ChangesWins.Text = "";
        ChangesLosses.Text = "";
    }
}